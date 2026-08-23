using LukeQuery.Datatypes;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace LukeQuery.DataSource;

class CsvDataSource: IDataSource
{
    public string Filename;
    public Schema Schema;
    private readonly bool HasHeaders;
    private readonly int BatchSize;

    public CsvDataSource(string filename, bool hasHeaders, int batchSize, Schema? schema = null)
    {
        Filename = filename;
        HasHeaders = hasHeaders;
        BatchSize = batchSize;
        Schema = schema ?? InferSchema();
    }

    public Schema InferSchema()
    {
        List<Field> fields = [];

        if (!File.Exists(Filename))
        {
            throw new FileNotFoundException(Filename);
        }

        TextFieldParser parser = new(Filename)
        {
            TextFieldType = FieldType.Delimited
        };
        parser.SetDelimiters(",");

        string[] headers = parser.ReadFields() ?? [];
        
        if (HasHeaders)
        {
            foreach (string header in headers)
            {
                fields.Add(new Field(header, ArrowTypes.StringType));
            }
        }
        else
        {
            int i = 0;
            foreach (string header in headers)
            {
                Field f = new($"field_{i}", ArrowTypes.StringType);
                fields.Add(f);
                i++;
            }
        }

        return new Schema(fields);
    }

    public IEnumerable<RecordBatch> Scan(List<String>? projection)
    {
        IEnumerable<RecordBatch> recordBatches = [];
        List<IColumnVector> columnVectors = [];
        Schema readSchema;
        List<String> readProjection = projection ?? [];

        if (!File.Exists(Filename))
        {
            throw new FileNotFoundException(Filename);
        }
        
        if (readProjection.Count != 0)
        {
            readSchema = Schema.Select(readProjection);
        }
        else
        {
            readSchema = Schema;
        }

        TextFieldParser parser = new(Filename)
        {
            TextFieldType = FieldType.Delimited
        };
        parser.SetDelimiters(",");
        string[] values;

        if (HasHeaders)
        {
            values = parser.ReadFields() ?? [];
        }

        int row = 1;
        while (!parser.EndOfData)
        {
            foreach (Field field in readSchema.Fields)
            {
                columnVectors.Add(new ArrowFieldVector(field, []));
            }

            for (int j = 0; j <= BatchSize - 1; j++)
            {                
                values = parser.ReadFields() ?? [];
                if (values.Count() != 0)
                {
                    for (int i = 0; i <= readSchema.FieldCount() - 1; i++)
                    {
                        columnVectors[i].AddValue(values[i]);
                    }
                }

                row++;
            }

            recordBatches = recordBatches.Append(new RecordBatch(readSchema, [.. columnVectors]));
            columnVectors.Clear();
        }

        return recordBatches;
    }
}