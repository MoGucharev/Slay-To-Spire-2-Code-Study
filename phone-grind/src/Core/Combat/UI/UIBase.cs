using System;
using Godot;
using Game.src.Core.Entities.equipment;
using Game.src.Core.Localization.DynamicVar;
using Game.src.Core.model;
using Game.src.Core.Node.Equ.NEquNamespace;
using Game.src.Core.model.equipment;
using Game.src.Core.Localization;
using Game.src.Core.Node.Pooling;
using Game.src.Core.Node.Equ.Holders;
using Game.src.Core.Helper;
using Game.src.Core.Hook;

namespace Game.src.Core.Combat.UI;


/// <summary>
/// 商店UI和网格UI的基类,但是由于没有创建其他基类，先这样用着
/// </summary>
public partial class UIBase :HBoxContainer
{

    public override void _Ready()
    {
        base._Ready();
        NStaticEqu.InitPool(); //对象池初始化
        ModelDb.InitEquDb(); //数据库初始化
        NodePool.GetPoolable(typeof(NStaticEqu),out IPoolable poolable); //从对象池得到对象
        NodePool.GetPoolable(typeof(NStaticEqu),out IPoolable poolable2);
        StaticEquHolder testHolder = SceneHelper.GetInstant<StaticEquHolder>("StaticEqu/Holder/static_equ_holder");
        StaticEquHolder testHolder2 = SceneHelper.GetInstant<StaticEquHolder>("StaticEqu/Holder/static_equ_holder");
        NStaticEqu test = (NStaticEqu)poolable;
        NStaticEqu test2 = (NStaticEqu)poolable2;
        test.EquipmentData = ModelDb.GetMutable<StaticSword>(); //从数据库得到数据
        test2.EquipmentData = ModelDb.GetMutable<StaticShield>(); //从数据库得到数据
        testHolder.SetEqu(test);
        testHolder2.SetEqu(test2);
        AddChild(testHolder);
        AddChild(testHolder2);
        Hooks.InitHooks();//钩子初始化
    }
}