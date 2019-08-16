//#define WORK_IN_PROGRESS

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using BrigandineGEDataEditor.DataTypes;

namespace BrigandineGEDataEditor
{
    /// <summary>
    /// Class for creating a view into a Brigandine Grand Edition data file that has been opened as a MemoryMappedFile.
    /// There are properties and methods for quick access and viewing of the internal data.
    /// The file loaded should be named SLPS_026.61 or SLPS_026.62 for the second disk.
    /// TODO 1. Improve functionality for loading the MemoryMappedFile from a string.
    /// TODO 2. Add functionality for altering and saving the data back to the file.
    /// </summary>
    public class MemoryAccessor
    {
        private static List<MemoryMappedFile> memoryMappedFilesToBeDisposed = new List<MemoryMappedFile>();
        public static void DisposeAllMappedFiles() => memoryMappedFilesToBeDisposed.ForEach(m => m.Dispose());

        public const int HeaderA = 0x4790;
        public const int HeaderB = 0xB070;
        public const long VirtualStartAddress = 0x80000000;

        /// <summary>
        /// Adjust the address from being a playstation memory location to it's file equivalent. This just
        /// means that I subtracted HeaderMinor, HeaderMajor and the VirtualStartAddress values from the address provided.
        /// </summary>
        /// <param name="address">Address to adjust for reading.</param>
        /// <returns></returns>
        public static long AdjustAddress(long address)
        {
            var adjustedAddress = (long) address - VirtualStartAddress;
            adjustedAddress -= (HeaderB + HeaderA);
            return adjustedAddress;
        }

        /// <summary>
        /// Creates an instance of the <seealso cref="MemoryAccessor"/> class.
        /// TODO: Create more factory methods to use things like strings and bin files directly.
        /// </summary>
        /// <param name="memoryMappedFile"></param>
        /// <returns>An instance of the accessor into the mapped file for easy access to the data structs.</returns>
        public static MemoryAccessor CreateAccessor(MemoryMappedFile memoryMappedFile)
        {
            var memoryAccessor = new MemoryAccessor(memoryMappedFile);
            if (memoryAccessor.IsOpen)
            {
                return memoryAccessor;
            }

            return null;
        }

        /// <summary>
        /// Creates an instance of the <seealso cref="MemoryAccessor"/> class.
        /// </summary>
        /// <param name="PathToFile">Path to the SLPS_026 file to read or write.</param>
        /// <returns>An instance of the accessor into the mapped file for easy access to the data structs.</returns>
        public static MemoryAccessor CreateAccessor(string PathToFile) =>
            CreateAccessor(MemoryMappedFile.CreateFromFile(PathToFile, FileMode.OpenOrCreate));

        private bool IsOpen { get; set; }
        public MemoryMappedFile MemoryMappedFile { get; private set; }

        private AttackData[] attacks;
        private CastleData[] castles;
        private DefaultKnightData[] defaultKnights;
        private ClassData[] classes;
        private ItemData[] items;
        private MonsterInSummonData[] monstersInSummon;
        private SpecialAttackData[] specialAttacks;
        private SpellData[] spells;
        private SkillData[] skills;
        private StatGrowthData[] statGrowths;

        private MemoryAccessor(MemoryMappedFile memoryMappedFile = null)
        {
            MemoryMappedFile = memoryMappedFile;
            using (var viewAccessor = MemoryMappedFile?.CreateViewAccessor())
            {
                var bytesCheck = new byte[] {0x50, 0x53, 0x2D, 0x58, 0x20, 0x45, 0x58, 0x45};
                byte[] bytes = new byte[8];
                if (viewAccessor.ReadArray(0, bytes, 0, 8) != 8)
                {
                    MemoryMappedFile.Dispose();
                    return;
                    throw new Exception("This file has the wrong header size returning early.");
                }

                for (int i = 0; i < bytesCheck.Length; i++)
                {
                    if (bytes[i] != bytesCheck[i])
                    {
                        MemoryMappedFile.Dispose();
                        return;
                        throw new Exception("This file has the wrong header returning early.");
                    }
                }

                memoryMappedFilesToBeDisposed.Add(MemoryMappedFile);
                IsOpen = true;
                GetData(viewAccessor);
            }
        }

