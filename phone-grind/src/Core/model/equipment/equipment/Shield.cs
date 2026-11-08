using System;
using Game.src.Core.Entities.equipment;
using Game.src.Core.Localization.DynamicVar;
using Game.src.Core.Localization;
using Game.src.Core.Command;
using Godot;
using Game.src.Core.Command.Builder;
using System.Threading.Tasks;
using Game.src.Core.Hook;

namespace Game.src.Core.model.equipment;

public sealed class StaticShield : StaticEqu
{
    public StaticShield()
    {
        StateProp = EquStateProp.Static;
        Prop = EquProp.Defend;
        ActionValue = new DamageVar(2m,DamageProp.Move);
        CDVar = new CDVar(1m,CDProp.Move);
        _VFXShortPath = "";
        Name = LocalManager.Instance.GetTranslateText(_localPropName,"Sword.name");
        Description = LocalManager.Instance.GetTranslateText(_localPropName,"Sword.description",("Damage",ActionValue.BaseValue));
        CostVar = new CostVar(4m,CostProp.Move);
        IconTexture = "res://image/EquIcon/Defend/guard.png";
    }

    public override void OnPlay()
    {
        Hooks.BeforeEquPlayed(this);
        DamageCmd damageCmd = new();
        damageCmd.DamageCmdCreate((DamageVar)ActionValue,null)
            .SetEquOnwer(this)
            .SetSfx("护盾").
            Executive();
    }
    public override Task BeforeEquPlayed(EquipmentBase transformData)
    {
        if (transformData.Onwer == this.Onwer) return Task.CompletedTask;
        GD.Print("BeforeEquPlayed");
        _damagePlus(1m);
        return Task.CompletedTask;

    }
}