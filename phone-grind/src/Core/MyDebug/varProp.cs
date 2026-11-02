using System;
using Godot;
using System.Collections.Generic;
using Game.src.Core.Localization;

namespace Game.src.Core.MyDebug;

public static class gDebug
{
    public static void CheckDic<Tkey,Tvalue>(
        IEnumerable<KeyValuePair<Tkey,Tvalue>> dic
    )
    {
        foreach (var ky in dic )
        {
            GD.Print($"键{ky.Key}:值{ky.Value}");
        }
    }
}