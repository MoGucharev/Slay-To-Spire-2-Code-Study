using Godot;

namespace Game.src.Core.Helper;

public static class SceneHelper
{
    /// <summary>
    /// 获得完整的场景路径
    /// </summary>
    /// <param name="shortPath">到scene为止，并去掉.tscn后缀的场景文件路径</param>
    /// <returns></returns>
    public static string GetPackedPath(string shortPath)
    {
        if (shortPath.StartsWith("/"))
        {
            string text = shortPath;
            shortPath = text.Substring(1,shortPath.Length);
        }
        string path = "res://scene/" + shortPath + ".tscn";
        return path;
    }

/// <summary>
/// 返回场景文件
/// </summary>
/// <param name="shortPath">到scene为止，并去掉.tscn后缀的场景文件路径</param>
/// <returns></returns>
    public static PackedScene GetPackedScene(string shortPath)
    {
        string path = GetPackedPath(shortPath);
        return ResourceLoader.Load<PackedScene>(path);
    }


    public static T GetInstant<T>(string shortPath) where T : Godot.Node
    {
        return GetPackedScene(shortPath).Instantiate<T>();
    }
}