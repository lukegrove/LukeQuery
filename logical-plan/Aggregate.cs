using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Logical plan for an aggregate expression.
/// </summary>
/// <param name="input">Input plan</param>
/// <param name="groupExpr">Group expressions</param>
/// <param name="aggregateExpr">Aggregate expressions</param>
public class Aggregate(ILogicalPlan input, List<ILogicalExpr> groupExpr, List<AggregateExpr> aggregateExpr) : ILogicalPlan
{
    /// <summary>
    /// Returns the aggregated schema.
    /// Groups columns first, e.g. DEPARTMENT, AVG in SELECT Department, AVG(salary) from Employees.
    /// </summary>
    /// <returns>Schema</returns>
    public Schema Schema()
    {
        List<Field> fields = groupExpr.Select(it => it.ToField(input)).Concat(aggregateExpr.Select(it => it.ToField(input))).ToList();
        return new Schema(fields);
    }

    /// <summary>
    /// Returns the input children.
    /// </summary>
    /// <returns>Input children</returns>
    public List<ILogicalPlan> Children()
    {
        return input.Children();
    }

    public override string ToString()
    {
        return $"Aggregate: groupexpr={groupExpr}, aggregateExpr={aggregateExpr}";
    }
}