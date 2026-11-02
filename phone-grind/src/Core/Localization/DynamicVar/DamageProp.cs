using System;

namespace Game.src.Core.Localization.DynamicVar;

/// <summary>
/// 伤害类型
/// -Move:可以通过buff等提升的正常类型
/// </summary>

[Flags]
public enum DamageProp
{
    Move = 2

}