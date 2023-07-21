using Engine;
using Examples.Voronoi;

public class Program
{
    private static void Main()
    {
        Application.Prepare(false);
        Voronoi.Start();
        Application.StartWindow("Voronoi Test");
    }
}