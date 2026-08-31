using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Wrapper for a logical plan that creates new node plans on each method call.
/// </summary>
public class DataFrameImpl(ILogicalPlan plan) : IDataFrame
{
    public IDataFrame Project(List<ILogicalExpr> expr)
    {
        return new DataFrameImpl(new Projection(plan, expr));
    }

    public IDataFrame Filter(ILogicalExpr expr)
    {
        return new DataFrameImpl(new Selection(plan, expr));
    }

    public IDataFrame Aggregate(List<ILogicalExpr> groupBy, List<AggregateExpr> aggregateExpr)
    {
        return new DataFrameImpl(new Aggregate(plan, groupBy, aggregateExpr));
    }

    public IDataFrame Limit(int n)
    {
        return new DataFrameImpl(new Limit(plan, n));
    }

    public IDataFrame Join(IDataFrame right, JoinType joinType, List<Tuple<string,string>> on)
    {
        return new DataFrameImpl(new Join(plan, right.LogicalPlan(), joinType, on));
    }

    public Schema Schema()
    {
        return plan.Schema();
    }

    public ILogicalPlan LogicalPlan()
    {
        return plan;
    }
}