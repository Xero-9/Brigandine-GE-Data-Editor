using System.Linq;
using System.Runtime.InteropServices;
using Memory_Map_Builder.Enums;

namespace Memory_Map_Builder.DataTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x1C)]
    public struct Castle
    {
        public uint      Name;
        public byte      MovesFlag;
        public byte      MovesCity_1;
        public byte      MovesCity_2;
        public byte      MovesCity_3;
        public byte      PrefixForCity;
        public OwnerEnum Owner;
        public ushort    ManaPerMonth;
        public byte      SummonMonster_1;
        public byte      SummonMonster_2;
        public byte      SummonMonster_3;
        public byte      SummonMonster_4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x1C)]
    public unsafe struct unsafeCastle
    {
        public       uint      Name;
        public       byte      MovesFlag;
        public fixed byte      CastlesConnectedTo[3];
        public       byte      PrefixForCity;
        public       OwnerEnum Owner;
        public       ushort    ManaPerMonth;
        public fixed byte      MonstersThatCanBeSummoned[0xF]; // or [16]

        public static implicit operator Castle(unsafeCastle castle)
        {
            return *(Castle*) &castle;
        }
    }
}
