using UnityEngine;

public static class Helpers
{
    public static void ChangeLayersRecursively(this GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        if (gameObject == null)
            return;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = layer;
            ChangeLayersRecursively(child.gameObject, layer);
        }
    }

    public static float Remap (float value, float minOriginalRange, float maxOriginalRange, float minTargetRange, float maxTargetRange)
    {
        float range1 = maxOriginalRange - minOriginalRange;
        float range2 = maxTargetRange - minTargetRange;

        return ((value - minOriginalRange) / range1 * range2) + minTargetRange;
    }
}
