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

    public static int[] RandomlyDivideInt(int total, int numberOfSegments, int minimumPerSegment)
    {
        if (minimumPerSegment * numberOfSegments > total)
        {
            Debug.LogError("total needs to be higher than the sum of the minimum in each segment!");
        }

        int[] result = new int[numberOfSegments];

        for (int i = 0; i < numberOfSegments; i++)
        {
            result[i] = minimumPerSegment;
            total -= minimumPerSegment;
        }

        while (total > 0)
        {
            result[Random.Range(0, numberOfSegments)]++;
            total--;
        }

        return result;
    }
}
