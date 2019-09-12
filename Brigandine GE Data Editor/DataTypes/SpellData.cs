using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct SpellData
    {
        public Text Name;
        public Text Description;
        public ushort MPCost;
        public byte Range;
        public byte Damage;
        public ElementEnum Element;
        public byte GroundAndSky;
        public byte Unknown1;
        public byte AOE; // Most Likely Not AoE
        public Unknown Unknowns;

        public unsafe struct Unknown
        {
#pragma warning disable 649
            private fixed byte unknown[3];
#pragma warning restore 649

            public byte this[int index]
            {
                get => unknown[index];
                set => unknown[index] = value;
            }

            public byte[] ToArray()
            {
                var dest = new byte[5];
                for (int i = 0; i < dest.Length; i++)
                {
                    dest[i] = unknown[i];
                }

                return dest;
            }
        }
    }
}