using System.Windows.Forms;

namespace Engine;

public static class Input
{
    public static event InputKeyCallback keyDown = delegate { };
    public static event InputKeyCallback keyUp = delegate { };
    public static event InputCursorClickedCallback cursorClick = delegate { };
    public static event InputCursorMovedCallback cursorMove = delegate { };
    public static event InputWheelScrollCallback wheelScroll = delegate { };


    private static (Vec2i ppos, Vec2 wpos) MapPositions(MouseEventArgs args)
    {
        Vec2i ppos = new(args.X, args.Y);
        return new(ppos, Camera.active.ScreenToWorld(ppos));
    }

    public static void OnCursorMove(object sender, MouseEventArgs args)
    {
        var (ppos, wpos) = MapPositions(args);
        cursorMove(ppos, wpos);
    }

    internal static void OnMBDown(object sender, MouseEventArgs args)
    {
        var mb = args.Button switch {
            MouseButtons.Left => MouseButton.Left,
            MouseButtons.Middle => MouseButton.Middle,
            MouseButtons.Right => MouseButton.Right,
            _ => MouseButton.Invalid
        };

        if(mb == MouseButton.Invalid)
            return;

        var (ppos, wpos) = MapPositions(args);
        cursorClick(mb, ppos, wpos);
    }

    internal static void OnWheelScroll(object sender, MouseEventArgs args)
        => wheelScroll(args.Delta >= 0 ? 1 : -1);

    internal static void OnKeyDown(object sender, KeyEventArgs args)
        => keyDown(args.KeyCode);

    internal static void OnKeyUp(object sender, KeyEventArgs args)
        => keyUp(args.KeyCode);
}