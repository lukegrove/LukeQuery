using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Logical plan that only keeps rows where an expression evaluates to TRUE.
/// Corresponds to SQL WHERE clause.
/// </summary>
/// <param name="input">ILogicalPlan</param>
/// <param name="expr">ILogicalExpr</param>
public class Selection(ILogicalPlan input, ILogicalExpr expr) : ILogicalPlan
{
    public Schema Schema()
    {
        return input.Schema();
    }

    /// <summary>
    /// Returns the input children.
    /// Selection does not change the schema of the input, as it only removes rows not columns.
    /// </summary>
    /// <returns>Input children.</returns>
    public List<ILogicalPlan> Children()
    {
        return input.Children();
    }

    public override string ToString()
    {
        return $"Selection {input}";
    }
}