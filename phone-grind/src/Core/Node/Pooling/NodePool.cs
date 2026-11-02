using System;
using System.Collections.Generic;
using Game.src.Core.Assets;

namespace Game.src.Core.Node.Pooling;

/// <summary>
/// 对象池创建时需要继承的协议
/// </summary>
public interface INodePool
{
    public IPoolable Get();
    public void Free(IPoolable poolable);
}
/// <summary>
/// 一系列的静态方法，用来管理NodePool
/// </summary>
public class NodePool
{
    private static readonly Dictionary<Type,INodePool> _totalpool = new();
    
    public static NodePool<T> Init<T>(string scenePath,int num) where T : Godot.Node,IPoolable
    {
        Type t = typeof(T);
        if (_totalpool.ContainsKey(t))
            throw new Exception ($"重复创建对象池，已经有{t}对象池了");
        NodePool<T> pool = new NodePool<T>(scenePath,num);
        _totalpool[t] = pool;
        return pool;
    }
/// <summary>
/// 返回对象池中对应的一个对象，如果此对象池不存在，则返回false，对象返回null；如果对象存在则返回true。
/// </summary>
/// <param name="type">对象类型</param>
/// <param name="poolable">返回的对象，如果不存在则为null</param>
/// <returns></returns>
    public static bool GetPoolable(Type type,out IPoolable poolable)
    {
        INodePool nodePool = GetPool(type);
        if (nodePool == null) 
        {
            poolable = null;
            return false;
        }
        poolable = nodePool.Get();
        return true;
    }


    public static INodePool GetPool(Type type)
    {
        if (_totalpool.TryGetValue(type,out INodePool value))
        {
            return value;
        }
        else return null;
    }
    public static bool FreePool(Type type)
    {
        return _totalpool.Remove(type);
    }
}

/// <summary>
/// nodePool实体，用这个来创建对象池
/// </summary>
/// <typeparam name="T">对象池类型</typeparam>
public class NodePool<T> : INodePool where T : Godot.Node, IPoolable
{
    private string _scenePath;
    private List<T> _freePool = new();
    private HashSet<T> _usedPool = new();

    public NodePool(string path,int num)
    {
        _scenePath = path;
        for (int i = 0; i < num; i++)
        {
            _freePool.Add(Instantiate());
        }
    }

    public IPoolable Get()
    {
        T instant;
        if (_freePool.Count > 0)
        {
            int index = _freePool.Count - 1;
            instant = _freePool[index];
            _freePool.RemoveAt(index);
        }
        else
        {
            instant = Instantiate();
        }
        instant.OnReturnFromPool();
        _usedPool.Add(instant);
        return instant;
    }

    public void Free(T obj)
    {
        if (!_usedPool.Remove(obj)) return;
        obj.OnFreeToPool();
        _freePool.Add(obj);
    }
 
//声明遵从的INodePool协议类型
    IPoolable INodePool.Get() => Get();
    void INodePool.Free(IPoolable poolable) => Free((T)poolable);

// 将先将场景缓存在Cache里，之后再实例化
    private T Instantiate()
    {
        T instant;
        instant = PreloadManager.Cache.GetPackedScene(_scenePath).Instantiate<T>();
        instant.OnInstantiate();
        return instant;
    }
}