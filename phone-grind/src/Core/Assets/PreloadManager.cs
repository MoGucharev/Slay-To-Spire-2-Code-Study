using Godot;
using System.Collections.Concurrent;

namespace Game.src.Core.Assets;

public class PreloadManager
{
    public static AssetCache Cache {get;} = new AssetCache();
}

public class AssetCache
{
    private readonly ConcurrentDictionary<string,Resource> _cache = new ConcurrentDictionary<string, Resource>();

    private Resource GetAssetCache(string path)
    {
        if (_cache.TryGetValue(path,out Resource resource))
        {
            if (GodotObject.IsInstanceValid(resource))
            {
                return resource;
            }
             _cache[path] = ResourceLoader.Load<Resource>(path);
            return _cache[path];
        }
        _cache[path] = ResourceLoader.Load<Resource>(path);
        return _cache[path];
    }

    public PackedScene GetPackedScene(string path)
    {
        return (PackedScene)GetAssetCache(path);
    }

}
