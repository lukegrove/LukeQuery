using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Used to capture metadata for expressions.
/// </summary>
public interface ILogicalExpr
{
    /// <summary>
    /// Returns name and data type of the expression's output.
    /// </summary>
    /// <param name="input">Input plan</param>
    /// <returns>Field</returns>
    public Field ToField(ILogicalPlan input);
}