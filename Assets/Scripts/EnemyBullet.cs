using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    [SerializeField] private float force;
    private PlayerHealth ph;
    void Start()
    {
        ph = GameObject.FindGameObjectWithTag("Target").GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Target");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector3(direction.x, direction.y, direction.z).normalized * force;
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
