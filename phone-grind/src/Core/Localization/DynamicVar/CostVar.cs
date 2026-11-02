using System;

namespace Game.src.Core.Localization.DynamicVar;

/// <summary>
/// 购买设备所需要的钱，实现约束
/// </summary>
public class CostVar : DynamicVar
{
    public const string defaultName = "Cost";

    public CostProp Prop;

    public CostVar(decimal costVar, CostProp prop)
        :base(costVar,defaultName)
    {
        Prop = prop;
    }
    public CostVar(decimal costVar,string name, CostProp prop)
        :base(costVar,name)
    {
        Prop = prop;
    }

//代表复制了一个新的原值，和之前的不一样。这里省略了STS2里变量认主的步骤
}