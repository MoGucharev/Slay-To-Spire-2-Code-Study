using Godot;
using Game.src.Core.Entities.equipment;
using Game.src.Core.Localization.DynamicVar;
using Game.src.Core.Helper;
using System;

namespace Game.src.Core.model;

/// <summary>
/// 玩家放置的物品的数据基础，包括相关方法和属性
/// </summary>
public class EquipmentBase
{
//计数器，记载是否是第一次用
    protected int useCount = 0;
/// <summary>
/// 是静态还是实体类型，静态类型是在商店里的类型，动态类型为实体类型,即在棋盘中行动的类型
/// </summary>
    public enum EquStateProp
    {
        Static,
        Model
    }
/// <summary>
/// 视效路径,要输入非带有文件夹的短路径
/// </summary>
    protected string _VFXShortPath ="";
    protected string _localPropName = "Equipment";
//状态类型

    protected EquStateProp _stateProp;
//功能类型
    protected  EquProp _equProp;
//行动值
    public virtual DynamicVar ActionValue{get;set;}
//几秒行动一次
    public CDVar CDVar;
//图标资源
    public string IconTexture;

//属于什么类型的图标的地址
    public string ActionTexture;
//场景的地址 
    public string EquPath;
//设备的基本属性
    public virtual EquStateProp StateProp
    {
        get =>_stateProp;
        set
        {
            _stateProp = value;
            EquPath = _setEquScenePath(_stateProp);
        }
    }

    public virtual EquProp Prop
    {
        get => _equProp;
        set
        {
            _equProp = value;
            ActionTexture = _setIconTexture(_equProp);
        }
    }
/// ID模块

    public ModelId Id {get;}

    public EquipmentBase()
    {
        Type type = typeof(EquipmentBase);
        if (!ModelDb.TryGetIdFromDb(type))
        throw new Exception ($"已经创建过{type}类型的ModelId");
        Id = ModelDb.GetModelId(type);
    }

    public EquipmentBase CloneEqu()
    {
        EquipmentBase equipmentBase = new();
        equipmentBase = (EquipmentBase)MemberwiseClone();
        equipmentBase.DeepClone();
        return equipmentBase;
    }

/// <summary>
/// 子类覆写,这是深拷贝
/// </summary>
    public virtual void DeepClone()
    {
        ActionValue = ActionValue.Clone<DynamicVar>(ActionValue);
    }

//

/// <summary>
/// 设置并返回视效，无视效时返回null
/// </summary>
/// <returns></returns>
    public Control? SetVFX()
    {
        if (_VFXShortPath == "") return null;
        return SceneHelper.GetInstant<Control>(_VFXShortPath);
    }



/// <summary>
/// 由于攻击，防御，功能三个图标不同，根据选的种类获取对应的行为图标
/// </summary>
/// <param name="EquProp">所选种类</param>
/// <returns></returns>
    private string _setIconTexture(EquProp EquProp)
    {
        switch (EquProp)
        {
            case EquProp.Attack:
                return "res://image/EquIcon/InformIcon/icon-sword.png";
            case EquProp.Defend:
                return "res://image/EquIcon/InformIcon/icon-shield.png";
            case EquProp.Function:
                return "res://image/EquIcon/InformIcon/icon-book.png";
            default:
                return "res://image/Debug/icon-information-3d.png";
        }
    }
/// <summary>
/// 根据设备的状态类型，设置不同的引用实例地址
/// </summary>
/// <param name="equStateProp">类型</param>
/// <returns></returns>
    private string _setEquScenePath(EquStateProp equStateProp)
    {
        switch (equStateProp)
        {
            case EquStateProp.Static:
                return SceneHelper.GetPackedPath("StaticEqu/StaticEqu");
            default:
                return SceneHelper.GetPackedPath("Debug/DebugScene");
        }

    }

//检测是否为第一次使用
    protected bool isFirstUse()
    {
        if (useCount == 0) {useCount++;return false;}
        else {useCount = 0;return true;}
    }
}
