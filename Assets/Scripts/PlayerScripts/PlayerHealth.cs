using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject[] HitPoints; //Physical ingame lifes
    public int Lifes; // Value van de levens

    private void Start()
    {
        Lifes = HitPoints.Length; //Sets lifes equal to the hitpoints
    }

    public void ReduceLife(int damage)
    {
        if (Lifes >= 1)
        {
            Lifes -= damage; //Takes a life when damage taken (use damage for enemies)
            Destroy(HitPoints[Lifes].gameObject); //Destroys hitpoint when damage was taken
            if (Lifes < 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Temporarly added this so it reloads the scene whenever all lifes are gone
            }
        }
    }    

    private void getExtraLife()
    {
        //Add code whenever you get a pickup you get an extra life back
    }
}
