using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Engine;

public static class Resources
{
    public static readonly Dictionary<string, Bitmap> loadedBitmaps = new();

    private static Assembly assembly = Assembly.GetExecutingAssembly();


    public static void LoadBitmapEmbed(string embedPath, string id)
    {
        Stream stream = assembly.GetManifestResourceStream(embedPath);
        loadedBitmaps.Add(id, new(stream));
        stream.Dispose();
    }

    public static void LoadBitmapFile(string filePath, string id)
        => loadedBitmaps.Add(id, new(filePath));


    public static void UnloadBitmap(string id)
    {
        loadedBitmaps[id].Dispose();
        loadedBitmaps.Remove(id);
    }

    public static Bitmap GetBmp(string id)
        => loadedBitmaps[id];


    public static void UnloadBmp(string id)
    {
        var item = loadedBitmaps[id];
        loadedBitmaps.Remove(id);
        item.Dispose();
    }
}