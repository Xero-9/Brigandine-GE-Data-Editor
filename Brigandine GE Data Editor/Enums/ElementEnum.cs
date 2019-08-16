using System;

namespace BrigandineGEDataEditor.Enums
{
    [Flags]
    public enum ElementEnum : ushort
    {
        None = 0,

        White = 1,
        //White_2 = 2,
        //White_3 = 4,

        Black = 0x8,
        //Black_2 = 16,
        //Black_3 = 32,

        Red = 0x40,
        //Red_2 = 128,
        //Red_3 = 256,

        Blue = 0x200,
        //Blue_2 = 1024,
        //Blue_3 = 2048,

        Green = 0x1000,
        //Green_2 = 8192,
        //Green_3 = 16384
    }
}