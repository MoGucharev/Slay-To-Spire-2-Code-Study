using Game.src.Core.RichTextTags;
using Game.src.Core.model;
using Game.src.Core.Node.Pooling;
using Game.src.Core.Helper;
using Godot;


namespace Game.src.Core.Node.Equ.NEquNamespace;

public partial class NStaticEqu : NEqu<StaticEqu>
{
//定义一系列节点
    protected MyRichTextLable _name;
    protected MyRichTextLable _description;
    protected MyRichTextLable _costNum;

    public static void InitPool()
    {
        NodePool.Init<NStaticEqu>(SceneHelper.GetPackedPath("StaticEqu/StaticEqu"),10);
    }

    public StaticEqu StaticEquData
    {
        get => _equipmentData;
        set
        { 
            EquipmentData = value;; 
        }
    }
    protected override void ReloadAdd()
    {
        UpdateName();
        UpdateDescription();
        UpdateCostNum();
    }
    protected override void CacheNodeAdd()
    {
        _name = GetNode<MyRichTextLable>("%Name");
        _description = GetNode<MyRichTextLable>("%Description");
        _costNum = GetNode<MyRichTextLable>("%CostContainer/RichTextLabel");
    }

/// <summary>
/// 外部调用，更新所有数值的方法
/// </summary>
    public override void UpdateVisual()
    {
        base.UpdateVisual();
        UpdateCostNum();
    }


    protected void UpdateName()
    {
        if (StaticEquData == null) _name.Text = "";
        _name.Text = StaticEquData.Name;
    }
    protected void UpdateDescription()
    {
        if (StaticEquData == null) _description.Text = "";
        _description.Text = StaticEquData.Description;
    }

    protected void UpdateCostNum()
    {
        if (StaticEquData == null) _costNum.Text = "";
        _costNum.Text = StaticEquData.CostVar.BaseValue.ToString();
    }
}
