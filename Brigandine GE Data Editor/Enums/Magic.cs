using System;

namespace BrigandineGEDataEditor.Enums
{
    [Flags]
    public enum Magic
    {
        Heal        = 0x01,
        HealingWord = 0x02,
        Cleanse     = 0x04,
        Hallow      = 0x08,
        DivineRay   = 0x10,
        HolyWord    = 0x20,
        Venom       = 0x40,
        MeteorDoom  = 0x80,
        Curse       = 0x100,
        Dimension   = 0x200,
        Weakness    = 0x400,
        Husk        = 0x800,
        Flame       = 0x1000,
        GenoFlame   = 0x2000,
        ExaBlast    = 0x4000,
        Accel       = 0x8000,
        Fury        = 0x10000,
        Solid       = 0x20000,
        Frost       = 0x40000,
        GenoFrost   = 0x80000,
        IceFall     = 0x100000,
        Flight      = 0x200000,
        Ward        = 0x400000,
        Charm       = 0x800000,
        Bolt        = 0x1000000,
        GenoBolt    = 0x2000000,
        React       = 0x4000000,
        Paralyze    = 0x8000000,
        Harden      = 0x10000000,
        Dumb        = 0x20000000,
    }
}