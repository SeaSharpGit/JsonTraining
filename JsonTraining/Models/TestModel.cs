namespace JT.Models
{
    public class TestModel
    {
        #region 值类型
        public short ShortTest { get; set; } = 123;
        public ushort UShortTest { get; set; } = 123;

        public int IntTest { get; set; } = 123;
        public uint UIntTest { get; set; } = 123U;

        public long LongTest { get; set; } = 123L;
        public ulong ULongTest { get; set; } = 123UL;

        public double DoubleTest { get; set; } = 1.23;
        public float FloatTest { get; set; } = 1.23F;
        public decimal DecimalTest { get; set; } = 1.23M;

        public Enum EnumTest { get; set; } = Enum.Second;
        #endregion

        public Struct StructTest { get; set; } = new Struct() { Name = "名字", Age = 123 };
        public string StringTest { get; set; } = "字符串";
        public object ObjectTest { get; set; } = new { Name = "对象" };

    }

    public struct Struct
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public enum Enum
    {
        First,
        Second
    }
}