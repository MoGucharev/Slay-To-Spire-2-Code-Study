using Godot;

namespace Game.src.Core.RichTextTags;

public partial class RichTextGold : AbstractRichTextEffect
{
    public RichTextGold()
        :base("Gold")
    {
        
    }

    public override bool _ProcessCustomFX(CharFXTransform charFX)
    {
        charFX.Color = new Color(1.0f, 0.843f, 0.0f);
        return true;
    }
}