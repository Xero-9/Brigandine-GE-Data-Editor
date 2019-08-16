using System.Runtime.CompilerServices;
using BrigandineGEDataEditor.DataTypes;

[assembly: InternalsVisibleTo("Brigandine GE Data Editor Tests")]

namespace BrigandineGEDataEditor
{
    public static class MemoryAddressDataTypeExtensions
    {
        public static int GetAddress(this AttackData _, int index) =>
            MemoryAddresses.Attacks.Address + (MemoryAddresses.Attacks.SizeOf * index);

        public static int GetAddress(this ItemData _, int index) =>
            MemoryAddresses.Items.Address + (MemoryAddresses.Items.SizeOf * index);

        public static int GetAddress(this MonsterInSummonData _, int index) =>
            MemoryAddresses.MonstersInSummon.Address + (MemoryAddresses.MonstersInSummon.SizeOf * index);

        public static int GetAddress(this CastleData _, int index) =>
            MemoryAddresses.Castles.Address + (MemoryAddresses.Castles.SizeOf * index);

        public static int GetAddress(this DefaultKnightData test, int index) =>
            MemoryAddresses.DefaultKnights.Address + (MemoryAddresses.DefaultKnights.SizeOf * index);

        public static int GetAddress(this ClassData _, int index) =>
            MemoryAddresses.Classes.Address + (MemoryAddresses.Classes.SizeOf * index);

        public static int GetAddress(this SpecialAttackData _, int index) =>
            MemoryAddresses.SpecialAttacks.Address + (MemoryAddresses.SpecialAttacks.SizeOf * index);

        public static int GetAddress(this SpellData _, int index) =>
            MemoryAddresses.Spells.Address + (MemoryAddresses.Spells.SizeOf * index);

        public static int GetAddress(this SkillData _, int index) =>
            MemoryAddresses.Skills.Address + (MemoryAddresses.Skills.SizeOf * index);
#if WORK_IN_PROGRESS
        //public static int GetAddress(this StatGrowthData _, int index) => MemoryAddresses.StatGrowths.Address + (MemoryAddresses.StatGrowths.SizeOf * index);
        //public static int GetAddress(this MonsterData _, int index) => MemoryAddresses.Monsters.Address + (MemoryAddresses.Monsters.SizeOf * index);
#endif
    }

    /// <summary>
    /// Contains the address, sizeof and length for each data type.
    /// </summary>
    internal static class MemoryAddresses
    {
        public static class Attacks
        {
            public static int Address => 0x8BCC0;
            public static int SizeOf => 0x14;
            public static int Length => 148;
        }

        public static class Items
        {
            public static int Address => 0x724BC;
            public static int SizeOf => 0x13;
            public static int Length => 136;
        }

        public static class MonstersInSummon
        {
            public static int Address => 0x8B6F0;
            public static int SizeOf => 16;
            public static int Length => 50;
        }

        public static class Castles
        {
            public static int Address => 0x86F60;
            public static int SizeOf => 0x1C;
            public static int Length => 42;
        }

        public static class DefaultKnights
        {
            public static int Address => 0x87BB0;
            public static int SizeOf => 0x28;
            public static int Length => 115;
        }

        public static class Classes
        {
            public static int Address => 0x8A1EC;
            public static int SizeOf => 0x30;
            public static int Length => 112;
        }

        public static class SpecialAttacks
        {
            public static int Address => 0x8C850;
            public static int SizeOf => 0x14;
            public static int Length => 26;
        }

        public static class Spells
        {
            public static int Address => 0x8CABC;
            public static int SizeOf => 0x14;
            public static int Length => 30;
        }


        public static class Skills
        {
            public static int Address => 0x8CD14;
            public static int SizeOf => 0x08;
            public static int Length => 21;
        }
#if WORK_IN_PROGRESS
        public static class StatGrowths
        {
            public static int Address     => 0x8CDB4;
            public static int SizeOf      => 0x05;
            public static int Length => 112;
        }

        // Not sure this is actually in the SLPS_026.
        public static class Monsters
        {
            public static int Address     => 0x8CDB4;
            public static int SizeOf      => 0x05;
            public static int Length => 112;
        }
#endif
    }
}