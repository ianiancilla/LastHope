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
}
