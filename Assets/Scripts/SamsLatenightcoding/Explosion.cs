using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public PlayerHealth playerhealth;
    [SerializeField]
    private int damage = 2;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            playerhealth.TakeDamage(damage);
        }

        Explode();
    }


    void Explode()
    {
        gameObject.SetActive(false);
    }

}
