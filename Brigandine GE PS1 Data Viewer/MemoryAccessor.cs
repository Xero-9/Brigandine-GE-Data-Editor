//#define WORK_IN_PROGRESS
using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Memory_Map_Builder.DataTypes;
using Microsoft.Win32.SafeHandles;

namespace Memory_Map_Builder
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
        private bool IsOpen { get; set; }
        // TODO Add Documentation.
        public static MemoryAccessor CreateAccessor(string PathToFile)
        {
            using (var fs = File.Open(PathToFile, FileMode.OpenOrCreate))
            {   
                return new MemoryAccessor(MemoryMappedFile.CreateFromFile(fs, "SLPS_026", fs.Length,
                                                                      MemoryMappedFileAccess.CopyOnWrite,
                                                                      HandleInheritability.None, true));

                //return CreateAccessor(MemoryMappedFile.CreateFromFile(PathToFile, FileMode.OpenOrCreate));
            }
        }
        // The MemoryMappedFile that this unmanagedMemoryAccessor uses for getting and eventually setting that data.
        private MemoryMappedFile brigandineAsMappedFile;
        // The view accessor that the safe buffer that is used in the Initialize function comes from this view.
        private MemoryMappedViewAccessor thisViewAccessor;
        
        // The unsafe versions are used since they use arrays internally,
        // which may be needed for future functions related to data checking and writing,
        // and will likely become internal at a later time.
        private AttackData[]            attackDatas;
        private unsafeCastleData[ ]         castles;
        private unsafeDefaultKnightData[ ]  defaultKnights;
        private unsafeClassData[ ] fighterDefaults;
        private ItemData[ ]                 itemsData;
        private unsafeSpecialAttackData[ ]  specialAttacks;
        private unsafeSpellData[ ]          spells;
        private SkillData[ ]                skillsData;
        private MemoryAccessor(MemoryMappedFile memoryMappedFile)
        {
            // TODO Fix Access Denied for passed memoryMappedFile.
            //brigandineAsMappedFile = memoryMappedFile;
            brigandineAsMappedFile = BrigandineMemoryMapBuilder.BrigandineAsMemoryMappedFile;
            thisViewAccessor = brigandineAsMappedFile.CreateViewAccessor();

            // TODO Find a better way to check its the right file and refactor it to it's own function.
            var bytesCheck = new byte[ ] { 0x50, 0x53, 0x2D, 0x58, 0x20, 0x45, 0x58, 0x45 };
            byte[ ] bytes = new byte[8];
            if (thisViewAccessor.ReadArray(0, bytes, 0, 8) != 8)
            {
                brigandineAsMappedFile.Dispose();
                return;
                throw new Exception("This file has the wrong header size returning early.");
            }
            for (int i = 0; i < bytesCheck.Length; i++)
            {
                if(bytes[i] != bytesCheck[i])
                {
                    brigandineAsMappedFile.Dispose();
                    return;
                    throw new Exception("This file has the wrong header returning early.");
                } 
            }
            memoryMappedFilesToBeDisposed.Add(this.brigandineAsMappedFile);
            // TODO Make ReadWrite when I have added handling for writing back to the file.
            //Initialize(thisViewAccessor.SafeMemoryMappedViewHandle, 0, thisViewAccessor.Capacity, FileAccess.Read);
            IsOpen = true;
            GetData();
        }

        private void GetData()
        {
            attackDatas     = new AttackData[MemoryAddresses.Attacks.Length];
            thisViewAccessor.ReadArray(MemoryAddresses.Attacks.Address, attackDatas, 0, MemoryAddresses.Attacks.Length);
            //AttackDatas     = attackDatas;

            castles         = new unsafeCastleData[MemoryAddresses.Castles.Length];
            thisViewAccessor.ReadArray(MemoryAddresses.Castles.Address, castles, 0, MemoryAddresses.Castles.Length);
            //castles         = castles;
            
            defaultKnights  = new unsafeDefaultKnightData[MemoryAddresses.DefaultKnights.Length];
            thisViewAccessor.ReadArray(MemoryAddresses.DefaultKnights.Address, defaultKnights, 0, MemoryAddresses.DefaultKnights.Length);
            //defaultKnights  = defaultKnights;
            
            fighterDefaults = new unsafeClassData[MemoryAddresses.FighterDefaults.Length];
            thisViewAccessor.ReadArray(MemoryAddresses.FighterDefaults.Address, fighterDefaults, 0,MemoryAddresses.FighterDefaults.Length);
            //fighterDefaults = fighterDefaults;
            
            itemsData           = new ItemData[MemoryAddresses.Items.Length];
            thisViewAccessor.ReadArray(MemoryAddresses.Items.Address, itemsData, 0, MemoryAddresses.Items.Length);
            //itemsData           = itemsData;
            
            specialAttacks  = new unsafeSpecialAttackData[MemoryAddresses.SpecialAttacks.Length];
            thisViewAccessor.ReadArray(MemoryAddresses.SpecialAttacks.Address, specialAttacks, 0, MemoryAddresses.SpecialAttacks.Length);
            //specialAttacks  = specialAttacks;
            
            spells          = new unsafeSpellData[MemoryAddresses.Spells.Length];
            thisViewAccessor.ReadArray(MemoryAddresses.Spells.Address, spells, 0, MemoryAddresses.Spells.Length);
            //spells          = spells;
            
            skillsData          = new SkillData[MemoryAddresses.Skills.Length];
            thisViewAccessor.ReadArray(MemoryAddresses.Skills.Address, skillsData, 0, MemoryAddresses.Skills.Length);
            //skillsData          = skillsData;

#if WORK_IN_PROGRESS
            var statGrowth = new StatGrowthData[MemoryAddresses.StatGrowth.Length];
            ReadArray(MemoryAddresses.StatGrowth.Address, statGrowth, 0, MemoryAddresses.StatGrowth.Length);
            StatGrowths = statGrowth;
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
            var adjustedAddress = (address > VirtualStartAddress) ? AdjustAddress(address) : address;

            while ((byteRead = thisViewAccessor.ReadByte(adjustedAddress + (uint) list.Count)) != 0x00)
            {
                list.Add(byteRead);
            }
            return Encoding.ASCII.GetString(list.ToArray());
        }


        #region Public properties for quick access to different types
        // See the comment starting above the AttackData[] field for why unsafe types are used for some types.
        // TODO Set has been left unfilled so that I implement the set functionality later when writing is finished.
        public ref AttackData[]     AttackDatas     { get => ref attackDatas; } //private set {} }
        public unsafeCastleData[]         Castles         { get => castles; private set {} }
        public unsafeDefaultKnightData[]  DefaultKnights  { get=> defaultKnights; private set {} }
        public unsafeClassData[] FighterDefaults { get => fighterDefaults; private set {} }
        public ItemData[ ]          ItemsData           { get => itemsData ; private set {} }
        public unsafeSpecialAttackData[]  SpecialAttacks  { get=> specialAttacks; private set {} }
        public unsafeSpellData[]          Spells          { get=> spells; private set {} }
        public SkillData[]          SkillsData          { get => skillsData; private set {} }
#if WORK_IN_PROGRESS
        private StatGrowthData[] statGrowths;
        public StatGrowthData[] StatGrowths { get => statGrowths; private set {} }
#endif
  #endregion
    }
}
