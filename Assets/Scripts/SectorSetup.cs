using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class SectorSetup : MonoBehaviour
{
    [Header("This Sector Settings")]
    [SerializeField] [Range(0, 11)] int sectorNumber;
    [SerializeField] string sectorPadButtonAsString;
    [SerializeField] string sectorKBButtonAsString;
    [SerializeField] private Color sectorColor;

    [Header("Cache")]
    [SerializeField] Camera myCamera;
    [SerializeField] TMP_Text inputButtonUI;
    [SerializeField] Volume postProcessingVolume;

    private int layer;
    private LayerMask layerMask;
    private const int SECTOR_TO_LAYER_OFFSET = 10;

    private string sectorButtonAsString;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetEverythingToSectorLayer();
        SetSectorColor();
        SetButtonText();
    }

    private void SetButtonText()
    {
        if (FindFirstObjectByType<SceneSettings>().activeControlScheme == ControlScheme.gamepad)
        {
            sectorButtonAsString = sectorPadButtonAsString;
        }
        else
        {
            sectorButtonAsString = sectorKBButtonAsString;
        }

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
    private void SetSectorColor()
    {
        postProcessingVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments);
        colorAdjustments.colorFilter.value = sectorColor;
    }

}
