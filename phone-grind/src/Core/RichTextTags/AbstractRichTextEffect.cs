using System;
using Godot;

namespace Game.src.Core.RichTextTags;



public abstract partial class AbstractRichTextEffect : RichTextEffect
{
    public string bbcode;
    protected AbstractRichTextEffect(string colorName)
    {
        bbcode = colorName;
    }

    protected static bool effectEnable = true;

    public abstract override bool _ProcessCustomFX(CharFXTransform charFX);
}


