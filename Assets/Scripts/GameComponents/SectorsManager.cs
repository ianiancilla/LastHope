using System;
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
    private int peopleEvacuated;
    private int peopleDead;

    // events
    public enum Ending { allDead, someSaved, allSaved }
    public event Action<Ending> GameEnd;

    private void Start()
    {
        Sector.OnAnySectorEvac += OnAnySectorEvac;
        Sector.OnAnySectorKilled += OnAnySectorKilled;

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
        int index = UnityEngine.Random.Range(0, inactiveSectors.Count);
        ActivateSector(inactiveSectors[index]);
        inactiveSectors.RemoveAt(index);

        yield return new WaitForSeconds(monitorStartInterval);

        index = UnityEngine.Random.Range(0, inactiveSectors.Count);
        ActivateSector(inactiveSectors[index]);
        inactiveSectors.RemoveAt(index);

        if (inactiveSectors.Count > 0)
        {
            StartCoroutine(ActivateRandomSector());
        }
    }

    private void OnAnySectorKilled(int peopleInSector)
    {
        peopleDead += peopleInSector;
        CheckForEndGame();
    }

    private void OnAnySectorEvac(int peopleInSector)
    {
        peopleEvacuated += peopleInSector;
        CheckForEndGame();
    }

    private bool CheckForEndGame()
    {
        if (peopleEvacuated + peopleDead != TotalPeopleToEvacuate)
        {
            return false;
        }

        if (peopleDead == 0)
        {
            Debug.Log("Everyone was saved!");
            GameEnd?.Invoke(Ending.allSaved);
        }
        else if (peopleEvacuated == 0)
        {
            Debug.Log("Everyone died!");
            GameEnd?.Invoke(Ending.allDead);
        }
        else
        {
            Debug.Log("Some people were saved, but not all...");
            GameEnd?.Invoke(Ending.someSaved);
        }
        return true;
    }

}