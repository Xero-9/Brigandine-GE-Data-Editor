using System.Runtime.InteropServices;

namespace Memory_Map_Builder.DataTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 5)]
    public struct ClassStatGrowth
    {
        public byte HPGrowth;
        public byte MPGrowth;
        public byte STRGrowth;
        public byte INTGrowth;
        public byte AGIGrowth;
    }
}
