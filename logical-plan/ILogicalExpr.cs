using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Used to capture metadata for expressions.
/// </summary>
public interface ILogicalExpr
{
    /// <summary>
    /// Returns name and data type of the expression's output
    /// </summary>
    /// <param name="plan">ILogicalPlan</param>
    /// <returns>Field</returns>
    public abstract Field ToField(ILogicalPlan plan);
}