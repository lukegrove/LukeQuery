using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Helper enum for SQL join types.
/// </summary>
public enum JoinType    
{
    Inner,
    Left,
    Right
}

/// <summary>
/// Logical operation for combining rows from inputs.
/// Corresponds to SQL JOIN clause.
/// </summary>
/// <param name="left">Left plan.</param>
/// <param name="right">Right plan.</param>
/// <param name="joinType">Type of join (inner/left/right).</param>
/// <param name="on">Index to join on.</param>
public class Join(ILogicalPlan left, ILogicalPlan right, JoinType joinType, List<Tuple<string, string>> on) : ILogicalPlan
{
    public Schema Schema()
    {
        var duplicateKeys = on.Where(it => it.Item1 == it.Item2).Select(it => it.Item1).ToHashSet();
        
        List<Field> fields;
        
        switch (joinType)
        {
            case JoinType.Inner:
            case JoinType.Left:
                var leftFields = left.Schema().Fields;
                var rightFields = right.Schema().Fields.Where(f => !duplicateKeys.Contains(f.Name)).ToList();
                fields = leftFields.Concat(rightFields).ToList();
                break;
            case JoinType.Right:
                var leftFieldsRight = left.Schema().Fields.Where(f => !duplicateKeys.Contains(f.Name)).ToList();
                var rightFieldsRight = right.Schema().Fields;
                fields = leftFieldsRight.Concat(rightFieldsRight).ToList();
                break;
            default:
                throw new ArgumentException("Unknown join type");
        }

        return new Schema(fields);
    }

    /// <summary>
    /// Returns a list of the children from both plans.
    /// </summary>
    /// <returns>List of children.</returns>
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