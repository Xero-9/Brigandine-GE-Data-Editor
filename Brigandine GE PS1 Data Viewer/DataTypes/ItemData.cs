using System;
using System.Runtime.InteropServices;
using Memory_Map_Builder.Enums;
using Memory_Map_Builder.Helper_Classes;

namespace Memory_Map_Builder.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct ItemData
    {
        public uint           Name;
        public ItemTypeEnum   Type;
        public ItemEffectEnum Effect;
        public sbyte          Atk_Str;
        public sbyte          Int;
        public sbyte          Hit;
        public sbyte          Avoid_Agi;
        public sbyte          Def_ShieldBlock;
        public sbyte          HP;
        public sbyte          MP;
        public sbyte          MovUp;
        public sbyte          RunePwr_RuneCost;
        public sbyte          RuneArea;
        public Element        AtkElement;
        public Element        ResistElement;

    }
}
