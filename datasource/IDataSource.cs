using LukeQuery.Datatypes;

namespace LukeQuery.DataSource;

/// <summary>
/// Interface for data sources.
/// </summary>
interface IDataSource
{
    public Schema Schema()
    {
        throw new NotImplementedException();
    }

    // A projection here refers to a search for column names, as opposed to a filter for row values.
    public IEnumerable<RecordBatch> Scan(List<String> projection)
    {
        throw new NotImplementedException();
    }
}