using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Godot;

namespace Game.src.Core.model;
/// <summary>
/// 数据的数据库
/// </summary>
public static class ModelDb
{
    //设备的类型列表
    private static Type[] _allEquipmentType;
    public static readonly Dictionary<ModelId,EquipmentBase> EquipmentDb = new();
    public static Type[] AllEquipmentType
    {
        get
        {
            if (_allEquipmentType != null) 
            {
                return _allEquipmentType;
            }
            HashSet<Type> types = new HashSet<Type>();
            foreach(Type type in EquipmentBaseSubType.GetAllType)
            {
                types.Add(type);
            }
            _allEquipmentType = types.ToArray();
            return _allEquipmentType;
        }
    }

    public static void InitEquDb(Type[] types = null)
    {
        Type[] typeValue = types ?? AllEquipmentType;
        foreach (Type type in typeValue)
        {
            ModelId modelId = GetModelId(type);
            EquipmentBase equipmentBase = (EquipmentBase)Activator.CreateInstance(type);
            EquipmentDb[modelId] = equipmentBase;
        }
    }
/// <summary>
/// 外部获取数据库中数据原件的方法
/// </summary>
/// <typeparam name="T">数据类型</typeparam>
/// <returns></returns>
    public static T Get<T>() where T : EquipmentBase
    {
        return (T)EquipmentDb[GetModelId<T>()];
    } 
/// <summary>
/// 外部获取数据库中数据复印件的方法，一般使用这个
/// </summary>
/// <typeparam name="T"></typeparam>
/// <returns></returns>
    public static T GetMutable<T>() where T : EquipmentBase
    {
        T equipment = Get<T>();
        return (T)equipment.CloneEqu();
    }

    public static ModelId GetModelId<T>()
    {
        return GetModelId(typeof(T));
    }

/// <summary>
/// 获得新的ModelId
/// </summary>
/// <param name="type">变量类型</param>
/// <returns></returns>
    public static ModelId GetModelId(Type type)
    {
        return new ModelId(GetCategory(type),GetEntry(type));
    }
    
    public static Type GetCategoryType(Type type)
    {

        while (type !=  typeof(EquipmentBase))
        {
            type = type.BaseType;
        }
        return type;
    }

    public static string GetCategory(Type type) => GetCategoryType(type).ToString();
    public static string GetEntry(Type type) => type.ToString();
//查看库里是否有类型
    public static bool TryGetIdFromDb(Type type) => 
    EquipmentDb.TryGetValue(GetModelId(type),out EquipmentBase equipmentBase) && equipmentBase != null;

}