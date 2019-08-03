using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    public struct AttackData
    {
        public uint    Name;
        public uint    Description;
        public byte    Hit;
        public byte    RangeMin;
        public byte    RangeMax;
        public byte    Damage;
        public Element Element;
        public byte    GroundOrSky;
        public byte    Unk;
        public byte    UsableAfterMove;
        public byte    Unk5;
        public byte    Unk6;
        public byte    Unk7;
    }
}
    

