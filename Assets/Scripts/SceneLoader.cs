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

    private static readonly int howToPlayIndex = -1;


    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }

    public static void LoadHowToPlay()
    {
        SceneManager.LoadScene(howToPlayIndex);
    }

    public static void LoadIntro()
    {
        SceneManager.LoadScene(introIndex);
    }

    public static void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }

    public static void LoadBadEnding()
    {
        SceneManager.LoadScene(endingBad);
    }
    public static void LoadMehEnding()
    {
        SceneManager.LoadScene(endingMeh);
    }
    public static void LoadGoodEnding()
    {
        SceneManager.LoadScene(endingGood);
    }

}
