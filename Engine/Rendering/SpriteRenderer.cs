using System.Drawing;

namespace Engine.Rendering;

public class SpriteRenderer : Renderer
{
    public Bitmap bmp;
    public Vec2 scale;
    public SpriteRenderingMode mode;


    public SpriteRenderer(Bitmap bmp, Vec2 scale, SpriteRenderingMode mode = SpriteRenderingMode.Multiply) : base()
    {
        this.bmp = bmp;
        this.scale = scale;
        this.mode = mode;
    }

    public SpriteRenderer(string bmpId, Vec2 scale, SpriteRenderingMode mode = SpriteRenderingMode.Multiply) : base()
    {
        this.bmp = Resources.GetBmp(bmpId);
        this.scale = scale;
        this.mode = mode;
    }


    public override void Draw(in Graphics graphics, float stwr)
    {
        Vec2 scale = mode switch {
            SpriteRenderingMode.Multiply => this.scale * (Vec2i)bmp.Size,
            SpriteRenderingMode.Fit => this.scale,
            _=> this.scale
        };

        Vec2 newScale = ApplyScreenScaling(scale);
        Vec2 newPos = ApplyPosOffset(drawPos, newScale);
        graphics.DrawImage(bmp, newPos.x, newPos.y, newScale.x, newScale.y);
    }
}