using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Game.src.Core.MyDebug;
using Godot;

namespace Game.src.Core.Localization;

public class LocalManager
{
    private string _localPath = "res://localization/";
    private string _fallbackName = "eng";
    private Dictionary<string,LocalTable> _translationTable = new();
    private Regex _regex = new Regex(@"\{([A-Za-z]\w*)(?::([^}]*))?\}");
    public static LocalManager Instance {get;private set;} = new LocalManager();

//初始化，设置各种翻译文件，可以拿来重置翻译文件
    public void Initialize(string localName)
    {
        _translationTable.Clear();
        _setTranslationTable(localName);
    }

/// <summary>
/// 获得本地化替换之后的字符串
/// </summary>
/// <param name="propName">文件名称</param>
/// <param name="key">要替换的key值，用于查找出原本的字符串</param>
/// <param name="objects">替换成的数组，第一个值是要替换的占位符的名称，第二个是要替换成的数值</param>
/// <returns></returns>
    public string GetTranslateText(string propName,string key,params (string name,object replacement)[] objects)
    {
        string _translationValue = _getTable(propName).GetRawText(key);
        if (_translationValue == "") throw new Exception($"无法找到{propName}表中{key}变量，英文回退也不存在");
        
        Dictionary<string,object> enterDic = new Dictionary<string, object>();
        foreach (var (name,replacement) in objects) enterDic[name] = replacement;

        return _regex.Replace(_translationValue,match =>
        {
            var matchTranslateValue = match.Groups[1].Value;
            if (!enterDic.TryGetValue(matchTranslateValue,out object? value)) return match.Value;
            return _toStringValue(value,match.Groups[2].Value);
        });
    }

/// <summary>
/// 将指定语言的每个翻译文件配置默认回退值，并放在_translationTable字典里。注意是每个翻译文件，按照文件夹顺序排列。
/// </summary>
/// <param name="localName">文件类型名称</param>
/// <returns></returns>
/// <exception cref="Exception">说明找不到文件夹</exception>
/// TODO 这里为了方便，我默认设置为中文了,之后做了启动顺序加载器再修改
    private void _setTranslationTable(string localName = "zhs")
    {
        string folderPath = _localPath+localName;
        var folder = DirAccess.Open(folderPath);
        if (folder == null) throw new Exception($"查找翻译{_localPath+localName}文件夹为空");
        string[] fileNames = folder.GetFiles();
        foreach (string fileName in fileNames)
        {
            string jsonString = ".json";
            string removedFileName = fileName[..^jsonString.Length];
            Dictionary<string,string> fileDic = _getDicInJson(localName,fileName);
            LocalTable fileTable = new LocalTable(localName,fileDic,_getFallBackFile(fileName));
            _translationTable[removedFileName] = fileTable;
        }
    }
/// <summary>
/// 从json中返回翻译文件字典
/// </summary>
/// <param name="localName">语言名称</param>
/// <param name="propName">文件夹中对应类型名称，比如设备的文件名称，UI翻译文件的名称</param>
/// <returns></returns>
/// <exception cref="Exception"></exception>
    private Dictionary<string,string> _getDicInJson(string localName,string propName)
    {
        string folderPath = _localPath+localName;
        string filePath = folderPath.PathJoin(propName);
        //将json转变为string
        string fileString = FileAccess.Open(filePath,FileAccess.ModeFlags.Read).GetAsText();
        return JsonSerializer.Deserialize<Dictionary<string,string>>(fileString)
            ?? new Dictionary<string, string>();
    }

/// <summary>
/// 返回指定的英文文件,将其作为LocalTable类型返回
/// </summary>
/// <param name="propName">指定文件名称</param>
/// <returns></returns>
    private LocalTable _getFallBackFile(string propName)
    {
        Dictionary<string,string> fallBackDic = _getDicInJson(_fallbackName,propName);
        return new LocalTable(_fallbackName,fallBackDic,null);
    }
    private LocalTable _getTable(string propName)
    {
        if (_translationTable.Count == 0) _setTranslationTable("zhs");
        if (_translationTable.TryGetValue(propName,out LocalTable value)) 
        return value;
        throw new Exception($"无法返回名称为{propName}的文件");
    }
/// <summary>
/// 两个作用：
/// -根据不同文化来转换要替代成的文本
/// -根据占位符：后的字符做出对应替换
/// </summary>
/// <param name="value"></param>
/// <param name="option"></param>
/// <returns></returns>
    private string _toStringValue(object value,string? option)
    {
        if (string.IsNullOrEmpty(option))
        {return Convert.ToString(value,CultureInfo.InvariantCulture) ?? "";}

        if (option.StartsWith("diff"))
        {
            //做完buff或者伤害增加后，再做伤害的显示更改
            return Convert.ToString(value,CultureInfo.InvariantCulture) ?? "";
        }


        return Convert.ToString(value,CultureInfo.InvariantCulture) ?? "";
    }
    
}