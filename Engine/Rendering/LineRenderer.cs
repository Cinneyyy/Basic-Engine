using System.Drawing;

namespace Engine.Rendering;

public class LineRenderer : Renderer
{
    public Vec2 from, to;
    public float width;
    public Color color
    {
        get => pen.Color;
        set => pen.Color = value;
    }

    private Pen pen;


    public LineRenderer(Vec2 from, Vec2 to, Color color, float width) : base()
    {
        this.from = from;
        this.to = to;
        this.width = width;
        pen = new(color, width);
    }


    public override void Draw(in Graphics graphics, float stwr)
    {
        pen.Width = width * stwr;
        var pos = drawPos;
        Vec2 newFrom = ApplyPosOffset(pos + from + yFlipVec / 2, Vec2.one * stwr);
        Vec2 newTo = ApplyPosOffset(pos + to + yFlipVec / 2, Vec2.one * stwr);
        graphics.DrawLine(pen, newFrom.x, newFrom.y, newTo.x, newTo.y);
    }
}