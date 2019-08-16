using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct AttackData
    {
        public Text Name;
        public Text Description;
        public byte Hit;
        public byte RangeMin;
        public byte RangeMax;
        public byte Damage;
        public ElementEnum Element;
        public byte GroundOrSky;
        public byte Unknown1;
        public byte UsableAfterMove;
        public Unknown Unknowns;

        public unsafe struct Unknown
        {
            public fixed byte unknown[3];

            public byte this[int index]
            {
                get => unknown[index];
                set => unknown[index] = value;
            }

            public byte[] ToArray()
            {
                var dest = new byte[3];
                for (int i = 0; i < dest.Length; i++)
                {
                    dest[i] = unknown[i];
                }

                return dest;
            }
        }
    }
}