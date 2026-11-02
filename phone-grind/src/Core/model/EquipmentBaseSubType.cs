using System.Collections.Generic;
using System;

namespace Game.src.Core.model;

public static class EquipmentBaseSubType
{
    public static Type t1 = typeof(StaticEqu);

    private static readonly Type[] _allTypes = new Type[1]
    {
        t1,
    };

    public static IReadOnlyList<Type> GetAllType => _allTypes;
}