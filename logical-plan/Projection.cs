using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Logical plan for computing a new column from an expression.
/// Corresponds to SQL SELECT cause.
/// </summary>
/// <param name="input">Input plan.</param>
/// <param name="expr">Expressions.</param>
public class Projection(ILogicalPlan input, List<ILogicalExpr> expr) : ILogicalPlan
{
    /// <summary>
    /// Returns the input schema.
    /// </summary>
    /// <returns>Schema</returns>
    public Schema Schema()
    {
        return input.Schema();
    }

    /// <summary>
    /// Returns the input children.
    /// </summary>
    /// <returns>Input children.</returns>
    public List<ILogicalPlan> Children()
    {
        return input.Children();
    }

    public override string ToString()
    {
        return $"Projection: {string.Join(", ", expr.Select(e => e.ToString()))}";
    }
}