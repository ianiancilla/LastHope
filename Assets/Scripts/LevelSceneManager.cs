using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneManager : MonoBehaviour
{
    public void BackToTitle()
    {
        SceneLoader.LoadMainMenu();
    }
}
