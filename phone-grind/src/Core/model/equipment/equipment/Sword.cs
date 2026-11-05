using System;
using Game.src.Core.Entities.equipment;
using Game.src.Core.Localization.DynamicVar;
using Game.src.Core.Localization;
using Game.src.Core.Command;
using Godot;
using Game.src.Core.Command.Builder;

namespace Game.src.Core.model.equipment;

public sealed class StaticSword : StaticEqu
{
    public StaticSword()
    {
        StateProp = EquStateProp.Static;
        Prop = EquProp.Attack;
        ActionValue = new DamageVar(4m,DamageProp.Move);
        CDVar = new CDVar(2m,CDProp.Move);
        _VFXShortPath = "";
        Name = LocalManager.Instance.GetTranslateText(_localPropName,"Sword.name");
        Description = LocalManager.Instance.GetTranslateText(_localPropName,"Sword.description",("Damage",4));
        CostVar = new CostVar(4m,CostProp.Move);
        IconTexture = "res://image/EquIcon/Attack/feinting_draw.png";
    }

    public override void OnPlay()
    {
        DamageCmd damageCmd = new();
        damageCmd.DamageCmdCreate((DamageVar)ActionValue,null)
            .SetEquOnwer(this)
            .SetSfx("asd").
            Executive();
    }
}

/// <summary>
/// 实例类型，留空等着填
/// </summary>
public sealed class ModelSword
{
    
}