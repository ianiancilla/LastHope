using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;
using System.Collections;

//[ExecuteAlways] // activate when you want to reflect any new edits in the scene
public class Sector : MonoBehaviour
{
    [Header("This Sector Settings")]
    [SerializeField] [Range(0, 11)] int sectorNumber;
    [SerializeField] string sectorPadButtonAsString;
    [SerializeField] string sectorKBButtonAsString;
    [SerializeField] private Color sectorColor;

    [Header("Cache")]
    [SerializeField] SectorsManager sectorManager;
    [SerializeField] Cannon myCannon;
    [SerializeField] Camera myCamera;
    [SerializeField] TMP_Text inputButtonUI;
    [SerializeField] Volume postProcessingVolume;
    [SerializeField] GameObject[] objectsToDisableOnSectorInactive;
    [SerializeField] GameObject interferencePanel;
    [SerializeField] GameObject successfulEvacuationPanel;
    [SerializeField] GameObject panelCannonLoaded;

    [field: SerializeField] public Transform VFXParent { get; private set; }

    // members
    private bool isEvacuated = false;
    private bool isKilled = false;

    private int maxHealth = 2;
    private int currentHealth;

    private int peopleInSector;
    private float evacTime;
    private float evacElapsedTime = 0f;

    private int layer;
    private LayerMask layerMask;
    private const int SECTOR_TO_LAYER_OFFSET = 10;

    private string sectorButtonAsString;


    // events
    public event Action OnSectorHit;
    public event Action OnSectorKilled;
    public static event Action<int> OnAnySectorKilled;
    public event Action OnCannonShoot;
    public event Action OnCannonLoaded;
    public event Action OnSectorEvac;
    public static event Action<int> OnAnySectorEvac;


    private void Start()
    {
        SetEverythingToSectorLayer();
        SetSectorColor();
        SetButtonText();
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        evacTime = sectorManager.evacTimePerPerson * peopleInSector;
        //Debug.Log($"{gameObject.name} starting EvacProgress coroutine with Evac Time " +
        //        $"{evacTime}");

        StartCoroutine(EvacProgress());
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

    public void SetPeopleInSector(int people) 
    {
        peopleInSector = people;
    }

    public void Shoot()
    {
        myCannon.Shoot();
        panelCannonLoaded.SetActive(false);
        OnCannonShoot?.Invoke();
    }

    public void CannonLoaded()
    {
        OnCannonLoaded?.Invoke();
        panelCannonLoaded.SetActive(true);
    }
   
    public void TakeDamage()
    {
        if (isEvacuated || isKilled) { return; }
        //Debug.Log($"Sector {gameObject.name} hit");
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SectorKilled();
            return;
        }
        OnSectorHit?.Invoke();
    }

    private void SectorKilled()
    {
        //Debug.Log($"Sector {gameObject.name} destroyed");
        isKilled = true;
        OnSectorKilled?.Invoke();
        OnAnySectorKilled?.Invoke(peopleInSector);
        StartCoroutine(KillSectroAfterDelay());
    }

    IEnumerator KillSectroAfterDelay()
    {
        yield return new WaitForSeconds(sectorManager.interferenceDelays);

        TurnOffMonitor();
    }

    private void TurnOffMonitor()
    {
        foreach (GameObject gameObject in objectsToDisableOnSectorInactive)
        {
            gameObject.SetActive(false);
        }

        foreach (Transform child in VFXParent)
        {
            Destroy(child.gameObject);
        }

        postProcessingVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments);
        colorAdjustments.colorFilter.value = Color.white;

        interferencePanel.gameObject.SetActive(true);
    }

    IEnumerator EvacProgress()
    {
        while (evacElapsedTime < evacTime && !isKilled)
        {
            evacElapsedTime += Time.deltaTime;
            yield return null;
        }

        if (!isKilled) { EvacSuccess(); }
    }

    public float GetEvacState()
    {
        return evacElapsedTime / evacTime;
    }

    private void EvacSuccess()
    {
        OnSectorEvac?.Invoke();
        OnAnySectorEvac?.Invoke(peopleInSector);

        isEvacuated = true;

        Debug.Log($"Successfully evacuated {this.gameObject.name}");
        TurnOffMonitor();
        successfulEvacuationPanel.gameObject.SetActive(true);
    }
}
