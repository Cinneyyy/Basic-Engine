using Engine;
using Engine.Rendering;

class Program
{
    private static void Main()
    {
        Camera.active = new(Vec2.zero, 16);
        Camera.active.SetupMovement(20);
        Camera.active.SetupZoom();

        var text = new Engine.Rendering.TextRenderer("Test", Color.white, 20);
        text.globalPos = new(0, 0);
        Time.update += delegate {
            text.text =
                $"Modified {Time.fps}\n" +
                $"Sample time {Time.fpsSampleTime}\n" +
                $"Target sample time {Time.fpsTargetSampleTime}\n" +
                $"Sample time error {Time.fpsSampleTimeError}\n\n"+
                $"Txt LP: {text.localPos}\n" +
                $"Txt GP: {text.globalPos}\n" +
                $"Cam LP: {Camera.active.localPos}\n" +
                $"Cam GP: {Camera.active.globalPos}";
        };
        Time.fpsTargetSampleTime = .25f;

       // text.SetParent(Camera.active);

        RectRenderer rend = new(Color.orange, 0, 0, 16, 9, true, 3);
        rend.SetParent(Camera.active);
        Time.update += delegate {
            rend.size = new(Camera.active.size, Camera.active.size / 9f);
        };

        /*
        var bmps = new System.Drawing.Bitmap[] {
            new("C:/tmp/down__still.png"),
            new("C:/tmp/left__still.png"),
            new("C:/tmp/up__still.png"),
            new("C:/tmp/right__still.png"),
        };

        var sr = new Engine.Rendering.SpriteRenderer(bmps[0], Vec2.one / 5f, SpriteRenderingMode.Multiply);
        var anim = new SpriteAnimator(sr, bmps);
        anim.interval = .2f;
        void update(Vec2i screen, Vec2 world) => sr.globalPos = world;
        Camera.active.SetupMovement(20);
        Camera.active.SetupZoom();
        Input.cursorMove += update;
        
        */

        // WindowInstance is set in constructer
        _ = new Window("Game Title", Camera.active.size, Color.black);
    }
}