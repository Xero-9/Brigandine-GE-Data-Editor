using System.Runtime.CompilerServices;
using Memory_Map_Builder.DataTypes;

[assembly: InternalsVisibleTo("Memory Map Builder Tests")]
namespace Memory_Map_Builder.Location
{
    /// <summary>
    /// Contains address, sizeof and length for each data type.
    /// </summary>
    internal static class MemoryAddresses
    {
        public static int GetLength(this DataTypes.AttackType _) => AttackType.Length;
        public static int GetAddress(this DataTypes.AttackType _) => AttackType.Address;
        public static class AttackType
        {
            public static int Address     => 0x8BCC0;
            public static int SizeOf      => 0x14;
            public static int Length => 148;
        }
        public static int GetLength(this DataTypes.Item _) => Item.Length;
        public static int GetAddress(this DataTypes.Item _) => Item.Address;
        public static class Item
        {
            public static int Address     => 0x724BC;
            public static int SizeOf      => 0x13;
            public static int Length => 136;
        }
        public static int GetLength(this DataTypes.Castle _) => Castle.Length;
        public static int GetAddress(this DataTypes.Castle _) => Castle.Address;
        public static class Castle
        {
            public static int Address     => 0x86F60;
            public static int SizeOf      => 0x1C;
            public static int Length => 42;
        }
        public static int GetLength(this DataTypes.DefaultKnight _) => DefaultKnight.Length;
        public static int GetAddress(this DataTypes.DefaultKnight _) => DefaultKnight.Address;
        public static class DefaultKnight
        {
            public static int Address     => 0x87BB0;
            public static int SizeOf      => 0x28;
            public static int Length => 115;
        }
        public static int GetLength(this DataTypes.FighterDefault _) => FighterDefault.Length;
        public static int GetAddress(this DataTypes.FighterDefault _) => FighterDefault.Address;
        public static class FighterDefault
        {
            public static int Address     => 0x8A1EC;
            public static int SizeOf      => 0x30;
            public static int Length => 112;
        }
        public static int GetLength(this DataTypes.SpecialAttack _) => SpecialAttack.Length;
        public static int GetAddress(this DataTypes.SpecialAttack _) => SpecialAttack.Address;
        public static class SpecialAttack
        {
            public static int Address     => 0x8C850;
            public static int SizeOf      => 0x14;
            public static int Length => 26;
        }
        public static int GetLength(this DataTypes.Spell _) => Spell.Length;
        public static int GetAddress(this DataTypes.Spell _) => Spell.Address;
        public static class Spell
        {
            public static int Address     => 0x8CABC;
            public static int SizeOf      => 0x14;
            public static int Length => 30;
        }
        public static int GetLength(this DataTypes.Skill _) => Skill.Length;
        public static int GetAddress(this DataTypes.Skill _) => Skill.Address;
        public static class Skill
        {
            public static int Address     => 0x8CD14;
            public static int Sizeof      => 0x08;
            public static int Length => 21;
        }
        public static int GetLength(this ClassStatGrowth _) => StatGrowth.Length;
        public static int GetAddress(this ClassStatGrowth _) => StatGrowth.Address;
        public static class StatGrowth
        {
            public static int Address     => 0x8CDB4;
            public static int SizeOf      => 0x05;
            public static int Length => 112;
        }
    }
}