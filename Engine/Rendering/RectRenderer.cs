using System.Drawing;

namespace Engine.Rendering;

public class RectRenderer : Renderer
{
    public Vec2 size;
    public bool outlineOnly;
    public float outlineThiccness;

    private SolidBrush brush;
    private Pen pen;


    public RectRenderer(Color color, float x, float y, float width, float height) : this(color, x, y, width, height, false, 0f) { }

    public RectRenderer(Color color, float x, float y, float width, float height, float outlineThiccness) : this(color, x, y, width, height, true, outlineThiccness) { }

    public RectRenderer(Color color, float x, float y, float width, float height, bool outlineOnly, float outlineThiccness) : base()
    {
        brush = new(color);
        pen = new(color);
        globalPos = new(x, y);
        size = new(width, height);
        this.outlineOnly = outlineOnly;
        this.outlineThiccness = outlineThiccness;
    }


    public override void Draw(in Graphics graphics, float stwr)
    {
        Vec2 newSize = ApplyScreenScaling(size);
        Vec2 newPos = ApplyPosOffset(globalPos, newSize);

        if(outlineOnly)
            graphics.DrawRectangle(pen, newPos.x, newPos.y, newSize.x, newSize.y);
        else
            graphics.FillRectangle(brush, newPos.x, newPos.y, newSize.x, newSize.y);
    }
}