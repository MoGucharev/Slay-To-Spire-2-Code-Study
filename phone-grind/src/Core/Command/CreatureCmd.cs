using Game.src.Core.Entities.Creatures;
using Game.src.Core.Localization.DynamicVar;

namespace  Game.src.Core.Command;

public static class CreatureCmd
{
    public static void TakeDamage(Creature creature,DamageVar damageVar)
    {
        int finalDamage = creature.AbsorbDamage(damageVar);
        creature.Hp -= finalDamage;
    }
}