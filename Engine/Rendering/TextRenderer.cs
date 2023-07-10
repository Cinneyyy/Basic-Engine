using System.Drawing;

namespace Engine.Rendering;

public class TextRenderer : Renderer
{
    public string text;
    public Font font;
    public Color color
    {
        get => brush.Color;
        set => brush.Color = value;
    }
    public float fontSize { get; set; }

    private SolidBrush brush;

    private static readonly Vec2 scaleBase = Vec2.one / 20f;


    public TextRenderer(string text, Color color, float fontSize, string font = "Arial") : base()
    {
        this.text = text;
        this.fontSize = fontSize;
        this.font = new(new FontFamily(font), 1);
        brush = new(color);
    }


    public override void Draw(in Graphics graphics, float stwr)
    {
        Vec2 newScale = ApplyScreenScaling(scaleBase);
        Vec2 newPos = ApplyPosOffset(drawPos, newScale);

        font = new(font.Name, fontSize*newScale.x, font.Style);
        graphics.DrawString(text, font, brush, newPos.x, newPos.y);
        
        // TODO: Fix custom alignment
        /* if(customAlignment.TryGetValue(out var alignment))
            sfw::TextRenderer.DrawText(graphics, text, font, newPos, color, alignment.ToTextFormatFlags());
        else*/
    }
}