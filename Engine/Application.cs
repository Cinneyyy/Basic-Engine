using Engine.Internal;
using System;

namespace Engine;

public static class Application
{
    public static bool is64BitOs => Environment.Is64BitOperatingSystem;
    public static string[] commandLineArgs => Environment.GetCommandLineArgs();
    public static string dataPath => Environment.CurrentDirectory;

    public static event GenericCallback onApplicationQuit = delegate { };


    public static void Quit(int exitCode = 0)
    {
        InternalGetters.renderLoop.run = InternalGetters.gameLoop.run = false;
        onApplicationQuit();
        Environment.Exit(exitCode);
    }
}