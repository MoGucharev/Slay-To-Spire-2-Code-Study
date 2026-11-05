using Game.src.Core.Node.Equ.NEquNamespace;
using  Game.src.Core.Node.Pooling;
using Game.src.Core.model;
using Game.src.Core.model.equipment;

using Godot;

namespace Game.src.Core.Node.Equ.Holders;

public partial class StaticEquHolder : Control
{
    [Signal] public delegate void PressedEventHandler (StaticEquHolder staticEquHolder);
//悬停放大数据
    protected Vector2 _normalScale =  Vector2.One;
    protected Vector2 _bigScale = Vector2.One * 1.3f;
    protected bool _isHover;
    protected bool _isClick;

    public NStaticEqu NStaticEqu{get;protected set;}
    public Button HitBox {get;private set;}

    public override void _Ready()
    {
        base._Ready();
        HitBox = GetNode<Button>("Button");
        _connectSignal();
    }

//将视觉层放到此层上
    public virtual void SetEqu(NStaticEqu nStaticEqu)
    {
        if (NStaticEqu != null) throw new System.Exception("此操控层已经有视觉节点了");
        NStaticEqu = nStaticEqu;
        if (nStaticEqu.GetParent() == null) 
        {
            AddChild(nStaticEqu);
        }
        else
        {
            nStaticEqu.Reparent(this); 
            AddChild(nStaticEqu);
        }

    }


//私有调用方法

    protected void _connectSignal()
    {
        HitBox.MouseEntered += _OnHover;
        HitBox.MouseExited += _UnHover;
        HitBox.ButtonDown += _OnClick;
        HitBox.ButtonUp += _UnClick;
    }
    private void _OnHover()
    {
        _isHover = true;
        _changeScale(_isHover);
    }

    private void _UnHover()
    {
        _isHover = false;
        _changeScale(_isHover);
    }

    private void _OnClick()
    {
        _isClick = true;
        NStaticEqu.StaticEquData.OnPlay();
    }

    private void _UnClick()
    {
        _isClick = false;
        EmitSignal(SignalName.Pressed,this);
    }


    private void _changeScale(bool isBig)
    {
        Scale = isBig ? _bigScale : _normalScale;
    }


}