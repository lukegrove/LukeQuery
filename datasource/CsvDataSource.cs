using LukeQuery.Datatypes;

namespace LukeQuery.DataSource;

class CsvDataSource(string filename, bool hasHeaders, int batchSize, Schema? schema = null) : IDataSource
{
    public string Filename = filename;
    public Schema Schema = schema ?? new Schema([]);
    private bool HasHeaders = hasHeaders;
    private int BatchSize = batchSize;

    //private Schema FinalSchema = new Schema(new List<Field>());
    // Need an infer schema method

    public IEnumerable<RecordBatch> Scan(List<String> columns)
    {
        // Look into projection logic

        return [];
    }
}