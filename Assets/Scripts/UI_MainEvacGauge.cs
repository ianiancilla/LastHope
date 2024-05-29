using UnityEngine;
using UnityEngine.UI;

public class UI_MainEvacGauge : MonoBehaviour
{
    [SerializeField] Image savedImg;
    [SerializeField] Image saveableImg;
    [SerializeField] Image deadImg;

    [Header("Cache")]
    [SerializeField] SectorsManager sectorManager;

    // members
    int totalPeople;

    void Start()
    {
        InitializeGaugeValues();

        Sector.OnAnySectorEvac += EvacGaugeUp;
        Sector.OnAnySectorKilled += SectorKilled;

        totalPeople = sectorManager.TotalPeopleToEvacuate;
    }

    private void OnDisable()
    {
        Sector.OnAnySectorEvac -= EvacGaugeUp;
        Sector.OnAnySectorKilled -= SectorKilled;
    }

    private void InitializeGaugeValues()
    {
        savedImg.fillAmount = 0f;
        saveableImg.fillAmount = 1f;
        deadImg.fillAmount = 0f;
    }

    private void EvacGaugeUp(int peopleInSector)
    {
        savedImg.fillAmount += PeopleToFillAmount(peopleInSector);
    }

    private void SectorKilled(int peopleInSector)
    {
        saveableImg.fillAmount -= PeopleToFillAmount(peopleInSector);
        deadImg.fillAmount += PeopleToFillAmount(peopleInSector);
    }

    private float PeopleToFillAmount(int people)
    {
        return (float)people / (float)totalPeople;
    }
}
