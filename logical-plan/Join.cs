using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

enum JoinType    
{
    Inner,
    Left,
    Right
}
/*
public class Join(ILogicalPlan left, ILogicalPlan right, JoinType joinType, List<Tuple<string, string>> on) : ILogicalPlan
{
    public Schema Schema()
    {

        return new Schema(fields);
    }

    public List<ILogicalPlan> Children()
    {
        List<ILogicalPlan> children = (List<ILogicalPlan>)left.Children().Union(right.Children());
        return children;
    }

    public override string ToString()
    {
        return $"Join: type={joinType}, on={on}";
    }
}
*/