using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Immutable wrapper for a logical plan to make plans easier to build.
/// Enables chaining.
/// </summary>
public interface IDataFrame
{
    /// <summary>
    /// Apply a projection to the query.
    /// </summary>
    /// <param name="expr">Expression.</param>
    /// <returns>IDataFrame</returns>
    public IDataFrame Project(List<ILogicalExpr> expr);

    /// <summary>
    /// Apply a filter to the query.
    /// </summary>
    /// <param name="expr">Expression.</param>
    /// <returns>IDataFrame</returns>
    public IDataFrame Filter(ILogicalExpr expr);

    /// <summary>
    /// Aggregate
    /// </summary>
    /// <param name="groupBy">Group by.</param>
    /// <param name="aggregateExpr">Aggregate.</param>
    /// <returns>IDataFrame</returns>
    public IDataFrame Aggregate(List<ILogicalExpr> groupBy, List<AggregateExpr> aggregateExpr);

    /// <summary>
    /// Limit the number of rows.
    /// </summary>
    /// <param name="n">Limit</param>
    /// <returns>IDataFrame</returns>
    public IDataFrame Limit(int n);

    /// <summary>
    /// Join with another DataFrame.
    /// </summary>
    /// <param name="right">Target DataFrame.</param>
    /// <param name="joinType">JoinType.</param>
    /// <param name="on">Index.</param>
    /// <returns>IDataFrame</returns>
    public IDataFrame Join(IDataFrame right, JoinType joinType, List<Tuple<string,string>> on);

    /// <summary>
    /// Returns schema of the DataFrame.
    /// </summary>
    /// <returns>Schema</returns>
    public Schema Schema();

    /// <summary>
    /// Returns the logical plan of the DataFrame.
    /// </summary>
    /// <returns>ILogicalPlan</returns>
    public ILogicalPlan LogicalPlan();
}