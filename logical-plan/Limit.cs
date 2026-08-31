using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Logical plan for a limit.
/// </summary>
/// <param name="input">Input plan.</param>
/// <param name="limit">Limit.</param>
public class Limit(ILogicalPlan input, int limit) : ILogicalPlan
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
        return $"Limit: {limit}";
    }
}