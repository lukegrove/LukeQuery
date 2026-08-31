using System.Data.SqlTypes;
using LukeQuery.Datatypes;

namespace LukeQuery.LogicalPlan;

// TODO: Add other literals Date/Interval/DateAdd/DateSubtract

/// <summary>
/// Expression representing a reference to a column by name.
/// </summary>
/// <param name="name">Column name.</param>
public class Column(string name) : ILogicalExpr
{
    private readonly string Name = name;

    public Field ToField(ILogicalPlan input)
    {
        foreach (Field field in input.Schema().Fields)
        {
            if (field.Name == Name)
            {
                return field;
            }
        }

        throw new SqlTypeException($"No column named {Name} in schema.");
    }

    public override string ToString()
    {
        return Name;
    }
}

/// <summary>
/// Expression representing a string literal.
/// Does not depend in input as the type is fixed.
/// </summary>
/// <param name="str">literal</param>
public class LiteralString(string literal) : ILogicalExpr
{
    private readonly string Literal = literal;

    public Field ToField(ILogicalPlan input)
    {
        return new Field(Literal, ArrowTypes.StringType);
    }

    public override string ToString()
    {
        return Literal;
    }
}

/// <summary>
/// Expression representing a literal long.
/// Does not depend on input as the type is fixed.
/// </summary>
/// <param name="long">literal</param>
public class LiteralLong(long literal) : ILogicalExpr
{
    private readonly long Literal = literal;

    public Field ToField(ILogicalPlan input)
    {
        return new Field(Literal.ToString(), ArrowTypes.Int64Type);
    }

    public override string ToString()
    {
        return Literal.ToString();
    }
}

/// <summary>
/// Expression representing a literal double.
/// Does not depend in input as the type is fixed.
/// </summary>
/// <param name="double">literal</param>
public class LiteralDouble(double literal) : ILogicalExpr
{
    private readonly double Literal = literal;

    public Field ToField(ILogicalPlan input)
    {
        return new Field(Literal.ToString(), ArrowTypes.Int64Type);
    }

    public override string ToString()
    {
        return Literal.ToString();
    }
}

