using System;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    // Work In Progress.
    [Serializable]
    public struct MonsterData
    {
        public CountryEnum Country;
        public byte MonsterSlotNumber;
        public byte Class;
        public byte Level;
        public ushort Hp;
        public ushort Mp;
        public byte Str;
        public byte Int;
        public byte Agi;
        public byte ItemEquipped;
        public Text Name;
    }
}