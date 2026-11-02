using System;

namespace Game.src.Core.Localization.DynamicVar;


/// <summary>
/// 攻击的CD间隔，继承于DynamicVar，实现对数值的约束
/// </summary>
public class CDVar : DynamicVar
{
    public const string defaultName = "CDDamage";

    public CDProp Prop;

    public CDVar(decimal cdTime, CDProp prop)
        :base(cdTime,defaultName)
    {
        Prop = prop;
    }
    public CDVar(decimal cdTime,string name, CDProp prop)
        :base(cdTime,name)
    {
        Prop = prop;
    }
}