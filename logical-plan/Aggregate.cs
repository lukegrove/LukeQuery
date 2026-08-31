using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Logical plan for an aggregate expression.
/// </summary>
/// <param name="input">ILogicalPlan</param>
/// <param name="groupExpr">List of ILogicalExpr</param>
/// <param name="aggregateExpr">List of AggregateExpr</param>
public class Aggregate(ILogicalPlan input, List<ILogicalExpr> groupExpr, List<AggregateExpr> aggregateExpr) : ILogicalPlan
{
    public Schema Schema()
    {
        // Groups columns first, e.g. DEPARTMENT, AVG in SELECT Department, AVG(salary) from Employyes
        // Combines two lists of fields
        List<Field> fields = groupExpr.Select(it => it.ToField(input)).Concat(aggregateExpr.Select(it => it.ToField(input))).ToList();
        return new Schema(fields);
    }

    public List<ILogicalPlan> Children()
    {
        return input.Children();
    }

    public override string ToString()
    {
        return $"Aggregate: groupexpr={groupExpr}, aggregateExpr={aggregateExpr}";
    }
}