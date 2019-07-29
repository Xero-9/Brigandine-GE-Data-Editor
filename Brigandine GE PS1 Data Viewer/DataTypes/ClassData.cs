using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Memory_Map_Builder.Enums;

namespace Memory_Map_Builder.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x30)]
    public struct ClassData
    {
        public uint           Name;
        public byte           Move;
        public MoveType       MoveType;
        public byte           Def;
        public Attack         MainAtk;
        public Attack         SecondaryAtk_1;
        public Attack         SecondaryAtk_2;
        public SpecialAttacks SpecialAtk_1;
        public SpecialAttacks SpecialAtk_2;
        public byte           MagicWB;
        public byte           MagicBR;
        public byte           MagicRB;
        public byte           MagicG;
        public FighterSkill   Skill_1;
        public FighterSkill   SKill_2;
        public Element        Element;
        public byte           Add_HP;
        public byte           Add_MP;
        public byte           Focus;
        public byte           Star;
        public ushort         ExpRequiredToLvl;
        public uint           Unknown;
        public uint           Unknown1;
        public uint           Unknown2;
        public uint           Unknown3;
        public uint           Unknown4;
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x30)]
    public unsafe struct unsafeClassData
    {
        public uint     Name;
        public byte     Move;
        public MoveType MoveType;
        public byte     Defense;
        // [0] = Main Attack; [1] = First Secondary Attack; [2] = Second Secondary Attack;
        public fixed byte    Attacks[3];
        public fixed byte    SpecialAttacks[2];
        public       Magic   Magic;
        public fixed byte    FighterSkills[2];
        public       Element Element;
        public       byte    Add_HP;
        public       byte    Add_MP;
        public       byte    Focus;
        public       byte    Star;
        public       ushort  ExpRequiredToLvl;
        //I think this may be sprite, animations and/or object references.
        public fixed uint Unkown[5];

        public static implicit operator ClassData(unsafeClassData @class)
        {
            return *(ClassData*) &@class;
        }
    }
}