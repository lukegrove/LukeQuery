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
    public Schema Schema();

    /// <summary>
    /// Returns the children (inputs) of the plan.
    /// Used for the visitor pattern to walk down the tree.
    /// A scan has no children (reads from DS), a filter has one (input), a join has two (left, right).
    /// </summary>
    /// <returns>List of children</returns>
    public List<ILogicalPlan> Children();
    
    /// <summary>
    /// Returns a pretty version of the plan.
    /// </summary>
    /// <returns>A very pretty string.</returns>
    public string Pretty()
    {
        return Format(this);
    }

    /// <summary>
    /// Formats the logical plan into a readable form.
    /// </summary>
    /// <param name="plan">Input plan.</param>
    /// <param name="indent">The amount of indentation.</param>
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