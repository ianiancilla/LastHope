using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] shootSounds;
    [SerializeField] private AudioClip[] failedShootSounds;
    [SerializeField] private AudioClip[] sectorDamagedSounds;
    [SerializeField] private AudioClip[] sectorKilledSounds;
    [SerializeField] private AudioClip[] sectorSavedSounds;
    //[SerializeField] private AudioClip UISelectSound;
    [SerializeField] private AudioClip UIClickSound;

    // cache
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Cannon.OnAnyCannonShot += OnCannonShot;
        Cannon.OnAnyCannonShotFailed += OnCannonShotFailed;
        Sector.OnAnySectorEvac += OnSectorSaved;
        Sector.OnAnySectorKilled += OnSectorKilled;
        Sector.OnAnySectorHit += OnSectorDamaged;
    }
    private void OnDisable()
    {
        Cannon.OnAnyCannonShot -= OnCannonShot;
        Cannon.OnAnyCannonShotFailed -= OnCannonShotFailed;
        Sector.OnAnySectorEvac -= OnSectorSaved;
        Sector.OnAnySectorKilled -= OnSectorKilled;
        Sector.OnAnySectorHit -= OnSectorDamaged;
    }

    private void OnCannonShot()
    {
        PlayRandomClipFromArray(shootSounds);
    }

    private void OnCannonShotFailed()
    {
        PlayRandomClipFromArray(failedShootSounds);
    }

    private void OnSectorDamaged()
    {
        PlayRandomClipFromArray(sectorDamagedSounds);
    }
    private void OnSectorKilled(int peopleInSector)
    {
        PlayRandomClipFromArray(sectorKilledSounds);
    }
    private void OnSectorSaved(int peopleInSector)
    {
        PlayRandomClipFromArray(sectorSavedSounds);
    }

    public void PlayRandomClipFromArray(AudioClip[] audioClips)
    {
        Debug.Log($"Playing a sound from {audioClips}");
        if (audioClips == null || audioClips.Length == 0) { return; }

        int index = Random.Range(0, audioClips.Length);
        Debug.Log($"Playing {audioClips[index]}");
        audioSource.PlayOneShot(audioClips[index], 1f);
    }

    public void PlayUIClick()
    {
        Debug.Log($"Playing UI click");
        if (UIClickSound == null) { return; }
        audioSource.PlayOneShot(UIClickSound, 1f);
    }

}
