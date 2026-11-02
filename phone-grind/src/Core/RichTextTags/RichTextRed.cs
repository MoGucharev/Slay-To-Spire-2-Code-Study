using Godot;

namespace Game.src.Core.RichTextTags;

public partial class RichTextRed : AbstractRichTextEffect
{
    public RichTextRed()
        :base("Red")
    {
        
    }

    public override bool _ProcessCustomFX(CharFXTransform charFX)
    {
        charFX.Color = new Color(1.0f,0.0f,0.0f);
        return true;
    }


}