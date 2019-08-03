using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x03)]
    public struct Attacks
    {
        public Attack primaryAttack;
        public Attack secondaryAttack;
        public Attack secondaryAttack2; // I think this is the critical attack.

        public Attack PrimaryAttack => primaryAttack;
        public Attack SecondaryAttack => secondaryAttack;
        public Attack SecondaryAttack2 => secondaryAttack2;

        public Attack this[int index]
        {
            get
            {
                switch (Math.Min(Math.Max(index, 0), 2))
                {
                    case 0:
                        return PrimaryAttack;
                    case 1:
                        return SecondaryAttack;
                    case 2:
                        return SecondaryAttack2;
                    default:
                        return PrimaryAttack;
                }
            }
            set
            {
                switch (Math.Min(Math.Max(index, 0), 2))
                {
                    case 0:
                        primaryAttack = value;
                        break;
                    case 1:
                        secondaryAttack = value;
                        break;
                    case 2:
                        secondaryAttack2 = value;
                        break;
                }
            }
        }
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x02)]
    public struct Skills
    {
        public FighterSkill firstSkill;
        public FighterSkill secondSill;
        
        public FighterSkill FirstSkill => firstSkill;
        public FighterSkill SecondSkill => secondSill; 
        
        public FighterSkill this[int index]
        {
            get
            {
                switch (Math.Min(Math.Max(index, 0), 1))
                {
                    case 0:
                        return FirstSkill;
                    case 1:
                        return SecondSkill;
                    default:
                        return FirstSkill;
                }
            }
            set
            {
                switch (Math.Min(Math.Max(index, 0), 1))
                {
                    case 0:
                        firstSkill = value;
                        break;
                    case 1:
                        secondSill = value;
                        break;
                }
            }
        }
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x02)]
    public struct SpecialAttacks
    {
        public SpecialAttacksEnum firstAttack;
        public SpecialAttacksEnum secondAttack;
        
        public SpecialAttacksEnum FirstAttack  => firstAttack;
        public SpecialAttacksEnum SecondAttack => secondAttack;

        public SpecialAttacksEnum this[int index]
        {
            get
            {
                switch (Math.Min(Math.Max(index, 0), 1))
                {
                    case 0:
                        return FirstAttack;
                    case 1:
                        return SecondAttack;
                    default:
                        return FirstAttack;
                }
            }
            set
            {
                switch (Math.Min(Math.Max(index, 0), 1))
                {
                    case 0:
                        firstAttack = value;
                        break;
                    case 1:
                        secondAttack = value;
                        break;
                }
            }
        }
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x30)]
    public struct ClassData
    {
        public uint           Name;
        public byte           Move;
        public MoveType       MoveType;
        public byte Defense;
        public Attacks Attacks;
        public SpecialAttacks SpecialAttacks;
        public byte           MagicWB;
        public byte           MagicBR;
        public byte           MagicRB;
        public byte           MagicG;
        public Skills Skills;
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
        public uint           Name;
        public byte           Move;
        public MoveType       MoveType;
        public byte           Defense;
        public Attacks        Attacks;
        public SpecialAttacks SpecialAttacks;
        public byte           MagicWB;
        public byte           MagicBR;
        public byte           MagicRB;
        public byte           MagicG;
        public Skills         Skills;
        public Element        Element;
        public byte           Add_HP;
        public byte           Add_MP;
        public byte           Focus;
        public byte           Star;
        public ushort         ExpRequiredToLvl;
        //I think this may be sprite, animations and/or object references.
        public fixed uint Unkown[5];

        public static implicit operator ClassData(unsafeClassData @class)
        {
            return *(ClassData*) &@class;
        }
    }
}