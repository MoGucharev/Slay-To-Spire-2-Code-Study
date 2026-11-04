

using System;
using Game.src.Core.Localization.DynamicVar;
using Godot;

namespace Game.src.Core.Entities.Creatures;

public class Creature
{
    public string Name;
    public decimal Hp;
    public decimal Block;


    public int AbsorbDamage(DamageVar damageVar)
    {
        int shouldDamage = damageVar.Prop.HasFlag(DamageProp.UnDefend)
            ? 0
            : (int)Math.Min(damageVar.BaseValue,Block);
        
        Block -= shouldDamage;
        return shouldDamage;
    }



}