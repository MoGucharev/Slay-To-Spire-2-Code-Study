using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using Godot;

namespace Game.src.Core.Localization;

public class LocalTable
{
    private string _languageName;
    private Dictionary<string,string> _translation;
    private LocalTable _fallback;

    public LocalTable(string languageName,Dictionary<string,string> translation,LocalTable fallback)
    {
        _languageName = languageName;
        _translation = translation;
        _fallback = fallback;
    }

//检测某个key是否已经输入
    public bool HasEntry(string key)
    {
        return _translation.ContainsKey(key) || (_fallback?.HasEntry(key) ?? false);
    }

//查找表中key对应的值，如果没有则回退，如果没有英文表则自动报错
    public string GetRawText(string key)
    {
        if (_translation.TryGetValue(key,out string? value)){ return value ;}
        if (_fallback is not null) return _fallback.GetRawText(key);
        throw new KeyNotFoundException($"没找到{key}在{_fallback}表里");
    }

}