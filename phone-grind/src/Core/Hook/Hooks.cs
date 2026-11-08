
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.src.Core.Entities;
using Game.src.Core.model;
using Game.src.Core.Node.Equ.NEquNamespace;
using Game.src.Core.Node.Pooling;
using Godot;

namespace Game.src.Core.Hook;

public static class Hooks
{
    //我的获取Hook实例的方法：通过查看那些对象的Hook已经覆写，将其加入到List里
    //杀戮尖塔的Hook获取方法：所有玩家有的卡牌，遗物，力量加成等注册各自的列表，由一个方法遍历所有列表，将其放到一个List里，在这里使用
    //本项目不做杀戮尖塔的方法，只做出占位符
    private static List<IPoolable> _poolables = new();

    private static void SetPoolables()
    {
        foreach (IPoolable poolable in TempomraryHookDb.nStaticEqus)
        {
            _poolables.Add(poolable);
        }
    } 

    public static void InitHooks()
    {
        SetPoolables();
    }

    public static async Task BeforeEquPlayed(EquipmentBase transformer)
    {
        SpreadAll(transformer);
        GD.Print("钩子BeforeEquPlayed");
    }

    public static async Task AfterEquPlayed()
    {
        
        GD.Print("钩子AfterEquPlayed");
    }
    public static async Task BeforeDamageApplied()
    {
        GD.Print("钩子BeforeDamageApplied");
    }
    public static async Task AfterDamageApplied()
    {
        GD.Print("钩子AfterDamageApplied");
    }
    public static async Task BeforeAttackApplied()
    {
        GD.Print("钩子BeforeAttackApplied");
    }
    public static async Task AfterAttackApplied()
    {
        GD.Print("钩子AfterAttackApplied");
    }

//私有
    private static void SpreadAll(EquipmentBase transformer)
    {
    foreach (NStaticEqu poolable in _poolables)
        {
            poolable.StaticEquData.BeforeEquPlayed(transformer);
            poolable.UpdateVisual();
        }

    }
}