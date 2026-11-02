using Godot;
using Game.src.Core.Helper;
using Game.src.Core.Node.Pooling;

namespace Game.src.Core.Helper;

public static class GodotTreeExtensions
{
    public static void QuereFreeSafe(this Godot.Node node)
    {
        if (!GodotObject.IsInstanceValid(node))return;
        if (!NodePool.GetPoolable(node.GetType(),out IPoolable poolable1))
        {
            node.QueueFree();
        }
        else
        {
            INodePool nodePool = NodePool.GetPool(node.GetType());
            nodePool.Free(poolable1);
        }
    }
}