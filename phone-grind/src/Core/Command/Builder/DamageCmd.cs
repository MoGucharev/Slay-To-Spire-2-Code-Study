

using Game.src.Core.Entities.Creatures;
using Game.src.Core.Localization.DynamicVar;
using Game.src.Core.model;
using Godot;

namespace Game.src.Core.Command.Builder;

public class DamageCmd
{
    private DamageVar _damage;
    private Creature _target;
    //等创建角色后再补充是敌人还是玩家
    //private Creature 
    private EquipmentBase? _equipmentOnwer;
    public DamageCmd DamageCmdCreate(DamageVar damageVar,Creature creature)
    {
        _damage = damageVar;
        _target = creature;
        return this;
    }

    public DamageCmd SetEquOnwer(EquipmentBase equipmentBase)
    {
        _equipmentOnwer = equipmentBase;
        return this;
    }

    public DamageCmd SetSfx(string path)
    {
        GD.Print($"成功设置音效{path}");
        return this;
    }

    public void Executive()
    {
        GD.Print("成功执行伤害cmd");
    }
}