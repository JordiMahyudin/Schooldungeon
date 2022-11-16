using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject[] HitPoints;
    public int Lifes;

    private void Start()
    {
        Lifes = HitPoints.Length;
    }

    void Update()
    {
        
    }

    public void ReduceLife(int damage)
    {
        Lifes -= damage;
        Destroy(HitPoints[Lifes].gameObject);
        if (Lifes < 1)
        {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }    

    private void getExtraLife()
    {
        //Add code whenever you get a pickup you get an extra life back
    }
}
