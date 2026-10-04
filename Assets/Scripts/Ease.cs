using UnityEngine;

public static class Ease
{
    // EaseOutQuad: Starts fast, decelerates to a soft stop
    public static float EaseOutQuad(float t)
    {
        return 1f - (1f - t) * (1f - t);
    }
}