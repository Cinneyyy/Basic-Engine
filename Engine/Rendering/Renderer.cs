using System.Drawing;

namespace Engine.Rendering;

public abstract class Renderer : Object
{
    protected private static readonly Vec2 yFlipVec = new(1, -1);


    public Renderer()
        => Window.RegisterRenderer(this);

    ~Renderer()
        => Window.UnregisterRenderer(this);


    /// <param name="stwr">Screen to world ratio</param>
    public abstract void Draw(in Graphics graphics, float stwr);


    public static Vec2 ApplyScreenScaling(in Vec2 scale)
        => scale * Window.instance.screenToWorldRatio;
    public static Vec2 ApplyScreenScaling(in Vec2 scale, float stwr)
        => scale * stwr;

    public static Vec2 ApplyPosOffset(in Vec2 pos, in Vec2 newScale)
        => (pos - Camera.active.globalPos) * yFlipVec * Window.instance.screenToWorldRatio + Window.instance.center - newScale/2f;
}