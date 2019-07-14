using System;
using System.Runtime.InteropServices;

namespace Memory_Map_Builder.DataTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
    public struct Skill
    {
        public uint Name;        
        public uint Description; 
    }
}