global using static Engine.Globals;

using System;

namespace Engine;

public static class Globals
{
    public static ConsoleColor consFg
    {
        get => Console.ForegroundColor;
        set => Console.ForegroundColor = value;
    }
    public static ConsoleColor consBg
    {
        get => Console.BackgroundColor;
        set => Console.BackgroundColor = value;
    }


    public static string CWriteLn(object o)
    {
        var msg = o.Ts();
        Console.WriteLine(msg);
        return msg;
    }

    public static string CWriteErr(object o)
    {
        var msg = o.Ts();
        var curCol = consFg;
        consFg = ConsoleColor.Red;
        Console.WriteLine(msg);
        consFg = curCol;
        return msg;
    }    
    public static string CWriteErr(Exception e, bool msgOnly = false)
    {
        var msg = msgOnly ? e.Message : e.Ts();
        var curCol = consFg;
        consFg = ConsoleColor.Red;
        Console.WriteLine(msg);
        consFg = curCol;
        return msg;
    }

    public static T[] Arr<T>() => Array.Empty<T>();
}