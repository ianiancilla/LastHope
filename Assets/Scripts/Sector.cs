using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Sector : MonoBehaviour
{
    [SerializeField] [Range(0, 11)] int sectorNumber;
    [SerializeField] string sectorButtonAsString;
    [SerializeField] private Color sectorColor;

    // members
    private int layer;
    private LayerMask layerMask;
    private const int SECTOR_TO_LAYER_OFFSET = 10;

    // cache
    [SerializeField] Cannon myCannon;
    [SerializeField] Camera myCamera;
    [SerializeField] TMP_Text inputButtonUI;
    [SerializeField] Volume postProcessingVolume;


    private void Start()
    {
        SetEverythingToSectorLayer();
        SetSectorColor();
        inputButtonUI.text = sectorButtonAsString;
    }

    /// <summary>
    /// Since we are having multiple cameras on screen, every sector is spatially
    /// in the same coordinates, but on a different layer without collisions with
    /// other layers, and with camera and post-processing only affecting that layer.
    /// </summary>
    private void SetEverythingToSectorLayer()
    {
        layer = sectorNumber + SECTOR_TO_LAYER_OFFSET;
        layerMask = 1 << layer;

        myCamera.cullingMask = layerMask;
        myCamera.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>()
                                                        .volumeLayerMask = layerMask;

        Helpers.ChangeLayersRecursively(this.gameObject, layer);
    }

    public void Shoot()
    {
        myCannon.Shoot();
    }

    private void SetSectorColor()
    {
        postProcessingVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments);
        colorAdjustments.colorFilter.value = sectorColor;
    }

    public void SetCameraViewportRect(float x, float y, float w, float h)
    {
        myCamera.rect = new Rect(x, y, w, h);
    }
}
