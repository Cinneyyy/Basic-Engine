using s_mf = System.MathF;
using s_m = System.Math;
using System.Linq;

namespace Engine;

public static class Mathf
{
    public const float E  = 2.71828182845904523536f;
    public const float Pi = 3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679821480865132823066470938446095505822317253594081284811174502841027019385211055596446229489549303819644288109756659334461284756482337867831652712019091456485669234603486104543266482133936072602491412737245870066063155881748815209209628292540917153643678925903600113305305488204665213841469519415116094330572703657595919530921861173819326117931051185480744623799627495673518857527248912279381830119491298336733624406566430860213949463952247371907021798609437027705392171762931767523846748184676694051320005681271452635608277857713427577896091736371787214684409012249534301465495853710507922796892589235420199561121290219608640344181598136297747713099605187072113499999983729780499510597317328160963185950244594553469083026425223082533446850352619311881710100031378387528865875332083814206171776691473035982534904287554687311595628638823537875937519577818577805321712268066130019278766111959092164201989f; // ik ik this is gonna get rounded but it's funny i think
    public const float Deg2Rad = Pi / 180f;
    public const float Rad2Deg = 1f / Pi;


    public static float Pow(this float n, float exp) => s_mf.Pow(n, exp);
    public static float Squared(this float n) => n*n;
    public static float Cubed(this float n) => n*n*n;
    public static int Squared(this int n) => n*n;
    public static int Cubed(this int n) => n*n*n;

    public static float Root(this float x, float y) => Pow(x, 1f / y);
    public static float Sqrt(this float x) => s_mf.Sqrt(x);
    public static float Cbrt(this float x) => s_mf.Cbrt(x);

    public static float Abs(this float x) => s_mf.Abs(x);
    public static int Abs(this int x) => s_m.Abs(x);

    public static float Sign(this float x) => x < 0f ? -1f : 1f;
    public static int Sign(this int x) => x < 0 ? -1 : 1;

    public static float Sin(this float x) => s_mf.Sin(x);
    public static float Cos(this float x) => s_mf.Cos(x);
    public static float Tan(this float x) => s_mf.Tan(x);

    public static float LogE(this float x) => s_mf.Log(x);
    public static float Log2(this float x) => s_mf.Log2(x);
    public static float Log10(this float x) => s_mf.Log10(x);
    public static float Log(this float n, float b) => s_mf.Log(n, b);

    public static float Sigmoid(this float x) => 1 / (1 + E.Pow(-x));
    public static float InvSigmoid(this float x) => 1 + E.Pow(-x);

    public static float Round(this float x, int digits) => s_mf.Round(x, digits);
    public static int Round(this float x) => (int)s_mf.Round(x);
    public static int Floor(this float x) => (int)s_mf.Floor(x);
    public static int Ceil(this float x) => (int)s_mf.Ceiling(x);

    public static float Rem(this float x) => x % 1f;
    public static float Rem(this float x, float y) => x % y;
    public static int Rem(this int x, int y) => x % y;

    public static float Avg(this float f, params float[] vals) => (f + vals.Sum()) / (1 + vals.Length);
    public static float Avg(this float[] vals) => vals.Sum() / vals.Length;

    public static float Lerp(this float a, float b, float t) => t * (b - a) + a;

    public static float Clamp01(this float x) => x.Clamp(0f, 1f);
    public static float Clamp(this float x, float min, float max) => float.Clamp(x, min, max);
    public static int Clamp(this int x, int min, int max) => int.Clamp(x, min, max);

    public static float Plus(this float x, float y) => x + y;
    public static float Minus(this float x, float y) => x - y;
    public static float Times(this float x, float y) => x * y;
    public static float Over(this float x, float y) => x / y;

    public static float Half(this ref float x) => x *= .5f;

    public static float Map(this float x, float fromLow, float fromHigh, float toLow, float toHigh)
        => ((x - fromLow) * (toHigh - toLow) / (fromHigh - fromLow)) + toLow;
    public static int Map(this int x, int fromLow, int fromHigh, int toLow, int toHigh)
        => x.AsFloat().Map(fromLow, fromHigh, toLow, toHigh).Round();

    public static float Asin(this float x) => s_mf.Asin(x);
    public static float Acos(this float x) => s_mf.Acos(x);
    public static float Atan(this float x) => s_mf.Atan(x);

    public static float Atan2(this float y, float x) => s_mf.Atan2(y, x);

    public static float Min(this float x, float y) => x < y ? x : y;
    public static float Max(this float x, float y) => x > y ? x : y;
    public static int Min(this int x, int y) => x < y ? x : y;
    public static int Max(this int x, int y) => x > y ? x : y;
}