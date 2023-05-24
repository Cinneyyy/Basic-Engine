using SWF = System.Windows.Forms;

namespace Engine;

public static class Cursor
{
    public static bool visible
    {
        get => _visible;
        set {
            _visible = value;
            if(_visible)
                SWF::Cursor.Show();
            else
                SWF::Cursor.Hide();
        }
    }
    public static Vec2i pixelPos
    {
        get => SWF::Cursor.Position;
        set => SWF::Cursor.Position = value;
    }
    public static Vec2 worldPos
    {
        get => Camera.active.ScreenToWorld(pixelPos);
        set => pixelPos = Camera.active.WorldToScreen(value);
    }

    private static bool _visible = true;
}