// Not working I think the struct may be wrong.
//#define DEBUG_MEMORY_STATGROWTH_BROKEN
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Memory_Map_Builder.DataGetter;
using Memory_Map_Builder.DataTypes;
using Memory_Map_Builder.Location;
using Microsoft.Win32.SafeHandles;

namespace Memory_Map_Builder
{
    /// <summary>
    /// Class for creating a view into a Brigandine Grand Edition data file that has been opened as a MemoryMappedFile.
    /// There are also properties and methods for quick access and viewing of the internal data.
    /// It should be named SLPS_026.61 or SLPS_026.62 for the second disk. <seealso cref="CreateAccessor"/>
    /// TODO 1. Add functionality for loading the MemoryMappedFile from a string.
    /// TODO 2. Add functionality for altering and saving the data back to the file.
    /// </summary>
    public class MemoryAccessor : UnmanagedMemoryAccessor
    {
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
        /// </summary>
        /// <param name="memoryMappedFile"></param>
        /// <returns>An instance of the accessor into the mapped file for easy access to the data structs.</returns>
        public static MemoryAccessor CreateAccessor(MemoryMappedFile memoryMappedFile) =>
            new MemoryAccessor(memoryMappedFile);


        // The MemoryMappedFile that this unmanagedMemoryAccessor uses for getting and eventually setting that data.
        private MemoryMappedFile brigandineAsMappedFile;
        // The view access that the safe buffer that this class uses in Initialize function comes from this view.
        private MemoryMappedViewAccessor thisViewAccessor;
        
        // The unsafe versions are used since they use arrays internally,
        // which may be needed for future functions related to data checking and writing,
        // and will likely become internal at a later time.
        private AttackType[]            attackTypes;
        private unsafeCastle[ ]         castles;
        private unsafeDefaultKnight[ ]  defaultKnights;
        private unsafeFighterDefault[ ] fighterDefaults;
        private Item[ ]                 items;
        private unsafeSpecialAttack[ ]  specialAttacks;
        private unsafeSpell[ ]          spells;
        private Skill[ ]                skills;
        private MemoryAccessor(MemoryMappedFile memoryMappedFile)
        {
            brigandineAsMappedFile = memoryMappedFile;
            thisViewAccessor = brigandineAsMappedFile.CreateViewAccessor();
            
            // TODO Make ReadWrite when I have added handling for writing back to the file.
            Initialize(thisViewAccessor.SafeMemoryMappedViewHandle, 0, thisViewAccessor.Capacity, FileAccess.Read);

            attackTypes     = new AttackType[MemoryAddresses.AttackType.Length];
            ReadArray(MemoryAddresses.AttackType.Address, attackTypes, 0, MemoryAddresses.AttackType.Length);
            //AttackTypes     = attackTypes;

            castles         = new unsafeCastle[MemoryAddresses.Castle.Length];
            ReadArray(MemoryAddresses.Castle.Address, castles, 0, MemoryAddresses.Castle.Length);
            //castles         = castles;
            
            defaultKnights  = new unsafeDefaultKnight[MemoryAddresses.DefaultKnight.Length];
            ReadArray(MemoryAddresses.DefaultKnight.Address, defaultKnights, 0, MemoryAddresses.DefaultKnight.Length);
            //defaultKnights  = defaultKnights;
            
            fighterDefaults = new unsafeFighterDefault[MemoryAddresses.FighterDefault.Length];
            ReadArray(MemoryAddresses.FighterDefault.Address, fighterDefaults, 0,MemoryAddresses.FighterDefault.Length);
            //fighterDefaults = fighterDefaults;
            
            items           = new Item[MemoryAddresses.Item.Length];
            ReadArray(MemoryAddresses.Item.Address, items, 0, MemoryAddresses.Item.Length);
            //items           = items;
            
            specialAttacks  = new unsafeSpecialAttack[MemoryAddresses.SpecialAttack.Length];
            ReadArray(MemoryAddresses.SpecialAttack.Address, specialAttacks, 0, MemoryAddresses.SpecialAttack.Length);
            //specialAttacks  = specialAttacks;
            
            spells          = new unsafeSpell[MemoryAddresses.Spell.Length];
            ReadArray(MemoryAddresses.Spell.Address, spells, 0, MemoryAddresses.Spell.Length);
            //spells          = spells;
            
            skills          = new Skill[MemoryAddresses.Skill.Length];
            ReadArray(MemoryAddresses.Skill.Address, skills, 0, MemoryAddresses.Skill.Length);
            //skills          = skills;

#if DEBUG_MEMORY_STATGROWTH_BROKEN
            var statGrowth = new ClassStatGrowth[MemoryAddresses.StatGrowth.Length];
            ReadArray(MemoryAddresses.StatGrowth.Address, statGrowth, 0, MemoryAddresses.StatGrowth.Length);
            StatGrowths = statGrowth;
#endif
        }


        /// <summary>
        /// From the address it reads the string until the first null character (0x00) and returns it.
        /// </summary>
        /// <param name="address">Pointer to the start of the string.</param>
        /// <returns>The a string from the bytes found.</returns>
        public string DereferenceString(uint address)
        { 
            if (address == 0)
                return "Empty";
            var list = new List<byte>();
            byte byteRead;

            // Check if the address is above the VirtualStartAddress so we can adjust the address to be used.
            var adjustedAddress = (address > VirtualStartAddress) ? AdjustAddress(address) : address;

            while ((byteRead = ReadByte(adjustedAddress + (uint) list.Count)) != 0x00)
            {
                list.Add(byteRead);
            }
            return Encoding.ASCII.GetString(list.ToArray());
        }


        #region Public properties for quick access to different types
        // See the comment above starting above the AttackType[] field for why unsafe types are used for some types.
        // TODO Set has been left unfilled so that I implement the set functionality later when writing is finished.
        public AttackType[]     AttackTypes     { get => attackTypes; private set {} }
        public unsafeCastle[]         Castles         { get => castles; private set {} }
        public unsafeDefaultKnight[]  DefaultKnights  { get=> defaultKnights; private set {} }
        public unsafeFighterDefault[] FighterDefaults { get => fighterDefaults; private set {} }
        public Item[ ]          Items           { get => items ; private set {} }
        public unsafeSpecialAttack[]  SpecialAttacks  { get=> specialAttacks; private set {} }
        public unsafeSpell[]          Spells          { get=> spells; private set {} }
        public Skill[]          Skills          { get => skills; private set {} }
#if DEBUG_MEMORY_STATGROWTH_BROKEN
        private ClassStatGrowth[] statGrowths;
        public ClassStatGrowth[] StatGrowths { get => statGrowths; private set; }
#endif
  #endregion
    }
}
