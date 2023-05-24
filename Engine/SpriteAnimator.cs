using Engine.Rendering;
using System.Drawing;

namespace Engine;

public class SpriteAnimator
{
    public SpriteRenderer renderer;
    public Bitmap[] frames;
    public float interval = .25f;
    public bool autoUpdate
    {
        get => _autoUpdate;
        set {
            _autoUpdate = value;
            if(value)
                GameLoop.update += Update;
            else
                GameLoop.update -= Update;
        }
    }
    public float timer;
    public int frame;

    private bool _autoUpdate = true;



    public SpriteAnimator(SpriteRenderer renderer)
    {
        this.renderer = renderer;
        frames = new Bitmap[1] { renderer.bmp };
        GameLoop.update += Update;
    }

    public SpriteAnimator(SpriteRenderer renderer, params Bitmap[] frames)
    {
        this.renderer = renderer;
        this.frames = frames;
        GameLoop.update += Update;
    }


    private void Update(float dt)
    {
        if((timer += dt) >= interval)
        {
            timer %= interval;
            if(++frame >= frames.Length)
                frame = 0;

            renderer.bmp = frames[frame];
        }
    }
}