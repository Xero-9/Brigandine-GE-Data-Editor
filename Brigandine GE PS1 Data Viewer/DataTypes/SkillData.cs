using System;
using System.Runtime.InteropServices;

namespace Memory_Map_Builder.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
    public struct SkillData
    {
        public uint Name;        
        public uint Description; 
    }
}