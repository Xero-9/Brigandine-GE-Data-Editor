using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct ItemData
    {
        public Text Name;
        public ItemTypeEnum Type;
        public ItemEffectEnum Effect;
        public byte AttackStr;
        public byte Int;
        public byte Hit;
        public byte Avoid_Agi;
        public byte Def_ShieldBlock;
        public byte HP;
        public byte MP;
        public byte MovUp;
        public byte RunePwr_RuneCost;
        public byte RuneArea;
        public ElementEnum AtkElement;
        public ElementEnum ResistElement;
    }
}