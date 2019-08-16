using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x30)]
    public unsafe struct ClassData
    {
        public uint Name;
        public byte Move;
        public MoveTypeEnum MoveType;
        public byte Defense;
        public Attack Attacks;

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x03)]
        public struct Attack
        {
            public AttackEnum primaryAttack;
            public AttackEnum secondaryAttack;
            public AttackEnum secondaryAttack2; // I think this is the critical attack.

            public AttackEnum PrimaryAttack => primaryAttack;
            public AttackEnum SecondaryAttack => secondaryAttack;
            public AttackEnum SecondaryAttack2 => secondaryAttack2;

            public AttackEnum this[int index]
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

        public SpecialAttack SpecialAttacks;

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x02)]
        public struct SpecialAttack
        {
            public SpecialAttacksEnum firstAttack;
            public SpecialAttacksEnum secondAttack;

            public SpecialAttacksEnum FirstAttack => firstAttack;
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

        public MagicEnum Spells;
        public Skill Skills;

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x02)]
        public struct Skill
        {
            public FighterSkillEnum firstSkill;
            public FighterSkillEnum secondSill;

            public FighterSkillEnum FirstSkill => firstSkill;
            public FighterSkillEnum SecondSkill => secondSill;

            public FighterSkillEnum this[int index]
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

        public ElementEnum Element;
        public byte Add_HP;
        public byte Add_MP;
        public byte Focus;
        public byte Star;

        public ushort ExpRequiredToLvl;

        //I think this may be sprite, animations and/or object references.
        public Unknown Unknowns;

        public unsafe struct Unknown
        {
            public fixed uint unknown[5];

            public uint this[int index]
            {
                get => unknown[index];
                set => unknown[index] = value;
            }

            public uint[] ToArray()
            {
                var dest = new uint[5];
                for (int i = 0; i < dest.Length; i++)
                {
                    dest[i] = unknown[i];
                }

                return dest;
            }
        }
    }
}