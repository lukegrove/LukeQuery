using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

public static class ArrowTypes
{
    public static readonly BooleanType BooleanType = Apache.Arrow.Types.BooleanType.Default;
    public static readonly Int8Type Int8Type = Apache.Arrow.Types.Int8Type.Default;
    public static readonly Int16Type Int16Type = Apache.Arrow.Types.Int16Type.Default;
    public static readonly Int32Type Int32Type = Apache.Arrow.Types.Int32Type.Default;
    public static readonly Int64Type Int64Type = Apache.Arrow.Types.Int64Type.Default;
    public static readonly UInt8Type UInt8Type = Apache.Arrow.Types.UInt8Type.Default;
    public static readonly UInt16Type UInt16Type = Apache.Arrow.Types.UInt16Type.Default;
    public static readonly UInt32Type UInt32Type = Apache.Arrow.Types.UInt32Type.Default;
    public static readonly UInt64Type UInt64Type = Apache.Arrow.Types.UInt64Type.Default;
    public static readonly FloatType FloatType = Apache.Arrow.Types.FloatType.Default;
    public static readonly DoubleType DoubleType = Apache.Arrow.Types.DoubleType.Default;
    public static readonly StringType StringType = Apache.Arrow.Types.StringType.Default;
    public static readonly BinaryType BinaryType = Apache.Arrow.Types.BinaryType.Default;
    public static readonly Date32Type DateDayType = Apache.Arrow.Types.Date32Type.Default;
    public static readonly IntervalType IntervalDayTimeType = Apache.Arrow.Types.IntervalType.DayTime;
}
