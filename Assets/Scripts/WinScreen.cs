using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private int mainMenuNumber;


    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuNumber);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
