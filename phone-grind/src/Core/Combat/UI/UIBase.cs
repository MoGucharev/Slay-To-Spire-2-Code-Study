using System;
using Godot;
using Game.src.Core.Entities.equipment;
using Game.src.Core.Localization.DynamicVar;
using Game.src.Core.model;
using Game.src.Core.Node.Equ.NEquNamespace;
using Game.src.Core.model.equipment;
using Game.src.Core.Localization;
using Game.src.Core.Node.Pooling;

namespace Game.src.Core.Combat.UI;


/// <summary>
/// 商店UI和网格UI的基类,但是由于没有创建其他基类，先这样用着
/// </summary>
public partial class UIBase :Control
{

    public override void _Ready()
    {
        base._Ready();
        NStaticEqu.InitPool();
        NodePool.GetPoolable(typeof(NStaticEqu),out IPoolable poolable);
        NStaticEqu test = (NStaticEqu)poolable;
        test.EquipmentData = new StaticSword();

        AddChild(test);

    }
}