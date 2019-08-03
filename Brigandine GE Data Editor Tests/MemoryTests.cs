using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Memory_Map_Builder;
using Memory_Map_Builder.DataTypes;
using Memory_Map_Builder.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memory_Map_Builder_Tests
{
    [TestClass]
    public class MemoryTests
    {

        public MemoryAccessor memoryAccessor = MemoryAccessor.CreateAccessor(BrigandineMemoryMapBuilder.BrigandineAsMemoryMappedFile);

        [TestMethod]
        public void AdjustAddressTest()
        {
            //Base address is 0x8000F800;
            var testPtr = 0x801A1119;
            var expectedPtr = 0x191919;
            Assert.AreEqual(expectedPtr, MemoryAccessor.AdjustAddress(testPtr));
        }

        [TestMethod]
        public void DereferenceStringTest()
        {
            var namePtr =  2147572493;
            var expectedString = "Vampire Lord";
            Assert.AreEqual(expectedString, memoryAccessor.DereferenceString(namePtr));
        }
        [TestMethod]
        public void ConstructorAndPropertiesInitializeProperlyTest()
        {
            Assert.AreEqual(MemoryAddresses.Attacks.Length, memoryAccessor.AttackDatas.Length);
            Assert.AreEqual(MemoryAddresses.Castles.Length, memoryAccessor.Castles.Length);
            Assert.AreEqual(MemoryAddresses.DefaultKnights.Length, memoryAccessor.DefaultKnights.Length);
            Assert.AreEqual(MemoryAddresses.FighterDefaults.Length, memoryAccessor.FighterDefaults.Length);
            Assert.AreEqual(MemoryAddresses.Items.Length, memoryAccessor.ItemsData.Length);
            Assert.AreEqual(MemoryAddresses.SpecialAttacks.Length, memoryAccessor.SpecialAttacks.Length);
            Assert.AreEqual(MemoryAddresses.Spells.Length, memoryAccessor.Spells.Length);
            Assert.AreEqual(MemoryAddresses.Skills.Length, memoryAccessor.SkillsData.Length);
#if DEBUG_MEMORY_STATGROWTH_BROKEN
            Assert.AreEqual(MemoryAddresses.StatGrowths.Length, memoryAccessor.StatGrowths.Length);
            Assert.AreNotEqual(0, MemoryAccessor.StatGrowths.HPGrowth);☺
#endif
            Assert.AreEqual("Full Blade", memoryAccessor.DereferenceString(memoryAccessor.AttackDatas[0].Name));
            Assert.AreEqual("Hervery", memoryAccessor.DereferenceString(memoryAccessor.Castles[0].Name));
            Assert.AreEqual(1, memoryAccessor.DefaultKnights[0].Level);
            Assert.AreEqual("Fighter", memoryAccessor.DereferenceString((uint)memoryAccessor.FighterDefaults[0].Name));
            Assert.AreEqual("Solomon's Ring", memoryAccessor.DereferenceString((uint) memoryAccessor.ItemsData[0].Name));
            Assert.AreEqual("Howl Fire", memoryAccessor.DereferenceString((uint) memoryAccessor.SpecialAttacks[0].Name));
            Assert.AreEqual("Heal", memoryAccessor.DereferenceString((uint) memoryAccessor.Spells[0].Name));
            Assert.AreEqual("Nimble", memoryAccessor.DereferenceString((uint)memoryAccessor.SkillsData[0].Name));
        }

        [TestMethod]
        public void CastFromUnsafeStructsTest()
        {
            CastleData firstCastleData = memoryAccessor.Castles[0];
            Assert.AreEqual("Hervery", memoryAccessor.DereferenceString(firstCastleData.Name));

            DefaultKnightData firstDefaultKnight = memoryAccessor.DefaultKnights[0];
            Assert.AreEqual(1, firstDefaultKnight.Level);

            ClassData classData = memoryAccessor.FighterDefaults[0];
            Assert.AreEqual("Fighter", memoryAccessor.DereferenceString(classData.Name));

            SpecialAttackData firstSpecialAttackData = memoryAccessor.SpecialAttacks[0];
            Assert.AreEqual("Howl Fire", memoryAccessor.DereferenceString(firstSpecialAttackData.Name));

            SpellData firstSpellData = memoryAccessor.Spells[0];
            Assert.AreEqual("Heal", memoryAccessor.DereferenceString((uint) firstSpellData.Name));
        }
    }
}
