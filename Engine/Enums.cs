using System;

namespace Engine;

public enum MouseButton
{
    Left,
    Middle,
    Right,
    Invalid
}

[Flags]
public enum Alignment
{
    Left    = 0b000_100,
    YCenter = 0b000_010,
    Right   = 0b000_001,

    Top     = 0b100_000,
    XCenter = 0b010_000,
    Bottom  = 0b001_000,

    Center  = YCenter | XCenter,
    TopLeft = Top | Left
}

/// <summary>
/// Method of generating a color in Random.Color
/// </summary>
public enum ColorRNGMode
{
    /// <summary>
    /// Creates a color from a randomly generated hex code [x0 - xFFFFFF].
    /// </summary>
    RandomHex,

    /// <summary>
    /// Creates a color from 3 randomly generated [0 - 1] values. This method tends to generate lighter colors than RandomHex.
    /// </summary>
    RandomValues,

    /// <summary>
    /// Generates a grayscale color from a single random value [0 - 1].
    /// </summary>
    Grayscale,

    /// <summary>
    /// Creates a color from 3 randomly generated [0 - 1] values, then normalizes it [-> always total of 1].
    /// </summary>
    NormalizedValues,

    /// <summary>
    /// Creates a color from a randomly generated hex code [x0 - xFFFFFF], then normalizes it [-> always total of 0xFFFFFF].
    /// </summary>
    NormalizedHex,

    /// <summary>
    /// Creates a new HSV color, where saturation and value are both 1, but the hue is a random value between 0 and 1.
    /// </summary>
    HueOnly,

    /// <summary>
    /// Selects a random pair from RGB [-> RG, GB or BR], generates a random value [0 - 1] for each, then normalizes it. This mode generates deep and strong colors with a brightness of 1.
    /// </summary>
    Pairs
}

public enum SpriteRenderingMode
{
    /// <summary>
    /// Streches the sprite to the bounds
    /// </summary>
    Fit,

    /// <summary>
    /// Multiplies the sprite's size by the renderer's size, ignoring bounds
    /// </summary>
    Multiply
}