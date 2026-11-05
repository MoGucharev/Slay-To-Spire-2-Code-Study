using System;


namespace Game.src.Core.Localization.DynamicVar;

public class DamageVar : DynamicVar
{
    public const string defaultName = "Damage";
//数值类型
    public DamageProp Prop {get;}

//构造函数重新赋值
    public DamageVar(decimal damage,DamageProp prop)
        : base(damage, defaultName)
    {
        Prop = prop;
    }

    public DamageVar(decimal damage,string name,DamageProp prop)
        : base(damage,name)
    {
        Prop = prop;
    }

}