using System.Collections.Generic;
using System;
using Game.src.Core.model.equipment;

namespace Game.src.Core.model;

public static class EquipmentBaseSubType
{
    public static Type t1 = typeof(StaticSword);
    public static Type t2 = typeof(StaticShield);

    private static readonly Type[] _allTypes = new Type[2]
    {
        t1,t2,
    };

    public static IReadOnlyList<Type> GetAllType => _allTypes;
}