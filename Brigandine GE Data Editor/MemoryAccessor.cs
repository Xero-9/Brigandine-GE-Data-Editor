//#define WORK_IN_PROGRESS

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BrigandineGEDataEditor.DataTypes;

namespace BrigandineGEDataEditor
{
    /// <summary>
    /// Class for creating a view into a Brigandine Grand Edition data file that has been opened as a MemoryMappedFile.
    /// There are properties and methods for quick access and viewing of the internal data.
    /// The file loaded should be named SLPS_026.61 or SLPS_026.62 for the second disk.
    /// </summary>
    public class MemoryAccessor : IDisposable
    {
        private static readonly List<MemoryMappedFile> MemoryMappedFilesToBeDisposed = new List<MemoryMappedFile>();
        public static void DisposeAllMappedFiles() => MemoryMappedFilesToBeDisposed.ForEach(m => m.Dispose());

        public const long FileSize = 0x97000;
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
            var adjustedAddress = address - VirtualStartAddress;
            adjustedAddress -= (HeaderB + HeaderA);
            return adjustedAddress;
        }

        /// <summary>
        /// Creates an instance of the <seealso cref="MemoryAccessor"/> class using the default english patch data.
        /// </summary>
        /// <returns>An instance of the accessor into the mapped file for easy access to the data structs.</returns>
        public static MemoryAccessor CreateAccessor() => CreateAccessor(DefaultMemoryMappedFileBuilder.BuildMemoryMapFromResourceFile(), true);

        /// <summary>
        /// Creates an instance of the <seealso cref="MemoryAccessor"/> class.
        /// TODO: Create more factory methods to use disk image files directly.
        /// </summary>
        /// <param name="memoryMappedFile">The MemoryMappedFile to get data from.</param>
        /// <param name="usingDefaultData">(Optional) Set to true if you are using the default data.</param>
        /// <returns>An instance of the accessor into the mapped file for easy access to the data structs.</returns>
        public static MemoryAccessor CreateAccessor(MemoryMappedFile memoryMappedFile, bool usingDefaultData = false)
        {
            MemoryAccessor memoryAccessor;
            try
            {
                memoryAccessor = new MemoryAccessor(memoryMappedFile, usingDefaultData);
            }
            catch (Exception)
            {
                memoryAccessor = null;
            }
            if (memoryAccessor != null && memoryAccessor.IsOpen)
            {
                return memoryAccessor;
            }
            memoryAccessor?.Dispose();
            return memoryAccessor;
        }

        /// <summary>
        /// Creates an instance of the <seealso cref="MemoryAccessor"/> class.
        /// </summary>
        /// <param name="pathToFile">Path to the SLPS_026 file to read or write.</param>
        /// <returns>An instance of the accessor into the mapped file for easy access to the data structs.</returns>
        public static MemoryAccessor CreateAccessor(string pathToFile) =>
            CreateAccessor(MemoryMappedFile.CreateFromFile(pathToFile, FileMode.OpenOrCreate));

        /// <summary>
        /// True if the MemoryAccessor is open and ready for editing.
        /// </summary>
        public bool IsOpen { get; private set; }
        private MemoryMappedFile MemoryMappedFile { get; set; }

        /// <summary>
        /// Returns if this MemoryAccessor is using the internal default data.
        /// </summary>
        public bool UsingDefaultData { get; private set; }
        private MemoryAccessor(MemoryMappedFile memoryMappedFile, bool usingDefaultData = false)
        {
            UsingDefaultData = usingDefaultData;
            MemoryMappedFile = memoryMappedFile;
            using (var viewAccessor = MemoryMappedFile?.CreateViewAccessor())
            {
                var bytesCheck = new byte[] {0x50, 0x53, 0x2D, 0x58, 0x20, 0x45, 0x58, 0x45};
                byte[] bytes = new byte[8];
                if (viewAccessor?.ReadArray(0, bytes, 0, 8) != 8)
                {
                    MemoryMappedFile?.Dispose();
                    throw new Exception("This file has the wrong header size returning early.");
                }

                if (bytesCheck.Where((t, i) => bytes[i] != t).Any())
                {
                    MemoryMappedFile?.Dispose();
                    throw new Exception("This file has the wrong header returning early.");
                }

                MemoryMappedFilesToBeDisposed.Add(MemoryMappedFile);
                IsOpen = true;
                GetData(viewAccessor);
            }
        }

