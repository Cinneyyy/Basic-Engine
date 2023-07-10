using System.Collections.Generic;
using System.Windows.Forms;
using Engine.Internal;
using Engine.Rendering;
using System.Drawing.Drawing2D;
using SWF = System.Windows.Forms;
using SYS = System;

namespace Engine;

public class Window
{
    public string title
    {
        get => canvas.Text;
        set => canvas.Text = value;
    }
    public readonly Canvas canvas;
    public Color clearColor;
    public Vec2i size => canvas.Size;
    public Vec2i center => size / 2;
    public float screenToWorldRatio { get; private set; }
    public float unitsPerScreenWidth
    {
        get => _unitsPerScreenWidth;
        set => screenToWorldRatio = canvas.Size.Width / (_unitsPerScreenWidth = value);
    }

    internal GameLoop gameLoop 
    { 
        get => InternalGetters.gameLoop;
        private init => InternalGetters.gameLoop = value;
    }
    internal RenderLoop renderLoop
    {
        get => InternalGetters.renderLoop;
        private init => InternalGetters.renderLoop = value;
    }

    private float _unitsPerScreenWidth;

    public static Window instance { get; private set; }

    private static readonly List<Renderer> registeredRenderers = new();


    public Window(string title, float unitsPerScreenWidth, Color clearColor)
    {
        instance = this;
        this.clearColor = clearColor;

        canvas = new()
        {
            Text = title,
            FormBorderStyle = FormBorderStyle.None,
            WindowState = FormWindowState.Maximized,
            BackColor = clearColor
        };

        canvas.Size = Screen.FromControl(canvas).Bounds.Size;
        canvas.Paint += OnPaint;
        canvas.KeyDown += Input.OnKeyDown;
        canvas.KeyUp += Input.OnKeyUp;
        canvas.MouseDown += Input.OnMBDown;
        canvas.MouseMove += Input.OnCursorMove;
        canvas.MouseWheel += Input.OnWheelScroll;

        gameLoop = new(canvas);
        renderLoop = new(canvas);
        gameLoop.Start();
        renderLoop.Start();

        screenToWorldRatio = canvas.Size.Width / (this.unitsPerScreenWidth = unitsPerScreenWidth);

        canvas.Refresh();
        SWF::Application.Run(canvas);
    }


    private void OnPaint(object sender, PaintEventArgs args)
    {
        var graphics = args.Graphics;

        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.SmoothingMode = SmoothingMode.HighSpeed;
        graphics.PixelOffsetMode = PixelOffsetMode.Half;
        graphics.CompositingQuality = CompositingQuality.HighSpeed;

        graphics.Clear(clearColor);

        int count = registeredRenderers.Count;
        for(int i = 0; i < count; i++)
            registeredRenderers[i].Draw(in graphics, screenToWorldRatio);
    }

    private void OnFormClosed(object sender, FormClosedEventArgs args)
        => Application.Quit(0);

    
    public static void RegisterRenderer(Renderer renderer, bool addToEnd = false)
    {
        if(renderer != null)
        {
            if(addToEnd)
                registeredRenderers.Add(renderer);
            else
                registeredRenderers.Insert(0, renderer);

            SYS::Console.WriteLine($"Registered renderer \"{renderer!.obj?.name ?? "[undefined name]"}\" to the {(addToEnd ? "end" : "start")} of the render queue");
        }
    }

    public static void UnregisterRenderer(Renderer renderer)
        => registeredRenderers.Remove(renderer);
}