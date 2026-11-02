using System;

namespace Game.src.Core.Localization.DynamicVar;


/// <summary>
/// -Move,可以改变的，即正常类型
/// -UnChanged,不可改变，一些敌人的debuff等
/// </summary>
public enum CostProp
{
    Move,

    UnChanged,
}