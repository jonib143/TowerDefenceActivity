using UnityEngine;

public static class Bezier
{
    /// <summary>
    /// Quadratic Bézier (3 control points):
    /// P0 = Start, P1 = Control/Pull point, P2 = Target
    /// </summary>
    public static Vector3 Quadratic(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t); // Pass 1
        Vector3 b = Vector3.Lerp(p1, p2, t); // Pass 1
        return Vector3.Lerp(a, b, t);        // Pass 2
    }

    /// <summary>
    /// Cubic Bézier (4 control points):
    /// P0 = Start, P1 = Tangent 1, P2 = Tangent 2, P3 = Target
    /// </summary>
    public static Vector3 Cubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Pass 1
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        // Pass 2
        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t); 

        // Pass 3
        return Vector3.Lerp(d, e, t);
    }

    /// <summary>
    /// Quadratic Tangent (Velocity / Direction vector)
    /// </summary>
    public static Vector3 QuadraticTangent(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return 2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1);
    }

    /// <summary>
    /// Cubic Tangent (Velocity / Direction vector)
    /// </summary>
    public static Vector3 CubicTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        return 3f * u * u * (p1 - p0) + 6f * u * t * (p2 - p1) + 3f * t * t * (p3 - p2);
    }
}