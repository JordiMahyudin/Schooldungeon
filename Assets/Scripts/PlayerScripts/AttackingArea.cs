using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AttackingArea : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    private int AD = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Damage();
        }
    }


    private void Damage()
    {
        health.Lifes = health.Lifes = AD;
        health.UpdateHealth();
    }
}

