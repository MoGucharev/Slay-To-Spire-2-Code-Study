using System;
using Game.src.Core.model;
using Game.src.Core.Node.Pooling;
using Game.src.Core.RichTextTags;
using Game.src.Core.Helper;
using Godot;
using Game.src.Core.Localization.DynamicVar;

namespace Game.src.Core.Node.Equ.NEquNamespace;



/// <summary>
/// 所有显示层设备的基类，包括静态和实例
/// </summary>
public abstract partial class NEqu<T>: Control,IPoolable where T : EquipmentBase
{
    protected T _equipmentData;
    protected TextureRect _iconTexture;
    protected TextureRect _actionTexture;
    protected MyRichTextLable _actionNum;
    protected MyRichTextLable _cdNum;
    protected Control _VFX;
    protected Control _overlayVFX;

    public T EquipmentData
    {
        get => _equipmentData;
        set
        {
            _equipmentData = value;
            Reload();
        }
    }
    public override void _Ready()
    {
        CacheNodeAdd();
        CacheNodeBase();
        Reload();
    }
 
/// <summary>
/// 写子类时必须写的内容，通过这些内容可以实现：
/// -写子类时提醒，记得将内容写里面
/// -加载资源由基类统一调控，子类不必再写Ready或者在资源中调用set来单独设置
/// </summary>
    protected abstract void ReloadAdd();
    protected abstract void CacheNodeAdd();
    protected void Reload()
    {
        if (!IsNodeReady())
            return;
        ReloadBase();
        ReloadAdd();
    }
/// <summary>
/// 覆写的版本没有节点是否能用的检查，只拿来管覆写。
/// </summary>
    protected virtual void ReloadBase()
    {
        UpdateVFX();
        UpdateIconTexture();
        UpdateActionTexture();
        UpdateActionNum();
        UpdateCDNum();
        
    }

    protected void CacheNodeBase()
    {
        _iconTexture = GetNode<TextureRect>("%IconTexture");
        _actionTexture = GetNode<TextureRect>("%ActionContainer/Texture");
        _actionNum = GetNode<MyRichTextLable>("%ActionContainer/RichTextLabel");
        _cdNum = GetNode<MyRichTextLable>("%CDContainer/RichTextLabel");
        _VFX = GetNode<Control>("VFX");
    }


/// <summary>
/// 公有方法，外部调用接口，如果想更新数据，可以从这里更新
/// </summary>

    public virtual void UpdateVisual()
    {
        if (!IsNodeReady())
            return;
        UpdateActionNum();
        UpdateCDNum();
    }
//协议内容
    public void OnInstantiate()
    {
        
    }
    public void OnReturnFromPool()
    {
        
    }
    public void OnFreeToPool()
    {
        EquipmentData = null;
    }
//私有方法
    protected virtual void UpdateVFX()
    {
        if (_overlayVFX != null)
        {
            _VFX.RemoveChild(_overlayVFX);
            _overlayVFX.QueueFree();
            _overlayVFX = null;
        }
        _overlayVFX = EquipmentData.SetVFX();

        if (_overlayVFX != null)
        {
            _VFX.AddChild(_overlayVFX);
        }
    }

    protected virtual void UpdateIconTexture()
    {
        if (EquipmentData == null) _iconTexture.Texture = null;
        _iconTexture.Texture =  GD.Load<Texture2D>(EquipmentData.IconTexture);
    }
    protected virtual void UpdateActionTexture()
    {
        if (EquipmentData == null) _actionTexture.Texture = null;
        _actionTexture.Texture =  GD.Load<Texture2D>(EquipmentData.ActionTexture);
    }
    protected virtual void UpdateActionNum()
    {
        if (EquipmentData == null) _actionNum.Text = "";
        _actionNum.Text = EquipmentData.ActionValue.BaseValue.ToString();
    }
    protected virtual void UpdateCDNum()
    {
        if (EquipmentData == null) _cdNum.Text = "";
        _cdNum.Text = EquipmentData.CDVar.BaseValue.ToString();
    }

}