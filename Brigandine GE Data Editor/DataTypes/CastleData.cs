using System;
using System.Runtime.InteropServices;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x1C)]
    public struct CastleData
    {
        public uint      Name;
        public byte      MovesFlag;
        public ConnectTo CastlesConnectedTo;
        public byte      PrefixForCity;
        public OwnerEnum Owner;
        public ushort    ManaPerMonth;
        public CanSummon MonsterCanSummon;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x0F)]
    public unsafe struct CanSummon
    {
        private fixed byte monsters[0xF];
        public byte this[int index]
        {
            get => monsters[index];
            set => monsters[index] = value;
        }
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x03)]
    public struct ConnectTo
    {
        public byte Castle1;
        public byte Castle2;
        public byte Castle3;

        public byte this[int index]
        {
            get
            {
                switch (Math.Min(Math.Max(index, 0), 2))
                {
                    case 0:
                        return Castle1;
                    case 1:
                        return Castle2;
                    case 2:
                        return Castle3;
                    default:
                        return Castle1;
                }
            }
            set{
                switch (Math.Min(Math.Max(index, 0), 2))
                {
                    case 0:
                        Castle1 = value;
                        break;
                    case 1:
                        Castle2 = value;
                        break;
                    case 2:
                        Castle3 = value;
                        break;
                }
            }
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x1C)]
    public unsafe struct unsafeCastleData
    {
        public       uint      Name;
        public       byte      MovesFlag;
        public fixed byte CastlesConnectedTo[3];
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