        private void GetData(MemoryMappedViewAccessor viewAccessor)
        {
            attacks = new AttackData[MemoryAddresses.Attacks.Length];
            viewAccessor.ReadArray(MemoryAddresses.Attacks.Address, attacks, 0, MemoryAddresses.Attacks.Length);

            castles = new CastleData[MemoryAddresses.Castles.Length];
            viewAccessor.ReadArray(MemoryAddresses.Castles.Address, castles, 0, MemoryAddresses.Castles.Length);

            defaultKnights = new DefaultKnightData[MemoryAddresses.DefaultKnights.Length];
            viewAccessor.ReadArray(MemoryAddresses.DefaultKnights.Address, defaultKnights, 0,
                MemoryAddresses.DefaultKnights.Length);

            classes = new ClassData[MemoryAddresses.Classes.Length];
            viewAccessor.ReadArray(MemoryAddresses.Classes.Address, classes, 0, MemoryAddresses.Classes.Length);

            items = new ItemData[MemoryAddresses.Items.Length];
            viewAccessor.ReadArray(MemoryAddresses.Items.Address, items, 0, MemoryAddresses.Items.Length);

            monstersInSummon = new MonsterInSummonData[MemoryAddresses.MonstersInSummon.Length];
            viewAccessor.ReadArray(MemoryAddresses.MonstersInSummon.Address, monstersInSummon, 0,
                MemoryAddresses.MonstersInSummon.Length);

            specialAttacks = new SpecialAttackData[MemoryAddresses.SpecialAttacks.Length];
            viewAccessor.ReadArray(MemoryAddresses.SpecialAttacks.Address, specialAttacks, 0,
                MemoryAddresses.SpecialAttacks.Length);

            spells = new SpellData[MemoryAddresses.Spells.Length];
            viewAccessor.ReadArray(MemoryAddresses.Spells.Address, spells, 0, MemoryAddresses.Spells.Length);

            skills = new SkillData[MemoryAddresses.Skills.Length];
            viewAccessor.ReadArray(MemoryAddresses.Skills.Address, skills, 0, MemoryAddresses.Skills.Length);

#if WORK_IN_PROGRESS
            var statGrowth = new StatGrowthData[MemoryAddresses.StatGrowth.Length];
            thisViewAccessor.ReadArray(MemoryAddresses.StatGrowth.Address, statGrowth, 0, MemoryAddresses.StatGrowth.Length);
#endif
        }

        /// <summary>
        /// If the address is above the VirtualStartAddress if will adjust it using <see cref="AdjustAddress"/> and then
        /// try to return a string at the given address.<para/>
        /// If the address is below the VirtualStartAddress it will be treated as a direct reference in the file and get
        /// the string directly at that spot instead of adjusting it first.
        /// </summary>
        /// <param name="address">Pointer to the start of the string.</param>
        /// <returns>The a string from the bytes found.</returns>
        public string DereferenceString(uint address)
        {
            if (address == 0)
                return "Empty";
            var list = new List<byte>();
            byte byteRead;

            // If the address is above the VirtualStartAddress adjust it from PS1 Memomry address space to local file
            // space.
            var viewAccessor = MemoryMappedFile.CreateViewAccessor();
            var adjustedAddress = (address > VirtualStartAddress) ? AdjustAddress(address) : address;
            if (adjustedAddress > viewAccessor.Capacity) return adjustedAddress.ToString("X");
            while ((byteRead = viewAccessor.ReadByte(adjustedAddress + (uint) list.Count)) != 0x00)
            {
                list.Add(byteRead);
            }

            return Encoding.ASCII.GetString(list.ToArray());
        }


        #region Public properties for quick access to different types

        // See the comment starting above the AttackData[] field for why unsafe types are used for some types.
        // TODO Set has been left unfilled so that I implement the set functionality later when writing is finished.
        public AttackData[] Attacks
        {
            get => attacks;
            private set { }
        }

        public CastleData[] Castles
        {
            get => castles;
            private set { }
        }

        public DefaultKnightData[] DefaultKnights
        {
            get => defaultKnights;
            private set { }
        }

        public ClassData[] Classes
        {
            get => classes;
            private set { }
        }

        public ItemData[] Items
        {
            get => items;
            private set { }
        }

        public MonsterInSummonData[] MonstersInSummon
        {
            get => monstersInSummon;
            private set { }
        }

        public SpecialAttackData[] SpecialAttacks
        {
            get => specialAttacks;
            private set { }
        }

        public SpellData[] Spells
        {
            get => spells;
            private set { }
        }

        public SkillData[] Skills
        {
            get => skills;
            private set { }
        }
#if WORK_IN_PROGRESS
        public StatGrowthData[] StatGrowths { get => statGrowths; private set {} }
#endif

        #endregion
    }
}