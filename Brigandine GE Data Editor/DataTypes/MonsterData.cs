using System;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    // Work In Progress.
    [Serializable]
    public struct MonsterData
    {
        public OwnerEnum Owner;
        public byte MonsterSlotNumber;
        public byte Class;
        public byte Level;
        public ushort HP;
        public ushort MP;
        public byte STR;
        public byte INT;
        public byte AGI;
        public byte ItemEquipped;
        public uint Name;
    }
}