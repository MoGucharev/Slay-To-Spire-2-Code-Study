using System;

namespace Game.src.Core.Localization.DynamicVar;

/// <summary>
/// 各种数值变量的基类,已经实现对变量数值的约束
/// </summary>
public class DynamicVar
{
//行动时的数值
    protected decimal _baseValue;
//预览按键的数值
    protected decimal _previewValue;
//数值名称
    protected string _name;

    public T Clone<T>(T var) where T:DynamicVar
    {
        T cost = var;
        return  cost;
    }


//数值类型

    public decimal BaseValue
    {
        get{ return _baseValue;}
        set
        { 
            _baseValue = Math.Min(value,9999999);
            ResetValue();
        }
    }

    public decimal PreviewValue
    {
        get{return _previewValue;}
        set{_previewValue = Math.Min(value,9999999);}
    }


//构造函数赋值
    public DynamicVar(decimal baseValue,string name)
    {
        BaseValue = baseValue;
        _name = name;
    }

//重置预览值
    public virtual void ResetValue()
    {
        PreviewValue = BaseValue;
    }



    public string ToString(IFormatProvider provider)
    {
        return GetBaseVarForProvider().ToString(provider);
    }

    public virtual decimal GetBaseVarForProvider()
    {
        return BaseValue;
    }
}