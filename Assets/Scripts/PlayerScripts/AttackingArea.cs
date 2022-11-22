using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AttackingArea : MonoBehaviour
{
    [SerializeField]
    public float thrust;
    [SerializeField]
    public float knockTime;

    private bool IsColliding;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy")) //Kijk of de tag gelijk is aan enemy
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null) //Checked if de enemy wel een rigidbody heeft.
            {

                enemy.isKinematic = false; //Set kinematic op false.
                Vector2 difference = enemy.transform.position;
                difference = difference.normalized * thrust;  //returns difference with a magnitude of 1 and multiplies it by the current thrust. (Speed of the knockback)
                enemy.AddForce(difference, ForceMode2D.Impulse); //Adds force aan de enemy zodat hij knockbacked
                enemy.isKinematic = true; //sets kinematic weer op true
                StartCoroutine(KnockBackControler(enemy));
            }
        
        }
    }

    private IEnumerator KnockBackControler(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
       
    }
}

