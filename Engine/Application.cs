using Engine.Internal;
using System;
using System.Runtime.ConstrainedExecution;

namespace Engine;

public static class Application
{
    public static bool is64BitOs => Environment.Is64BitOperatingSystem;
    public static string[] commandLineArgs => Environment.GetCommandLineArgs();
    public static string dataPath => Environment.CurrentDirectory;

    public static event GenericCallback onApplicationQuit = delegate { };


    public static void Prepare(bool hideConsoleWindow = true, float cameraSize = 10f)
    {
        Camera.active = new(new(), Vec2.zero, cameraSize);

        ConsoleWindow.ShowWindow(hideConsoleWindow ? ConsoleWindow.Mode.HIDE : ConsoleWindow.Mode.RESTORE);
        Input.keyDown += key => { if(key == System.Windows.Forms.Keys.Escape) Quit(); };
    }

    public static void StartWindow(string windowTitle, int clear = 0x000000)
        => _ = new Window(windowTitle, Camera.active.xSize, new Color(clear));


    public static void Quit(int exitCode = 0)
    {
        InternalGetters.renderLoop.run = InternalGetters.gameLoop.run = false;
        onApplicationQuit();
        Environment.Exit(exitCode);
    }
}