using System.Runtime.InteropServices;
using System;

namespace Engine;

public static partial class ConsoleWindow
{
    public enum Mode
    {
        HIDE = 0,
        MAXIMIZE = 3,
        MINIMIZE = 6,
        RESTORE = 9
    }

    public static Mode CurMode { get; private set; } = Mode.RESTORE;

    [LibraryImport("kernel32.dll")]
    private static partial IntPtr GetConsoleWindow();
    public static readonly IntPtr ThisConsole = GetConsoleWindow();

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool ShowWindow(IntPtr hWnd, int cmdShow);

    public static bool ShowWindow(Mode windowMode)
    {
        CurMode = windowMode;
        return ShowWindow(ThisConsole, (int)windowMode);
    }
    public static bool ShowWindow(int cmdShow)
    {
        CurMode = (Mode)cmdShow;
        return ShowWindow(ThisConsole, cmdShow);
    }

    [LibraryImport("user32.dll")]
    private static partial IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    public static IntPtr PostMessage(ConsoleKey key) => PostMessage(ThisConsole, 0x100, (IntPtr)key, IntPtr.Zero);
}