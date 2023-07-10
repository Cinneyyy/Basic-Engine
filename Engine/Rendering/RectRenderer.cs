using System.Drawing;

namespace Engine.Rendering;

public class RectRenderer : Renderer
{
    public Vec2 size;
    public bool outlineOnly;
    public float outlineThiccness;

    private SolidBrush brush;
    private Pen pen;


    public RectRenderer(Color color, Vec2 pos, Vec2 size)
    {
        brush = new(color);
        pen = new Pen(color);
        offset = pos;
        this.size = size;
    }

    public RectRenderer(Color color, Vec2 pos, Vec2 size, float outlineThiccness) : this(color, pos, size)
    {
        outlineOnly = true;
        this.outlineThiccness = outlineThiccness;
    }

    public RectRenderer(Color color, float x, float y, float width, float height) : this(color, x, y, width, height, false, 0f) { }

    public RectRenderer(Color color, float x, float y, float width, float height, float outlineThiccness) : this(color, x, y, width, height, true, outlineThiccness) { }

    public RectRenderer(Color color, float x, float y, float width, float height, bool outlineOnly, float outlineThiccness) : base()
    {
        brush = new(color);
        pen = new(color);
        offset = new(x, y);
        size = new(width, height);
        this.outlineOnly = outlineOnly;
        this.outlineThiccness = outlineThiccness;
    }


    public override void Draw(in Graphics graphics, float stwr)
    {
        Vec2 newSize = ApplyScreenScaling(size - outlineThiccness);
        Vec2 newPos = ApplyPosOffset(drawPos, newSize);

        if(outlineOnly)
        {
            pen.Width = outlineThiccness * stwr;
            graphics.DrawRectangle(pen, newPos.x, newPos.y, newSize.x, newSize.y);
        }
        else
            graphics.FillRectangle(brush, newPos.x, newPos.y, newSize.x, newSize.y);
    }
}