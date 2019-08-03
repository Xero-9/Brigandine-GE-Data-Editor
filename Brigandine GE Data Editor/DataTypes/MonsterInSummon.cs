using System;
using System.Runtime.InteropServices;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 10)]
    public struct MonsterInSummonData
    {
        public uint   Name;
        public byte   Level;
        public byte   Exp;
        public ushort BaseHP;
        public ushort BaseMP;
        public byte   Str;
        public byte   Int;
        public byte   Hit;
        public byte   RuneCost;
        public ushort ManaCost;
    }
}
