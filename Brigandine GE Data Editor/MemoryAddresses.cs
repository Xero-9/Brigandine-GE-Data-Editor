using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Brigandine GE Data Editor Tests")]
namespace BrigandineGEDataEditor
{
    /// <summary>
    /// Contains the address, sizeof and length for each data type.
    /// </summary>
    internal static class MemoryAddresses
    {
        //public static int GetLength(this DataTypes.AttackData _) => AttackData.Length;
        //public static int GetAddress(this DataTypes.AttackData _) => AttackData.Address;
        public static class Attacks
        {
            public static int Address     => 0x8BCC0;
            public static int SizeOf      => 0x14;
            public static int Length => 148;
        }
        //public static int GetLength(this ItemData _) => Item.Length;
        //public static int GetAddress(this ItemData _) => Item.Address;
        public static class Items
        {
            public static int Address     => 0x724BC;
            public static int SizeOf      => 0x13;
            public static int Length => 136;
        }
        //public static int GetLength(this MonsterInSummonData _) => Item.Length;
        //public static int GetAddress(this MonsterInSummonData _) => Item.Address;
        public static class MonsterInSummon
        {
            public static int Address     => 0x8B6F0;
            public static int SizeOf      => 16;
            public static int Length => 50;
        }

        //public static int GetLength(this CastleData _) => Castle.Length;
        //public static int GetAddress(this CastleData _) => Castle.Address;
        public static class Castles
        {
            public static int Address     => 0x86F60;
            public static int SizeOf      => 0x1C;
            public static int Length => 42;
        }
        //public static int GetLength(this DefaultKnightData _) => DefaultKnight.Length;
        //public static int GetAddress(this DefaultKnightData _) => DefaultKnight.Address;
        public static class DefaultKnights
        {
            public static int Address     => 0x87BB0;
            public static int SizeOf      => 0x28;
            public static int Length => 115;
        }
        //public static int GetLength(this ClassData _) => FighterDefault.Length;
        //public static int GetAddress(this ClassData _) => FighterDefault.Address;
        public static class FighterDefaults
        {
            public static int Address     => 0x8A1EC;
            public static int SizeOf      => 0x30;
            public static int Length => 112;
        }
        //public static int GetLength(this SpecialAttackData _) => SpecialAttack.Length;
        //public static int GetAddress(this SpecialAttackData _) => SpecialAttack.Address;
        public static class SpecialAttacks
        {
            public static int Address     => 0x8C850;
            public static int SizeOf      => 0x14;
            public static int Length => 26;
        }
        //public static int GetLength(this SpellData _) => Spell.Length;
        //public static int GetAddress(this SpellData _) => Spell.Address;

        public static class Spells
        {
            public static int Address     => 0x8CABC;
            public static int SizeOf      => 0x14;
            public static int Length => 30;
        }
        //public static int GetLength(this SkillData _) => Skill.Length;
        //public static int GetAddress(this SkillData _) => Skill.Address;
        public static class Skills
        {
            public static int Address     => 0x8CD14;
            public static int Sizeof      => 0x08;
            public static int Length => 21;
        }
#if WORK_IN_PROGRESS
        //public static int GetLength(this StatGrowthData _) => StatGrowth.Length;
        //public static int GetAddress(this StatGrowthData _) => StatGrowth.Address;
        public static class StatGrowth
        {
            public static int Address     => 0x8CDB4;
            public static int SizeOf      => 0x05;
            public static int Length => 112;
        }
        
        // Not sure this is actually in the SLPS_026.
        //public static int GetLength(this MonsterData _) => Monsters.Length;
        //public static int GetAddress(this MonsterData _) => Monsters.Address;
        public static class Monsters
        {
            public static int Address     => 0x8CDB4;//0x001A8CEC// default knight diff 0x8A6F8
            public static int SizeOf      => 0x05;
            public static int Length => 112;
        }
#endif
    }
}