        private void GetData(MemoryMappedViewAccessor viewAccessor)
        {
            Attacks = new AttackData[MemoryAddresses.Attacks.Length];
            viewAccessor.ReadArray(MemoryAddresses.Attacks.Address, Attacks, 0, MemoryAddresses.Attacks.Length);

            Castles = new CastleData[MemoryAddresses.Castles.Length];
            viewAccessor.ReadArray(MemoryAddresses.Castles.Address, Castles, 0, MemoryAddresses.Castles.Length);

            DefaultKnights = new DefaultKnightData[MemoryAddresses.DefaultKnights.Length];
            viewAccessor.ReadArray(MemoryAddresses.DefaultKnights.Address, DefaultKnights, 0, MemoryAddresses.DefaultKnights.Length);

            Classes = new ClassData[MemoryAddresses.Classes.Length];
            viewAccessor.ReadArray(MemoryAddresses.Classes.Address, Classes, 0, MemoryAddresses.Classes.Length);

            Items = new ItemData[MemoryAddresses.Items.Length];
            viewAccessor.ReadArray(MemoryAddresses.Items.Address, Items, 0, MemoryAddresses.Items.Length);

            SpecialAttacks = new SpecialAttackData[MemoryAddresses.SpecialAttacks.Length];
            viewAccessor.ReadArray(MemoryAddresses.SpecialAttacks.Address, SpecialAttacks, 0, MemoryAddresses.SpecialAttacks.Length);

            Spells = new SpellData[MemoryAddresses.Spells.Length];
            viewAccessor.ReadArray(MemoryAddresses.Spells.Address, Spells, 0, MemoryAddresses.Spells.Length);

            Skills = new SkillData[MemoryAddresses.Skills.Length];
            viewAccessor.ReadArray(MemoryAddresses.Skills.Address, Skills, 0, MemoryAddresses.Skills.Length);

#if WORK_IN_PROGRESS

            monstersInSummon = new MonsterInSummonData[MemoryAddresses.MonstersInSummon.Length];
            viewAccessor.ReadArray(MemoryAddresses.MonstersInSummon.Address, monstersInSummon, 0,
                MemoryAddresses.MonstersInSummon.Length);

            statGrowth = new StatGrowthData[MemoryAddresses.StatGrowth.Length];
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

            // If the address is above the VirtualStartAddress adjust it from PS1 Memory address space to local file
            // space.
            using (var viewAccessor = MemoryMappedFile.CreateViewAccessor())
            {
                var adjustedAddress = (address > VirtualStartAddress) ? AdjustAddress(address) : address;
                if (adjustedAddress > viewAccessor.Capacity) return adjustedAddress.ToString("X");
                byte byteRead;
                while ((byteRead = viewAccessor.ReadByte(adjustedAddress + (uint) list.Count)) != 0x00)
                {
                    list.Add(byteRead);
                }
            }

            return Encoding.ASCII.GetString(list.ToArray());
        }
        
        /// <summary>
        /// Saves all data back to the MemoryMappedFile used to create this instance.
        /// TODO Add better file handling when saving
        /// </summary>
        public void SaveAllDataTypesIntoMemoryMappedFile(FileStream fileStream = null)
        {
            if (fileStream != null)
            {
                if (fileStream.Length != FileSize || !Regex.IsMatch(fileStream.Name, ".61|.62"))
                {
                    throw new
                        Exception($"{fileStream.Name} Size: {fileStream.Length} is not a valid file to save to please choose one that is the proper size and has the extension .61 or .62.");
                }
                var newMemoryMappedFile = MemoryMappedFile.CreateFromFile(fileStream, fileStream.Name, fileStream.Length, MemoryMappedFileAccess.ReadWriteExecute, HandleInheritability.None, false);
                MemoryMappedFilesToBeDisposed.Remove(MemoryMappedFile);
                MemoryMappedFile.Dispose();
                MemoryMappedFile = newMemoryMappedFile;
            }
            
            using(var accessor = MemoryMappedFile.CreateViewAccessor())
            {
                
                accessor.WriteArray(new AttackData().GetAddress(0), Attacks, 0, Attacks.Length);
                accessor.WriteArray(new CastleData().GetAddress(0), Castles, 0, Castles.Length);
                accessor.WriteArray(new ClassData ().GetAddress(0), Classes, 0, Castles.Length);
                accessor.WriteArray(new DefaultKnightData().GetAddress(0), DefaultKnights, 0, DefaultKnights.Length);
                accessor.WriteArray(new ItemData().GetAddress(0), Items, 0, Items.Length);
                accessor.WriteArray(new SkillData().GetAddress(0), Skills, 0, Skills.Length);
                accessor.WriteArray(new SpellData().GetAddress(0), Spells, 0, Spells.Length);
                accessor.WriteArray(new SpecialAttackData().GetAddress(0), SpecialAttacks, 0, SpecialAttacks.Length);

                // Work In Progress
                #if Work_In_Progress
                        accessor.WriteArray(new MonsterInSummonData().GetAddress(0), MonstersInSummon, 0, MonstersInSummon.Length);
                        accessor.WriteArray(new MonsterData().GetAddress(0), memoryAccessor.Monsters, 0, memoryAccessor.Attacks.Length);
                        accessor.WriteArray(new StatGrowthData().GetAddress(0), memoryAccessor.StatGrowths, 0, memoryAccessor.Attacks.Length);
                #endif
            }

        }

        #region Public properties for quick access to different types

        // See the comment starting above the AttackData[] field for why unsafe types are used for some types.
        public AttackData[] Attacks { get; set; }

        public CastleData[] Castles { get; set; }

        public DefaultKnightData[] DefaultKnights { get; set; }

        public ClassData[] Classes { get; set; }

        public ItemData[] Items { get; set; }

        public SpecialAttackData[] SpecialAttacks { get; set; }

        public SpellData[] Spells { get; set; }

        public SkillData[] Skills { get; set; }

#if WORK_IN_PROGRESS
        public MonsterInSummonData[] MonstersInSummon { get; set; }
        public MonsterData[] Monsters { get; set; }
        public StatGrowthData[] StatGrowths { get; set; }
#endif

        #endregion

        public void Dispose() => MemoryMappedFile?.Dispose();
    }
}