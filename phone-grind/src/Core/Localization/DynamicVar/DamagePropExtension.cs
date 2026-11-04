using System;

namespace  Game.src.Core.Localization.DynamicVar;

public static class DamagePropExpension
{
    public static bool IsOnlyMoveDamage(this DamageVar damageVar)
    {
        DamageProp damageProp = damageVar.Prop;
        if (damageProp.HasFlag(DamageProp.Move))
            return !damageProp.HasFlag(DamageProp.UnDefend);
        return false;
    }

    public static bool IsOnlyUnMoveDamage(this DamageVar damageVar)
    {
        DamageProp damageProp = damageVar.Prop;
        if (damageProp.HasFlag(DamageProp.UnDefend))
            return !damageProp.HasFlag(DamageProp.UnDefend);
        return false;
    }

    public static bool IsMoveAndUndefend(this DamageVar damageVar)
    {
        DamageProp damageProp = damageVar.Prop;
        if (damageProp.HasFlag(DamageProp.Move))
            return damageProp.HasFlag(DamageProp.UnDefend);
        return false;
    }

    public static bool IsUnMoveAndUndefend(this DamageVar damageVar)
    {
        DamageProp damageProp = damageVar.Prop;
        if (damageProp.HasFlag(DamageProp.UnMove))
            return damageProp.HasFlag(DamageProp.UnDefend);
        return false;
    }
}