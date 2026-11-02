using System;
using Godot;

namespace Game.src.Core.RichTextTags;

/// <summary>
/// 记得给对应节点挂上此脚本
/// -能使用自己创建的颜色标识的RichTextLable
/// </summary>
public partial class MyRichTextLable : RichTextLabel
{
    public override void _Ready()
    {
        BbcodeEnabled = true;
        var fx = new Godot.Collections.Array {
            new RichTextGold(),
            new RichTextRed(),
            new RichTextGreen()
            };
        CustomEffects = fx;
    }
}