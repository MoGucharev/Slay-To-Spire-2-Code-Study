using System;

namespace Game.src.Core.Localization.DynamicVar;

/// <summary>
/// CD的种类
/// -Move 可以改变的类型
/// -UnChanged 无法改变的类型，比如敌方施加的debuff
/// </summary>
[Flags]
public enum CDProp
{
    Move = 2,
    UnChanged = 4,

}