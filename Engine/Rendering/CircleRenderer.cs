using System.Drawing;

namespace Engine.Rendering;

public class CircleRenderer : Renderer
{
    public float radius;

    private SolidBrush brush;


    public CircleRenderer(Color color, float x, float y, float radius) : base()
    {
        brush = new(color);
        globalPos = new(x, y);
        this.radius = radius;
    }


    public override void Draw(in Graphics graphics, float stwr)
    {
        Vec2 newSize = ApplyScreenScaling(Vec2.one * radius);
        Vec2 newPos = ApplyPosOffset(globalPos, newSize);

        graphics.FillEllipse(brush, newPos.x, newPos.y, newSize.x, newSize.y);
    }
}