using LukeQuery.Datatypes;
using LukeQuery.DataSource;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Logical plan for scanning a datasource.
/// Reads from the datasource. The leaf node in every query tree, where data enters the plan.
/// </summary>
/// <param name="path">Path for datasource.</param>
/// <param name="dataSource">Datasource.</param>
/// <param name="projection">List of columns to read. Reads all if empty.</param>
public class Scan(string path, IDataSource dataSource, List<string> projection): ILogicalPlan
{
    /// <summary>
    /// Returns the Schema.
    /// </summary>
    /// <returns>Schema</returns>
    public Schema Schema()
    {
        return DeriveSchema();
    }

    /// <summary>
    /// Derives the schema from the projection.
    /// </summary>
    /// <returns>Schema</returns>
    private Schema DeriveSchema()
    {
        Schema schema = dataSource.Schema();

        if (projection.Count == 0)
        {
            return schema;
        }

        return schema.Select(projection);
    }

    /// <summary>
    /// Returns an empty list of children.
    /// </summary>
    /// <returns>Empty list.</returns>
    public List<ILogicalPlan> Children()
    {
        return [];
    }

    public override string ToString()
    {
        if (projection.Count == 0)
        {
            return $"Scan: {path}; projection=None";
        }

        return $"Scan: {path}; projection=${projection}";
    }
}