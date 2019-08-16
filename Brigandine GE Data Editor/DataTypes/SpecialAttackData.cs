using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct SpecialAttackData
    {
        public Text Name;
        public Text Description;
        public byte MP;
        public byte Range;
        public byte Damage;
        public byte Unk1;
        public ElementEnum Element;
        public byte GroundOrSky;
        public Unknown Unknowns;

        public unsafe struct Unknown
        {
            public fixed byte unknown[5];

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