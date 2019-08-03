using System;
using System.Runtime.InteropServices;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x28)]
    public struct DefaultKnightData
    {
        public TextData   Name;
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

        public unsafe struct Monster
        {
            public fixed byte monsters[7];

            public byte this[int index]
            {
                get => monsters[index];
                set => monsters[index] = value;
            }
        }

        public Monster Monsters;
        public byte   StartingClass;
        public byte   Weapon;
        public byte   Item;
        public uint    Unk;
        public byte   Team;
        public byte   Town;
    }
}
