namespace BrigandineGEDataEditor.Enums
{
    public enum FighterSkill : byte
    {
        React            = 0x00,
        HitRun           = 0x01,
        Deflect25Pct     = 0x02,
        ShieldBlock25Pct = 0x03,
        ShieldBlock20Pct = 0x04,
        AntiProjectile   = 0x05,
        MagicDef20Pct    = 0x06,
        MagicDef10Pct    = 0x07,
        HPRegen10Pct     = 0x08,
        HPRegen5Pct      = 0x09,
        AquaticHeal10Pct = 0x0A,
        AquaticHeal65Pct = 0x0B,
        Immune           = 0x0C,
        Invulnerable     = 0xD,
        Evade5Pct        = 0x0E,
        CounterDmg10Pct  = 0x0F,
        Crit10Pct        = 0x10,
        Crit5Pct         = 0x11,
        Accuracy5Pct     = 0x12,
        Barrier          = 0x13,
        ActTimes2        = 0x80,
        None             = 0xFF
    }
}
