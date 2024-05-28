using UnityEngine;

public class Ground : MonoBehaviour
{
    // cache
    [SerializeField] private Sector mySector;


    public void HitByAsteroid()
    {
        mySector.TakeDamage();
    }

}
