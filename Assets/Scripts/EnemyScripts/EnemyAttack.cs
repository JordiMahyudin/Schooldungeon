using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth ph;

    void Start()
    {
        ph = GameObject.FindGameObjectWithTag("Target").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //player takes damage
        if (other.gameObject.CompareTag("Target"))
        {
            ph.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
