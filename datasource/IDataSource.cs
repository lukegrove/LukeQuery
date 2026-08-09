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

    public IEnumerable<RecordBatch> Scan(List<String> columns)
    {
        throw new NotImplementedException();
    }
}