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
        NStaticEqu.InitPool(); //对象池初始化
        ModelDb.InitEquDb(); //数据库初始化
        NodePool.GetPoolable(typeof(NStaticEqu),out IPoolable poolable); //从对象池得到对象
        NStaticEqu test = (NStaticEqu)poolable;
        test.EquipmentData = ModelDb.GetMutable<StaticSword>(); //从数据库得到数据

        AddChild(test);

    }
}