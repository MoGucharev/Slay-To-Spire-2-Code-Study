using System;

namespace Game.src.Core.Localization.DynamicVar;

/// <summary>
/// 伤害类型
/// -Move:可以通过buff等提升的正常类型
/// -UnMove:无法提升的类型
/// -UnDefend: 无法通过防御等抵消的类型
/// </summary>

[Flags]
public enum DamageProp
{
    Move = 2,
    UnMove = 4,
    UnDefend = 8,

}

public static class DamageMixProp
{
    public const DamageProp ConstThoughDamage = DamageProp.UnMove | DamageProp.UnDefend;
    public const DamageProp ChangeThoughDamage = DamageProp.Move | DamageProp.UnDefend;
}