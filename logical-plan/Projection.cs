using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Logical plan for computing a new column from an expression.
/// Corresponds to SQL SELECT cause.
/// </summary>
/// <param name="input">ILogicalPlan</param>
/// <param name="expr">List of ILogicalExpr</param>
public class Projection(ILogicalPlan input, List<ILogicalExpr> expr) : ILogicalPlan
{
    public Schema Schema()
    {
        return input.Schema();
    }

    public List<ILogicalPlan> Children()
    {
        return input.Children();
    }

    public override string ToString()
    {
        return $"Projection: {string.Join(", ", expr.Select(e => e.ToString()))}";
    }
}