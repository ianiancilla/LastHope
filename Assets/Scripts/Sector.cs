using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

//[ExecuteAlways] // activate when you want to reflect any new edits in the scene
public class Sector : MonoBehaviour
{
    [SerializeField] [Range(0, 11)] int sectorNumber;
    [SerializeField] string sectorPadButtonAsString;
    [SerializeField] string sectorKBButtonAsString;
    [SerializeField] private Color sectorColor;

    // members
    private int layer;
    private LayerMask layerMask;
    private const int SECTOR_TO_LAYER_OFFSET = 10;
    private string sectorButtonAsString;
    private int maxHealth = 2;
    private int currentHealth;


    // cache
    [SerializeField] Cannon myCannon;
    [SerializeField] Camera myCamera;
    [SerializeField] TMP_Text inputButtonUI;
    [SerializeField] Volume postProcessingVolume;

    // events
    public event Action OnSectorHit;
    public event Action OnSectorDestroyed;
    public event Action OnCannonShoot;
    public event Action OnCannonLoaded;

    private void Start()
    {
        SetEverythingToSectorLayer();
        SetSectorColor();
        SetButtonText();
        currentHealth = maxHealth;
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

    public void SetCameraViewportRect(float x, float y, float w, float h)
    {
        myCamera.rect = new Rect(x, y, w, h);
    }

    public void Shoot()
    {
        myCannon.Shoot();
        OnCannonShoot?.Invoke();
    }

    public void CannonLoaded()
    {
        OnCannonLoaded?.Invoke();
    }
   
    public void TakeDamage()
    {
        //Debug.Log($"Sector {gameObject.name} hit");
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SectorDestroyed();
            return;
        }
        OnSectorHit?.Invoke();
    }

    private void SectorDestroyed()
    {
        //Debug.Log($"Sector {gameObject.name} destroyed");
        OnSectorDestroyed?.Invoke();
    }

}
