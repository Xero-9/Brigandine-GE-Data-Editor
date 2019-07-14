using System;
using System.Runtime.InteropServices;
using Memory_Map_Builder.Helper_Classes;

namespace Memory_Map_Builder.DataTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct Spell
    {
        public uint    Name;
        public uint    Description;
        public ushort  MPCost;
        public byte    Range;
        public byte    Damage;
        public Element Element;
        public byte    GRandSK;
        public byte    Unk1;
        public byte    AOE; // Most Likely Not AoE
        public byte    Unk2;
        public byte    Unk3;
        public byte    Unk4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public unsafe struct unsafeSpell
    {
        public       uint    Name;
        public       uint    Description;
        public       ushort  MPCost;
        public       byte    Range;
        public       byte    Damage;
        public       Element Element;
        public       byte    GRandSK;
        public       byte    Unk1;
        public       byte    AOE; // Most Likely Not AoE
        public fixed byte    Unknown[3];

        public static implicit operator Spell(unsafeSpell spell)
        {
            return *(Spell*) &spell;
        }
    }
}
