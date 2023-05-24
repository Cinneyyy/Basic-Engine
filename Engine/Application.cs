using System;

namespace Engine;

public static class Application
{
    public static bool runningOn64Bit => Environment.Is64BitOperatingSystem;
    public static string[] commandLineArgs => Environment.GetCommandLineArgs();
    public static string dataPath => Environment.CurrentDirectory;

    public static event GenericCallback onApplicationQuit = delegate { };


    public static void Quit(int exitCode = 0)
    {
        onApplicationQuit();
        Environment.Exit(exitCode);
    }
}