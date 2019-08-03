using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct SpellData
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
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public unsafe struct unsafeSpellData
    {
        public       uint    Name;
        public       uint    Description;
        public       ushort  MPCost; 
        public       byte    Range; 
        public       byte    Damage;
        public       Element Element; 
        public       byte    GroundAndSky; 
        public       byte    Unknown1; 
        public       byte    AOE; // Most Likely Not AoE
        public fixed byte    Unknown[3];
        public static implicit operator SpellData(unsafeSpellData spell)
        {

            return *(SpellData*) &spell;
        }
    }
}
