


using System.Numerics;

/// <summary>
/// Utility Class For Math
/// </summary>
class Util
{
    /// <summary>
    /// Returns the distance between A and B.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static float DistanceBetween(Vector2 A, Vector2 B)
    {
        return MathF.Sqrt(MathF.Pow(B.X - A.X, 2f) + MathF.Pow(B.Y - A.Y, 2f));
    }
    
    /// <summary>
    /// Clamps the given Value between Min and Max.
    /// </summary>
    /// <param name="Value"></param>
    /// <param name="Min"></param>
    /// <param name="Max"></param>
    /// <returns></returns>
    public static float Clamp(float Value, float Min, float Max)
    {
        if (Min > Max) Min = Max;
        if (Max < Min) Max = Min;

        if (Value < Min) return Min;
        if (Value > Max) return Max;

        return Value;
    }

    /// <summary>
    /// Returns the normalized direction from point A to point B.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static Vector2 GetDirectionBetween(Vector2 A, Vector2 B)
    {
        return Util.Normalized(B - A);
    }
    

    /// <summary>
    /// Returns the Length of the given A Vector2.
    /// </summary>
    /// <param name="A"></param>
    /// <returns></returns>
    public static float GetLength(Vector2 A)
    {
        return Util.DistanceBetween(Vector2.Zero, A);
    }

    /// <summary>
    /// returns the Normalized form of the given A
    /// </summary>
    /// <param name="A"></param>
    /// <returns></returns>
    public static Vector2 Normalized(Vector2 A)
    {
        return new Vector2(A.X / Util.GetLength(A), A.Y / Util.GetLength(A));
    }
}

