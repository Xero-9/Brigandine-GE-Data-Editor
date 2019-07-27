using System;
using System.Linq;
using System.Runtime.InteropServices;
using Memory_Map_Builder.Enums;

namespace Memory_Map_Builder.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x1C)]
    public struct CastleData
    {
        public uint      Name;
        public byte      MovesFlag;
        public byte      MovesCity_1;
        public byte      MovesCity_2;
        public byte      MovesCity_3;
        public byte      PrefixForCity;
        public OwnerEnum Owner;
        public ushort    ManaPerMonth;
        public byte      MonstersThatCanBeSummoned_1;
        public byte      MonstersThatCanBeSummoned_2;
        public byte      MonstersThatCanBeSummoned_3;
        public byte      MonstersThatCanBeSummoned_4;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x1C)]
    public unsafe struct unsafeCastleData
    {
        public       uint      Name;
        public       byte      MovesFlag;
        public fixed byte      CastlesConnectedTo[3];
        public       byte      PrefixForCity;
        public       OwnerEnum Owner;
        public       ushort    ManaPerMonth;
        public fixed byte      MonstersThatCanBeSummoned[0xF]; // or [16]

        public static implicit operator CastleData(unsafeCastleData castle)
        {
            return *(CastleData*) &castle;
        }
    }
}
