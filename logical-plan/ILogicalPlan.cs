using System.Text;
using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

/// <summary>
/// Represents a data transformation or action that returns a relation (set of tuples).
/// </summary>
public interface ILogicalPlan
{
    /// <summary>
    /// Returns schema of the dataset produced by the plan.
    /// </summary>
    /// <returns>Schema</returns>
    public Schema Schema()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the children (inputs) of the plan.
    /// Used for the visitor pattern to walk down the tree.
    /// A scan has no children (reads from DS), a filter has one (input), a join has two (left, right).
    /// </summary>
    /// <returns></returns>
    public List<ILogicalPlan> Children()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns a pretty version of the plan.
    /// </summary>
    /// <returns>string</returns>
    public string Pretty()
    {
        return Format(this);
    }

    // TODO: move Format to a different class ???

    /// <summary>
    /// Formats the logical plan into a readable form.
    /// </summary>
    /// <param name="plan">ILogicalPlan</param>
    /// <param name="indent">int</param>
    /// <returns>Formatted string</returns>
    public string Format(ILogicalPlan plan, int indent = 0)
    {
        StringBuilder stringBuilder = new();

        for (int i = 0; i < indent; i++)
        {
            stringBuilder.Append('\t');
        }

        stringBuilder.Append(plan.ToString()).Append('\n');;

        foreach (ILogicalPlan child in plan.Children())
        {
            stringBuilder.Append(Format(child, indent+1));
        }

        return stringBuilder.ToString();
    }
}