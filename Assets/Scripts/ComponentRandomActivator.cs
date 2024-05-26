using UnityEngine;

public class ComponentRandomActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    private void Start()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }

        int selected = Random.Range(0, gameObjects.Length);
        gameObjects[selected].SetActive(true);
    }
}
