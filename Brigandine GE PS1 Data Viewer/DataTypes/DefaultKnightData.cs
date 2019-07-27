using System;
using System.Runtime.InteropServices;

namespace Memory_Map_Builder.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x28)]
    public struct DefaultKnightData
    {
        public uint   Name;
        public byte   Class;
        public byte   Level;
        public ushort XP;
        public ushort HP;
        public ushort MP;
        public byte   STR;
        public byte   INT;
        public byte   AGI;
        public byte   RunePwrGrowth_RuneArea;
        public ushort RunePwr;
        public byte   Monster_1;
        public byte   Monster_2;
        public byte   Monster_3;
        public byte   Monster_4;
        public byte   Monster_5;
        public byte   Monster_6;
        public byte   Monster_7;
        public byte   StartingClass;
        public byte   Weapon;
        public byte   Item;
        public int    Unk;
        public byte   Team;
        public byte   Town;
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x28)]
    public unsafe struct unsafeDefaultKnightData
    {
        public       uint   Name;
        public       byte   Class;
        public       byte   Level;
        public       ushort XP;
        public       ushort HP;
        public       ushort MP;
        public       byte   STR;
        public       byte   INT;
        public       byte   AGI;
        public       byte   RunePwrGrowth_RuneArea;
        public       ushort RunePwr;
        public fixed byte   Monsters[7];
        public       byte   StartingClass;
        public       byte   Weapon;
        public       byte   Item;
        public       int    Unk;
        public       byte   Team;
        public       byte   Town;

        public static implicit operator DefaultKnightData(unsafeDefaultKnightData defaultKnight)
        {
            return *(DefaultKnightData*) &defaultKnight;
        }
    }
}