/// <summary>
/// Shared structure for input comparison.
/// </summary>
/// <param name="name">Expression type.</param>
/// <param name="op">Operator symbol for printing.</param>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public abstract class BinaryExpr(string name, string op, ILogicalExpr left, ILogicalExpr right) : ILogicalExpr
{
    private readonly string Name = name;

    public override string ToString()
    {
        return $"{left} {op} {right}";
    }

    public Field ToField(ILogicalPlan input)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Shared structure type for boolean comparison.
/// </summary>
/// <param name="name">Expression type.</param>
/// <param name="op">Operator symbol for printing.</param>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public abstract class BooleanBinaryExpr(string name, string op, ILogicalExpr left, ILogicalExpr right) : BinaryExpr(name, op, left, right)
{
    private readonly string name = name;

    public new Field ToField(ILogicalPlan input)
    {
        return new Field(name, ArrowTypes.BooleanType);
    }
}

/// <summary>
/// Logical expression for an equality comparison.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Eq(ILogicalExpr left, ILogicalExpr right) : BooleanBinaryExpr("eq", "=", left, right) {}

/// <summary>
/// Logical expression for an inequality comparison.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Neq(ILogicalExpr left, ILogicalExpr right) : BooleanBinaryExpr("neq", "!=", left, right) {}

/// <summary>
/// Logical expression for a greater than comparison.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Gt(ILogicalExpr left, ILogicalExpr right) : BooleanBinaryExpr("gt", ">", left, right) {}

/// <summary>
/// Logical expression for a greater than or equals comparison.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class GtEq(ILogicalExpr left, ILogicalExpr right) : BooleanBinaryExpr("gteq", ">=", left, right) {}

/// <summary>
/// Logical expression for a less than comparison.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Lt(ILogicalExpr left, ILogicalExpr right) : BooleanBinaryExpr("lt", "<", left, right) {}

/// <summary>
/// Logical expression for a less than or equals comparison.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class LtEq(ILogicalExpr left, ILogicalExpr right) : BooleanBinaryExpr("lteq", "<=", left, right) {}

/// <summary>
/// Logical expression for a logical AND.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class And(ILogicalExpr left, ILogicalExpr right) : BooleanBinaryExpr("and", "AND", left, right) {}

/// <summary>
/// Logical expression for a logical OR.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Or(ILogicalExpr left, ILogicalExpr right) : BooleanBinaryExpr("or", "OR", left, right) {}

/// <summary>
/// Shared structure for mathematical operations.
/// </summary>
/// <param name="name">Expression type.</param>
/// <param name="op">Operator symbol for printing.</param>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public abstract class MathExpr(string name, string op, ILogicalExpr left, ILogicalExpr right) : BinaryExpr(name, op, left, right)
{
    private readonly string Name = name;
    private readonly ILogicalExpr Left = left;

    public new Field ToField(ILogicalPlan input)
    {
        return new Field(Name, Left.ToField(input).Type);
    }
}

/// <summary>
/// Logical expression for a mathematical addition.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Add(ILogicalExpr left, ILogicalExpr right) : MathExpr("add", "+", left, right) {}

/// <summary>
/// Logical expression for a mathematical subtraction.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Subtract(ILogicalExpr left, ILogicalExpr right) : MathExpr("subtract", "-", left, right) {}

/// <summary>
/// Logical expression for a mathematical multiplication.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Multiply(ILogicalExpr left, ILogicalExpr right) : MathExpr("mult", "*", left, right) {}

/// <summary>
/// Logical expression for a mathematical division.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Divide(ILogicalExpr left, ILogicalExpr right) : MathExpr("div", "/", left, right) {}

/// <summary>
/// Logical expression for a mathematical modulo.
/// </summary>
/// <param name="left">Left operand.</param>
/// <param name="right">Right operand.</param>
public class Modulus(ILogicalExpr left, ILogicalExpr right) : MathExpr("mod", "%", left, right) {}

/// <summary>
/// Base class for an aggregate expression that shares a type with the input expression.
/// </summary>
/// <param name="name">Expression type.</param>
/// <param name="expr">Input expression.</param>
public abstract class AggregateExpr(string name, ILogicalExpr expr) : ILogicalExpr
{
    public Field ToField(ILogicalPlan input)
    {
        return new Field(name, expr.ToField(input).Type);
    }

    public override string ToString()
    {
        return $"{name}({expr})";
    }
}

/// <summary>
/// Logical expression for an aggregate SUM.
/// </summary>
/// <param name="input">Input expression.</param>
public class Sum(ILogicalExpr input) : AggregateExpr("SUM", input) {}

/// <summary>
/// Logical expression for an aggregate MIN.
/// </summary>
/// <param name="input">Input expression.</param>
public class Min(ILogicalExpr input) : AggregateExpr("MIN", input) {}

/// <summary>
/// Logical expression for an aggregate MAX.
/// </summary>
/// <param name="input">Input expression.</param>
public class Max(ILogicalExpr input) : AggregateExpr("MAX", input) {}

/// <summary>
/// Logical expression for an aggregate AVG.
/// </summary>
/// <param name="input">Input expression.</param>
public class Avg(ILogicalExpr input) : AggregateExpr("AVG", input) {}

/// <summary>
/// Logical expression for an aggregate COUNT.
/// </summary>
/// <param name="input">Input expression.</param>
public class Count(ILogicalExpr input) : AggregateExpr("COUNT", input)
{
    private readonly ILogicalExpr Input = input;

    public new Field ToField(ILogicalPlan input)
    {
        return new Field("COUNT", ArrowTypes.Int32Type);
    }

    public override string ToString()
    {
        return $"COUNT({Input})";
    }
}

/// <summary>
/// Class for aliased expressions, changing the name but preserving the type.
/// </summary>
/// <param name="expr">Expression</param>
/// <param name="alias">Alias</param>
public class Alias(ILogicalExpr expr, string alias) : ILogicalExpr
{
    public override string ToString()
    {
        return $"{expr} AS {alias}";
    }

    public Field ToField(ILogicalPlan input)
    {
        return new Field(alias, expr.ToField(input).Type);
    }
}