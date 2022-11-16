using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private int mainMenuNumber;
    [SerializeField]
    private int tutorialNumber;
    [SerializeField]
    private int settingNumber;
    [SerializeField]
    GameObject parent;
    private void MainMenu()
    {
        SceneManager.LoadScene(mainMenuNumber);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Settings()
    {
       parent.SetActive(false);
    }
    public void BackToMainMenu()
    {
        parent.SetActive(true);
    }
}