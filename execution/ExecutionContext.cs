using LukeQuery.DataSource;
using LukeQuery.LogicalPlan;

namespace LukeQuery.Execution;

/// <summary>
/// Starting point for buliding DataFrames.
/// Creates initial DataFrames from data sources.
/// </summary>
public class ExecutionContext()
{
    /// <summary>
    /// Constructs DataFrame for CSV data source.
    /// </summary>
    /// <param name="filename">File name.<param>
    /// <param name="hasHeaders">Has headers?</param>
    /// <param name="batchSize">Batch size.</param>
    /// <returns>IDataFrame</returns>
    public static IDataFrame CSV(string filename, bool hasHeaders, int batchSize)
    {
        return new DataFrameImpl(new Scan(filename, new CsvDataSource(filename, hasHeaders, batchSize), []));
    }
}