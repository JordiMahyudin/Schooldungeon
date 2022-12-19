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
    [SerializeField]
    public GameObject AttackCollider;

    public PlayerController Pcontroller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") && Pcontroller.attackhitboxRight.activeInHierarchy) //Kijk of de tag gelijk is aan enemy
        {
                Rigidbody enemy = other.GetComponent<Rigidbody>();
                if (enemy != null) //Checked if de enemy wel een rigidbody heeft.
                {
                    enemy.isKinematic = false; //Set kinematic op false.
                    Vector2 difference = enemy.transform.position;
                    difference = difference.normalized * thrust;  //returns difference with a magnitude of 1 and multiplies it by the current thrust. (Speed of the knockback)
                    enemy.AddForce(difference, ForceMode.Impulse); //Adds force aan de enemy zodat hij knockbacked
                    StartCoroutine(KnockBackControler(enemy));
                }
        }
        else if (other.CompareTag("enemy") && Pcontroller.attackhitboxLeft.activeInHierarchy)
        {
            Rigidbody enemy = other.GetComponent<Rigidbody>();
            if (enemy != null) //Checked if de enemy wel een rigidbody heeft.
            {
                enemy.isKinematic = false; //Set kinematic op false.
                Vector2 difference = enemy.transform.position;
                difference = difference.normalized * -thrust;  //returns difference with a magnitude of 1 and multiplies it by the current thrust. (Direction of knockback)
                enemy.AddForce(difference, ForceMode.Impulse); //Adds force aan de enemy zodat hij knockbacked
                StartCoroutine(KnockBackControler(enemy));
            }
        }
    }
    private IEnumerator KnockBackControler(Rigidbody enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
    }
}

