using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private  Image[] HitPoints; //Physical ingame lifes
    public  int Lifes; // Value van de levens

    private void Start()
    {
        Lifes = HitPoints.Length; //Sets lifes equal to the hitpoints
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < HitPoints.Length; i++)
        {
            if (i < Lifes)
            {
                HitPoints[i].color = Color.red;
            }
            else
            {
                HitPoints[i].color = Color.black;
            }
        }
        if (Lifes <= 0)
        {
            Death();
        }
    }    

    private void GetExtraLife()
    {
        //Add code whenever you get a pickup you get an extra life back
    }

    private void Death()
    {

    }
}
