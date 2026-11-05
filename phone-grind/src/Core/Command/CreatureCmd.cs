using Game.src.Core.Entities.Creatures;
using Game.src.Core.Localization.DynamicVar;
using Game.src.Core.Hook;

namespace  Game.src.Core.Command;

public static class CreatureCmd
{
    public static void TakeDamage(Creature creature,DamageVar damageVar)
    {
        Hooks.BeforeDamageApplied();
        int finalDamage = creature.AbsorbDamage(damageVar);
        creature.Hp -= finalDamage;
        Hooks.AfterDamageApplied();
    }
}