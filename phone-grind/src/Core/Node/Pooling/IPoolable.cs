using System;

namespace Game.src.Core.Node.Pooling;
/// <summary>
/// 对象创建时需要继承的
/// </summary>
public interface IPoolable
{
//第一次实例化
    void OnInstantiate();
//再次调用
    void OnReturnFromPool();
//返回对象池
    void OnFreeToPool();
    
}