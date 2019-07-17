//#define WORK_IN_PROGRESS
using System.Runtime.InteropServices;

namespace Memory_Map_Builder.DataTypes
{
#if WORK_IN_PROGRESS
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 5)]
    public struct StatGrowthData
    {
        public byte HPGrowth;
        public byte MPGrowth;
        public byte STRGrowth;
        public byte INTGrowth;
        public byte AGIGrowth;
    }
#endif
}
