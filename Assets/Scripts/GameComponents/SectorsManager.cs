using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorsManager: MonoBehaviour
{
    [Header("Cache")]
    [field: SerializeField] public Sector[] Sectors { get; private set; }

    [Header("Progress")]
    [SerializeField] private float monitorStartInterval;


    [Header("Evac")]
    [field: SerializeField] public float evacTimePerPerson { get; private set; } = 0.2f;
    [field: SerializeField] public int TotalPeopleToEvacuate { get; private set; }
    [SerializeField] private int minimumPeoplePerSector;

    [Header("Destruction")]
    [Tooltip("Seconds before screen goes blank after destruction")]
    [field: SerializeField] public float interferenceDelays { get; private set; } = 0.8f;


    // members
    private List<Sector> inactiveSectors = new List<Sector>();


    private void Start()
    {
        DistributePopulationToSectors();

        foreach (Sector sector in Sectors) { inactiveSectors.Add(sector); }

        StartCoroutine(ActivateRandomSector());
    }

    private void DistributePopulationToSectors()
    {
        int[] peoplePerSector = Helpers.RandomlyDivideInt(TotalPeopleToEvacuate,
                                                            Sectors.Length,
                                                            minimumPeoplePerSector);

        //for (int i = 0; i < peoplePerSector.Length; i++)
        //{
        //    Debug.Log(peoplePerSector[i]);
        //}

        for (int i = 0; i < peoplePerSector.Length; i++)
        {
            Sectors[i].SetPeopleInSector(peoplePerSector[i]);
        }
    }

    private void ActivateSector(Sector sector)
    {
        Debug.Log($"Enabling {sector.gameObject.name}");
        sector.gameObject.SetActive(true);
    }

    IEnumerator ActivateRandomSector()
    {
        int index = Random.Range(0, inactiveSectors.Count);
        ActivateSector(inactiveSectors[index]);
        inactiveSectors.RemoveAt(index);

        yield return new WaitForSeconds(monitorStartInterval);

        index = Random.Range(0, inactiveSectors.Count);
        ActivateSector(inactiveSectors[index]);
        inactiveSectors.RemoveAt(index);

        if (inactiveSectors.Count > 0)
        {
            StartCoroutine(ActivateRandomSector());
        }
    }

}
