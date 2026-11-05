using System.Collections.Generic;
using System.Linq;
using Game.src.Core.Node.Equ.NEquNamespace;


namespace Game.src.Core.Entities;

public static class TempomraryHookDb
{
    public static List<NStaticEqu> nStaticEqus = new();

/// <summary>
/// 将指定StaticEqu加入Hook检索的数据库中，如果返回false则说明已经加入过了。
/// </summary>
/// <param name="nStaticEqu"></param>
/// <returns></returns>
    public static bool JoinEquDb(NStaticEqu nStaticEqu)
    {
        if (nStaticEqus.Contains(nStaticEqu)) return false;
        nStaticEqus.Add(nStaticEqu);
        return true;
    }


}