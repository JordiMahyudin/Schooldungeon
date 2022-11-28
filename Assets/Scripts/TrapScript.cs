using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField]
    PlayerHealth playerHealth;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("In trap");
          int hp = playerHealth.Lifes;
           playerHealth.TakeDamage(hp);
        }
    }
}
