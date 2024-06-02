using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static readonly int mainMenuIndex = 0;
    private static readonly int introIndex = 1;
    private static readonly int levelIndex = 2;
    private static readonly int endingBad = 3;
    private static readonly int endingMeh = 4;
    private static readonly int endingGood = 5;

    private static readonly int howToPlayIndex = 6;


    public static void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuIndex);
    }

    public static void LoadHowToPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(howToPlayIndex);
    }

    public static void LoadIntro()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(introIndex);
    }

    public static void LoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }

    public static void LoadBadEnding()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(endingBad);
    }
    public static void LoadMehEnding()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(endingMeh);
    }
    public static void LoadGoodEnding()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(endingGood);
    }

}
