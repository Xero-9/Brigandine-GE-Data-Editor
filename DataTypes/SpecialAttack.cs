using System;
using System.Runtime.InteropServices;
using Memory_Map_Builder.Helper_Classes;

namespace Memory_Map_Builder.DataTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct SpecialAttack
    {
        public uint    Name;
        public uint    Description;
        public byte    MP;
        public byte    Range;
        public byte    Damage;
        public byte    Unk1;
        public Element Element;
        public byte    GroundOrSky;
        public byte    Unk2;
        public byte    Unk3;
        public byte    Unk4;
        public byte    Unk5;
        public byte    Unk6;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public unsafe struct unsafeSpecialAttack
    {
        public       uint    Name;
        public       uint    Description;
        public       byte    MP;
        public       byte    Range;
        public       byte    Damage;
        public       byte    Unk1;
        public       Element Element;
        public       byte    GroundOrSky;
        public fixed byte    Unknown[5];

        public static implicit operator SpecialAttack(unsafeSpecialAttack specialAttack)
        {
            return *(SpecialAttack*) &specialAttack;
        }
    }
}
