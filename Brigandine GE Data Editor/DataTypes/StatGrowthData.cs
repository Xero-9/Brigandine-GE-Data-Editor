using System;
using System.Runtime.InteropServices;

namespace BrigandineGEDataEditor.DataTypes
{
    // Work In Progress.
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 5)]
    public struct StatGrowthData
    {
        public byte HPGrowth;
        public byte MPGrowth;
        public byte STRGrowth;
        public byte INTGrowth;
        public byte AGIGrowth;
    }
}
