using LukeQuery.Datatypes;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace LukeQuery.DataSource;

/// <summary>
/// Comma seperated value data source.
/// </summary>
public class CsvDataSource: IDataSource
{
    private readonly string Filename;
    private readonly Schema Schema;
    private readonly bool HasHeaders;
    private readonly int BatchSize;

    /// <summary>
    /// Class constructor for CSV datasources. Will infer schema if one is not provided.
    /// </summary>
    /// <param name="filename">Filename or path.</param>
    /// <param name="hasHeaders">Does the file have headers?</param>
    /// <param name="batchSize">The number of batches to parse.</param>
    /// <param name="schema">Optional schema.</param>
    public CsvDataSource(string filename, bool hasHeaders, int batchSize, Schema? schema = null)
    {
        Filename = filename;
        HasHeaders = hasHeaders;
        BatchSize = batchSize;
        Schema = schema ?? InferSchema();
    }

    /// <summary>
    /// Validates that the file used in the constructor is valid.
    /// </summary>
    public void ValidateFile()
    {
        if (!File.Exists(Filename))
        {
            throw new FileNotFoundException(Filename);
        }
    }

    /// <summary>
    /// Creates a CSV Reader object.
    /// </summary>
    /// <returns>CsvReader</returns>
    public CsvReader CreateCSVReader()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = HasHeaders,
        };
        var reader = new StreamReader(Filename);
        var csv = new CsvReader(reader, config);

        return csv;
    }

    /// <summary>
    /// Infers the schema for the datasource if one is not provided.
    /// </summary>
    /// <returns>Schema</returns>
    public Schema InferSchema()
    {
        List<Field> fields = [];
        string[] headers;

        ValidateFile();

        var csv = CreateCSVReader();
        csv.Read();

        if (HasHeaders)
        {
            csv.ReadHeader();
            headers = csv.HeaderRecord ?? [];
            csv.Read();

            int i = 0;
            foreach (string header in headers)
            {
                fields.Add(new Field(header, FieldTypeParser.InferType(csv.GetField(i))));
                i++;
            }
        }
        else
        {
            csv.Read();

            for (int i = 0; i < csv.ColumnCount-1; i++)
            {
                fields.Add(new Field($"field_{i}", FieldTypeParser.InferType(csv.GetField(i))));
            }
        }

        return new Schema(fields);
    }

    /// <summary>
    /// Reads the projection for the schema.
    /// </summary>
    /// <param name="projection">Projection list.</param>
    /// <returns>Schema</returns>
    public Schema ReadProjection(List<string> projection)
    {
        List<string> readProjection = projection ?? [];
        Schema schema;

        if (readProjection.Count != 0)
        {
            schema = Schema.Select(readProjection);
        }
        else
        {
            schema = Schema;
        }

        return schema;
    }

    /// <summary>
    /// Scans the datasource for the specified projection, and returns a batch-size list of record batches.
    /// </summary>
    /// <param name="projection">Projection list.</param>
    /// <returns>RecordBatch enumerator.</returns>
    public IEnumerable<RecordBatch> Scan(List<string>? projection)
    {
        ValidateFile();
        
        Schema projSchema = ReadProjection(projection ?? []);

        List<IColumnVector> columnVectors = [];
        IEnumerable<RecordBatch> recordBatches = [];
        VectorFactory vectorFactory;

        var csv = CreateCSVReader();
        int batchCounter = 0;

        csv.Read();

        if (HasHeaders)
        {
            csv.ReadHeader();
        }

        while (csv.Read())
        {
            if (batchCounter == BatchSize-1)
            {
                recordBatches = recordBatches.Append(new RecordBatch(projSchema, [.. columnVectors]));
                columnVectors.Clear();
                batchCounter = 0;
            }

            if (batchCounter == 0)
            {
                foreach (Field field in projSchema.Fields)
                {
                    vectorFactory = new(field);
                    columnVectors.Add(vectorFactory.CreateFieldVector());
                }
            }

            int i = 0;

            foreach (Field field in projSchema.Fields)
            {
                columnVectors[i].AddValue(csv.GetField(i) ?? "");
                i++;
            }

            batchCounter++;
        }

        recordBatches = recordBatches.Append(new RecordBatch(projSchema, [.. columnVectors]));

        return recordBatches;
    }
}