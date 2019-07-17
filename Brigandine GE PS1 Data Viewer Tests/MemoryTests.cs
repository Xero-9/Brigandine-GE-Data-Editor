using System;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using Memory_Map_Builder;
using Memory_Map_Builder.DataTypes;
using Memory_Map_Builder.Enums;
using Memory_Map_Builder.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memory_Map_Builder_Tests
{
    public static class BrigandineMemoryMapBuilder
    {
        public static MemoryMappedFile BrigandineAsMemoryMappedFile { get; private set; }

        static BrigandineMemoryMapBuilder()
        {
            BuildMemoryMapFromResourceFile();
        }

        private static void BuildMemoryMapFromResourceFile()
        {
            using (var stream = GetResourceStream("SLPS_026"))
            {
                //var tempFile = Path.GetTempFileName();
                var memoryMappedFile =
                    MemoryMappedFile.CreateOrOpen("BrigandineDataFile", stream.Length);
                memoryMappedFile.CreateViewAccessor().WriteArray(0, new byte[stream.Length], 0, (int)stream.Length);
                var mapStream = memoryMappedFile.CreateViewStream();
                stream.CopyToAsync(mapStream).ConfigureAwait(true).GetAwaiter().GetResult();
                mapStream.FlushAsync().ConfigureAwait(true).GetAwaiter().GetResult();
                mapStream.Dispose();
                BrigandineAsMemoryMappedFile = memoryMappedFile;
            }
        }
        private static UnmanagedMemoryStream GetResourceStream(String name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string[ ] resources = assembly.GetManifestResourceNames();
            string resource =
                resources.SingleOrDefault(r => r.EndsWith(name, StringComparison.CurrentCultureIgnoreCase));

            if (resource == null)
            {
                throw new ArgumentException("The resource does not exist.",
                                                   "name");
            }
            return (UnmanagedMemoryStream) assembly.GetManifestResourceStream(resource);
        }
    }
    [TestClass]
    public class MemoryTests
    {

        public MemoryAccessor MemoryAccessor = MemoryAccessor.CreateAccessor(BrigandineMemoryMapBuilder.BrigandineAsMemoryMappedFile);

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
            Assert.AreEqual(expectedString, MemoryAccessor.DereferenceString(namePtr));
        }
        [TestMethod]
        public void ConstructorAndPropertiesInitializeProperlyTest()
        {
            Assert.AreEqual(MemoryAddresses.AttackType.Length, MemoryAccessor.AttackDatas.Length);
            Assert.AreEqual(MemoryAddresses.Castle.Length, MemoryAccessor.Castles.Length);
            Assert.AreEqual(MemoryAddresses.DefaultKnight.Length, MemoryAccessor.DefaultKnights.Length);
            Assert.AreEqual(MemoryAddresses.FighterDefault.Length, MemoryAccessor.FighterDefaults.Length);
            Assert.AreEqual(MemoryAddresses.Item.Length, MemoryAccessor.ItemsData.Length);
            Assert.AreEqual(MemoryAddresses.SpecialAttack.Length, MemoryAccessor.SpecialAttacks.Length);
            Assert.AreEqual(MemoryAddresses.Spell.Length, MemoryAccessor.Spells.Length);
            Assert.AreEqual(MemoryAddresses.Skill.Length, MemoryAccessor.SkillsData.Length);
#if DEBUG_MEMORY_STATGROWTH_BROKEN
            Assert.AreEqual(MemoryAddresses.StatGrowths.Length, memoryAccessor.StatGrowths.Length);
            Assert.AreNotEqual(0, MemoryAccessor.StatGrowths.HPGrowth);☺
#endif
            Assert.AreEqual("Full Blade", MemoryAccessor.DereferenceString(MemoryAccessor.AttackDatas[0].Name));
            Assert.AreEqual("Hervery", MemoryAccessor.DereferenceString(MemoryAccessor.Castles[0].Name));
            Assert.AreEqual(1, MemoryAccessor.DefaultKnights[0].Level);
            Assert.AreEqual("Fighter", MemoryAccessor.DereferenceString((uint)MemoryAccessor.FighterDefaults[0].Name));
            Assert.AreEqual("Solomon's Ring", MemoryAccessor.DereferenceString((uint) MemoryAccessor.ItemsData[0].Name));
            Assert.AreEqual("Howl Fire", MemoryAccessor.DereferenceString((uint) MemoryAccessor.SpecialAttacks[0].Name));
            Assert.AreEqual("Heal", MemoryAccessor.DereferenceString((uint) MemoryAccessor.Spells[0].Name));
            Assert.AreEqual("Nimble", MemoryAccessor.DereferenceString((uint)MemoryAccessor.SkillsData[0].Name));
        }

        [TestMethod]
        public void CastFromUnsafeStructsTest()
        {
            CastleData firstCastleData = MemoryAccessor.Castles[0];
            Assert.AreEqual("Hervery", MemoryAccessor.DereferenceString(firstCastleData.Name));

            DefaultKnightData firstDefaultKnight = MemoryAccessor.DefaultKnights[0];
            Assert.AreEqual(1, firstDefaultKnight.Level);

            ClassData firstClassData = MemoryAccessor.FighterDefaults[0];
            Assert.AreEqual("Fighter", MemoryAccessor.DereferenceString(firstClassData.Name));

            SpecialAttackData firstSpecialAttackData = MemoryAccessor.SpecialAttacks[0];
            Assert.AreEqual("Howl Fire", MemoryAccessor.DereferenceString(firstSpecialAttackData.Name));

            SpellData firstSpellData = MemoryAccessor.Spells[0];
            Assert.AreEqual("Heal", MemoryAccessor.DereferenceString((uint) firstSpellData.Name));
        }
    }
}
