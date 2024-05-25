using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;

    // members
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void HitByAsteroid()
    {
        Debug.Log("Sector hit");

        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Sector destroyed");
        }
    }

}
