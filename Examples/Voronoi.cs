using Engine;
using System.Windows.Forms;
using SYS = System;
using SD = System.Drawing;
using Engine.Rendering;
using System.IO;

namespace Examples.Voronoi;

public static class Voronoi
{
    public struct Point
    {
        public Vec2 pos;
        public SD::Color color;


        public Point(Vec2 pos, SD::Color color)
        {
            this.pos = pos;
            this.color = color;
        }
    }


    public static Vec2i res { get; set; }
    public static Color color { get; set; }
    public static int seed { get; set; }
    public static int pointCount { get; set; }
    public static SD::Bitmap bitmap { get; set; }
    public static Point[] points { get; set; }
    
    private static int randomSeed
    {
        get {
            Random.ResetSeed();
            return (int)(Random.value * int.MaxValue);
        }
    }

    private static Color randomColor
    {
        get {
            Random.ResetSeed();
            return Random.Color(ColorRNGMode.NormalizedValues);
        }
    }


    private static void Init()
    {
        Input.keyDown += key => {
            SYS.Console.WriteLine("Pressed key " + key);

            switch(key)
            {
                case Keys.Up: seed = randomSeed; break;
                case Keys.Down: color = randomColor; break;
                case Keys.Left: Generate(); break;
                case Keys.Right: Draw(); break;
            }

            SYS::Console.WriteLine("Finished button action");
        };

        bitmap = new(res.x, res.y);

        var renderer = new Object()
            .WithModule(new SpriteRenderer(bitmap, Vec2.one * (Camera.active.xSize / (16/9f)) / bitmap.Height));

        Generate();
        Draw();
    }

    
    public static void Generate()
    {
        SYS.Console.WriteLine("Starting Generate");

        Random.seed = seed;
        var positions = new Vec2[pointCount];

        Random.seed = seed;
        for(int i = 0; i < pointCount; i++)
            positions[i] = new(Random.value, Random.value);

        Random.seed = seed;
        bool isBlack = color == Color.black;
        points = new Point[pointCount];
        for(int i = 0; i < pointCount; i++)
            points[i] = new(positions[i], isBlack ? randomColor : color * Random.Range(.75f, 1f));

        SYS.Console.WriteLine("Generated texture");
    }

    public static void Draw()
    {
        SYS.Console.WriteLine("Starting draw");
        float xFac = 1f/bitmap.Width, yFac = 1f/bitmap.Height;

        for(int x = 0; x < bitmap.Width; x++)
            for(int y = 0; y < bitmap.Height; y++)
                bitmap.SetPixel(x, y, SelectColorDistance(x*xFac, y*yFac));

        bitmap.Save($"C:/tmp/voronoi{Random.Range(0,99999)}.png");
        SYS.Console.WriteLine("Drew texture");
    }

    public static SD::Color SelectColorDistance(float x, float y)
    {
        Point closestPoint = default;
        float closestDist = 1f;
        Vec2 pos = new(x, y);

        foreach(var p in points)
        {
            float dist = pos.Dist(p.pos);
            if(dist <= closestDist)
            {
                closestDist = dist;
                closestPoint = p;
            }
        }

        return closestPoint.color;
    }

    public static void Start()
    {
        SYS.Console.WriteLine("Starting init of voronoi noise");

       // ConsoleWindow.ShowWindow(ConsoleWindow.Mode.RESTORE);
       // SYS::Console.WriteLine("Enter voronoi resolution [e.g. 1920/1080], then define a base color [e.g. #ff8a5b] and a seed [e.g. random or a 32bit integer]");
        
       // string[] resStr = SYS::Console.ReadLine().Split('/');
       // string colStr = SYS::Console.ReadLine()[1..], seedStr = SYS::Console.ReadLine().ToLower();
        string[] settings = File.ReadAllLines("C:/tmp/voronoi.txt");
        string[] resStr = settings[0].Split('/');
        string colStr = settings[1][1..], seedStr = settings[2].ToLower(), pointsStr = settings[3];

        res = new(resStr[0].AsInt(), resStr[1].AsInt());
        color = colStr == "andom" ? randomColor : new(SYS::Convert.ToInt32(colStr, 16));
        seed = seedStr == "random" ? randomSeed : seedStr.AsInt();
        pointCount = pointsStr.AsInt();

       // SYS::Console.WriteLine("Pressing the up arrow key will randomize the seed and pressing the down arrow key will randomize the color. To begin, press any key.");
       // SYS::Console.ReadKey();

       // ConsoleWindow.ShowWindow(ConsoleWindow.Mode.HIDE);
        Init();
    }
}