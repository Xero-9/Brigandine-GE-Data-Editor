using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct SpellData
    {
        public TextData    Name;
        public TextData    Description;
        public ushort  MPCost;
        public byte    Range; 
        public byte    Damage;
        public Element Element;
        public byte    GRandSK;
        public byte    Unk1; 
        public byte    AOE; // Most Likely Not AoE
        public Unknown Unknowns;

        public unsafe struct Unknown
        {
            public fixed byte unknown[3];

            public byte this[int index]
            {
                get => unknown[index];
                set => unknown[index] = value;
            }
        }
    }
}
