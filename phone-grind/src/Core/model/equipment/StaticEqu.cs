using System;
using Game.src.Core.Localization.DynamicVar;
using Game.src.Core.Entities.equipment;
using System.Security.Cryptography.X509Certificates;

namespace Game.src.Core.model;

public interface IStaticEqu
{
    
}
public abstract class StaticEqu : EquipmentBase
{
//显示名称
    public string Name;
//购买栏里的描述
    public string Description;
//购买花费的钱
    public CostVar CostVar;

    public override void DeepClone()
    {
        base.DeepClone();
        CostVar = CostVar.Clone<CostVar>(CostVar);
    }

    public abstract void OnPlay();


}