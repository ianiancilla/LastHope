using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static readonly int mainMenuIndex = 0;
    private static readonly int howToPlayIndex = 1;
    private static readonly int introIndex = 2;
    private static readonly int levelIndex = 3;

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

}
