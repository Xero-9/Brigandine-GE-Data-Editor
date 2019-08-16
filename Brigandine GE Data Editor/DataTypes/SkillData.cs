using System;
using System.Runtime.InteropServices;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
    public struct SkillData
    {
        public Text Name;
        public Text Description;
    }
}