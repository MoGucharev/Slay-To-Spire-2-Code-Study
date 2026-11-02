using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

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
        Type[] typeValue = types ?? _allEquipmentType;
        foreach (Type type in typeValue)
        {
            ModelId modelId = GetModelId(type);
            EquipmentBase equipmentBase = (EquipmentBase)Activator.CreateInstance(type);
            EquipmentDb[modelId] = equipmentBase;
        }
    }

    public static T Get<T>() where T : EquipmentBase
    {
        return (T)EquipmentDb[GetModelId<T>()];
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
//
    public static bool TryGetIdFromDb(Type type)
    {
        ModelId id = GetModelId(type);
        if (EquipmentDb[id] == null) return false;
        else return true;
    }

}