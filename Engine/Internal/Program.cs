namespace Engine.Internal;

internal class Program
{
    public const bool CamMovement = false, CamZoom = false;
    public const float CamSize = 10f;
    public const string WindowTitle = "Game Title";
    public const int ClearColor = 0x000000;


    private static void Main()
    {
        Camera.active = new(new(), Vec2.zero, CamSize);

#pragma warning disable CS0162
        if(CamMovement) Camera.active.SetupMovement(20);
        if(CamZoom) Camera.active.SetupZoom();
#pragma warning restore CS0162

        ConsoleWindow.ShowWindow(ConsoleWindow.Mode.RESTORE);
        Input.keyDown += key => { if(key == System.Windows.Forms.Keys.Escape) Application.Quit(); };

        Game.Game.Start();

        Input.InitInputState();
        _ = new Window(WindowTitle, Camera.active.size, new Color(ClearColor));
    }
}