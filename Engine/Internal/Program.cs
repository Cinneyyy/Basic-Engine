namespace Engine.Internal;

internal class Program
{
    public const bool CamMovement = true, CamZoom = true;
    public const float CamSize = 10f;
    public const string WindowTitle = "Game Title";
    public const int ClearColor = 0x000000;


    private static void Main()
    {
        Camera.active = new(new(), Vec2.zero, CamSize);

        if(CamMovement) Camera.active.SetupMovement(20);
        if(CamZoom) Camera.active.SetupZoom();

        ConsoleWindow.ShowWindow(ConsoleWindow.Mode.RESTORE);
        Input.keyDown += key => { if(key == System.Windows.Forms.Keys.Escape) Application.Quit(); };

        _ = new Window(WindowTitle, Camera.active.size, new Color(ClearColor));
    }
}