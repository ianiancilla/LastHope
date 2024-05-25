using UnityEngine;
using TMPro;


public class Sector : MonoBehaviour
{
    [SerializeField] [Range(0, 11)] int sectorNumber;

    [SerializeField] string sectorButtonAsString;

    // members
    private int layer;
    private LayerMask layerMask;
    private const int SECTOR_TO_LAYER_OFFSET = 10;

    // cache
    [SerializeField] Cannon myCannon;
    [SerializeField] Camera myCamera;
    [SerializeField] TMP_Text inputButtonUI;

    private void Start()
    {
        layer = sectorNumber + SECTOR_TO_LAYER_OFFSET;
        layerMask = 1 << layer;
        
        myCamera.cullingMask = layerMask;

        Helpers.ChangeLayersRecursively(this.gameObject, layer);      

        inputButtonUI.text = sectorButtonAsString;
    }

    public void Shoot()
    {
        myCannon.Shoot();
    }
}
