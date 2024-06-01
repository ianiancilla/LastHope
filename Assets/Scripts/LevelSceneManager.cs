using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneManager : MonoBehaviour
{

    private void Start()
    {
        SectorsManager.OnGameEnd += TriggerEnding;
    }

    private void TriggerEnding(SectorsManager.Ending ending)
    {
        switch (ending)
        {
            case SectorsManager.Ending.allDead:
                SceneLoader.LoadBadEnding();
                break;
            case SectorsManager.Ending.someSaved:
                SceneLoader.LoadMehEnding();
                break;
            case SectorsManager.Ending.allSaved:
                SceneLoader.LoadGoodEnding();
                break;
            default:
                break;
        }
    }

    public void BackToTitle()
    {
        SceneLoader.LoadMainMenu();
    }
}
