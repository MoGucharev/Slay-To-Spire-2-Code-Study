using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public partial class Main : Control
{
    private Button button;
    private ProgressBar progressBar;
    private HFlowContainer _hbox;
    private int _loadNum = 0;
    private List<string> imagePaths = new();
    [Export]private string _targetShortPath = "assets/icons/";

    private int LoadNum
    {
        get => _loadNum;
        set{_loadNum = Mathf.Min(value,GetFileTexturesCount(_targetShortPath));}
    }

    public override void _Ready()
    {
        base._Ready();
        button = GetNode<Button>("Button");
        progressBar = GetNode<ProgressBar>("ProgressBar");
        _hbox = GetNode<HFlowContainer>("HFlowContainer");
        button.Pressed += _OnButtonPressed;
    }

    private async void _OnButtonPressed()
    {
        LoadNum = 0;
        button.Disabled = false;
        progressBar.MaxValue = GetFileTexturesCount(_targetShortPath);
        await Task.Run(_multipleLineCallable);

    }

    private void _updateUI(int num)
    {
        progressBar.Value = num;
    }

    private void _multipleLineCallable()
    {
        Callable.From(() => {_=AddTexture();}).CallDeferred();
    }

    private async Task AddTexture()
    {
        foreach (string path in imagePaths)
        {
            Texture2D texture = ResourceLoader.Load<Texture2D>(path);
            TextureRect textureRect = GetTextureRect("res://testMultipleLine/Texture.tscn");
            textureRect.Texture = texture;
            _hbox.AddChild(textureRect);
            LoadNum += 1;
            _updateUI(LoadNum);
            await ToSignal(GetTree(),SceneTree.SignalName.PhysicsFrame);
        }

    }

/// <summary>
/// 将图片文件全部放到列表里
/// </summary>
/// <param name="shortPath"></param>
/// <param name="IsSkipTargetPath">如果为true，则忽略你输入的地址所承载的文件夹里的图片</param>
    private void GetImagePathCycle(string shortPath,bool IsSkipTargetPath)
    {
        string fullPath = "res://import/" + shortPath;
        foreach (string name in DirAccess.GetFilesAt(fullPath))
        {
            if (IsSkipTargetPath) return;
            if (name.EndsWith(".png"))
            {
                imagePaths.Add(fullPath.PathJoin(name));
            }
        }
        foreach (string dicName in DirAccess.GetDirectoriesAt(fullPath))
        {
            GetImagePathCycle(shortPath + dicName +"/",false);
        }
    }

    private TextureRect GetTextureRect(string path)
    {
        return (TextureRect)ResourceLoader
            .Load<PackedScene>(path)
            .Instantiate();
    }

//使用带有图片的文件夹的路径，来获取图片路径
//废弃方法，之前调用过，改进后不需要此方法
//这里放着是因为它有一些可以学的地方
    private string[] GetFileTexturesPath(string shortPath)
    {
        string fullPath = "res://import/" + shortPath;
        string[] pngPaths = DirAccess.GetFilesAt(fullPath)
            .Where(f => f.EndsWith(".png"))
            .Select(f => fullPath + f)
            .ToArray();
        return pngPaths;
    }

    private int GetFileTexturesCount(string shortPath)
    {
        if (imagePaths.Count == 0) 
            GetImagePathCycle(shortPath,true);
        return imagePaths.Count();
    }

}
