using Memory_Map_Builder.Enums;

namespace Memory_Map_Builder.DataTypes
{
    public struct MonsterData
    {
        public OwnerEnum Owner;
        public byte MonsterSlotNumber;
        public byte Class;
        public byte Level;
        public ushort HP;
        public ushort MP;
        public byte STR;
        public byte INT;
        public byte AGI;
        public byte ItemEquiped;
        public uint Name;
    }
}