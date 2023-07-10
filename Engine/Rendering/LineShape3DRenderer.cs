using System;
using System.Drawing;

namespace Engine.Rendering;

[Obsolete("Broken atm")]
public class LineShape3DRenderer : Renderer
{
    public Pair<Vec3>[] lines;
    public float width;
    public Color color
    {
        get => pen.Color;
        set => pen.Color = value;
    }
    public Ref<float> camDistance;
    public float camDistVal => (float)camDistance;
    public float xRot, yRot;

    private Pen pen;


    public LineShape3DRenderer(Color color, float width, Ref<float> camDistance, params Pair<Vec3>[] lines) : base()
    {
        this.lines = lines;
        this.width = width;
        this.camDistance = camDistance;
        pen = new(color, width);
    }


    private void DrawLine(Vec3 from, Vec3 to, in Graphics graphics, float stwr)
    {
        Vec2 fromv2 = C_Toc(from), tov2 = C_Toc(to);

        var pos = drawPos;
        Vec2 newFrom = ApplyPosOffset(pos + fromv2 + yFlipVec / 2, Vec2.one * stwr);
        Vec2 newTo = ApplyPosOffset(pos + tov2 + yFlipVec / 2, Vec2.one * stwr);
        graphics.DrawLine(pen, newFrom.x, newFrom.y, newTo.x, newTo.y);
    }

    private float C_Z(float t, float r, float h)
        => xRot.Cos() * r * (yRot + t).Sin() - xRot.Sin() * h;

    private Vec2 C(float t, float r, float h)
    {
        float t1 = camDistVal - C_Z(t,r,h);
        float t2 = camDistVal / t1;

        float x = t2 * r * (yRot+t).Cos();
        float y = t2 * (r * xRot.Sin() * (yRot + t).Sin() + h * xRot.Sin());

        return new(x, t1 > 0 ? y : 0);
    }

    private Vec2 C_Toc(Vec3 v)
        => C(v.y.Atan2(v.x), (v.x.Squared() + v.y.Squared()).Sqrt(), v.z);


    public override void Draw(in Graphics graphics, float stwr)
    {
        pen.Width = width * stwr;
        foreach(var line in lines)
            DrawLine(line.a, line.b, in graphics, stwr);
    }
}