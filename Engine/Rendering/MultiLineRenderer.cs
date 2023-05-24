using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Engine.Rendering;

public class MultiLineRenderer : Renderer
{
    public List<Vec2> points;
    public float width;
    public Color color
    {
        get => pen.Color;
        set => pen.Color = value;
    }
    public bool closed;

    private Pen pen;


    public MultiLineRenderer(Color color, float width, bool closed, params Vec2[] points) : base()
    {
        this.closed = closed;
        this.width = width;
        this.points = points.ToList();
        pen = new(color, width);
    }


    public void AddPoint(Vec2 location)
        => points.Add(location);


    public override void Draw(in Graphics graphics, float stwr)
    {
        pen.Width = width * stwr;

        Vec2 stwrVec = Vec2.one * stwr;
        Vec2 preOffset = globalPos + yFlipVec / 2;
        int pointCount = points.Count-1;
        for(int i = 0; i < pointCount; i++)
        {
            Vec2 from = ApplyPosOffset(points[i] + preOffset, stwrVec);
            Vec2 to = ApplyPosOffset(points[i+1] + preOffset, stwrVec);
            graphics.DrawLine(pen, from.x, from.y, to.x, to.y);
        }

        if(closed && pointCount >= 1)
        {
            Vec2 from = ApplyPosOffset(points[^1] + preOffset, stwrVec);
            Vec2 to = ApplyPosOffset(points[0] + preOffset, stwrVec);
            graphics.DrawLine(pen, from.x, from.y, to.x, to.y);
        }
    }
}