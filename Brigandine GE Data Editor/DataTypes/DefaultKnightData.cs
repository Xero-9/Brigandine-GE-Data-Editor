using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;
#pragma warning disable 649

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x28)]
    public struct DefaultKnightData
    {
        public Text Name;
        public ClassEnum Class;
        public byte Level;
        public ushort XP;
        public ushort HP;
        public ushort MP;
        public byte STR;
        public byte INT;
        public byte AGI;
        public byte RunePwrGrowth_RuneArea;
        public ushort RunePwr;

        public unsafe struct Monster
        {
            private fixed byte monsters[6];

            public byte this[int index]
            {
                get => monsters[index];
                set => monsters[index] = value;
            }

            public byte[] ToArray()
            {
                var dest = new byte[6];
                for (int i = 0; i < dest.Length; i++)
                {
                    dest[i] = monsters[i];
                }

                return dest;
            }
        }

        public Monster Monsters;

        public unsafe struct Unknown
        {
            private fixed byte unknown[4];

            public byte this[int index]
            {
                get => unknown[index];
                set => unknown[index] = value;
            }

            public byte[] ToArray()
            {
                var dest = new byte[4];
                for (int i = 0; i < dest.Length; i++)
                {
                    dest[i] = unknown[i];
                }

                return dest;
            }
        }

        public Unknown Unknown1;
        public Unknown Unknown2;

        public CountryEnum Country;
        public CastlesEnum Castle;

        public Unknown Unknown3;
        public byte Unknown4;
        public byte Unknown5;
    }
}