using Godot;

namespace Game.src.Core.RichTextTags;

public partial class RichTextGreen : AbstractRichTextEffect
{
    public RichTextGreen()
        :base("Green")
    {
        
    }

    public override bool _ProcessCustomFX(CharFXTransform charFX)
    {
        charFX.Color = new Color(0.0f, 1.0f, 0.0f);
        return true;
    }
}