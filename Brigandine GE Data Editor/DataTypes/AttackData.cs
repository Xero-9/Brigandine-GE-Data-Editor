using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct AttackData
    {
        public TextData    Name;
        public TextData    Description;
        public byte    Hit;
        public byte    RangeMin;
        public byte    RangeMax;
        public byte    Damage;
        public Element Element;
        public byte    GroundOrSky;
        public byte    Unk;
        public byte    UsableAfterMove;
        public Unknown Unknowns;

        public unsafe struct Unknown
        {
            public fixed byte unknown[3];

            public byte this[int index]
            {
                get => unknown[index];
                set => unknown[index] = value;
            }
        }
    }
}
    

