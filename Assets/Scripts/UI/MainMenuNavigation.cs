using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;

    private void Start()
    {
        GoToMainMenu();
    }
    public void GoToMainMenu()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void GoToOptions()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void GoToCredits()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void StartIntro()
    {
        SceneLoader.LoadIntro();
    }
}
