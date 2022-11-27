using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public PlayerHealth ph;
    // Start is called before the first frame update

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
        if (other.gameObject.CompareTag("Target"))
        {
            Debug.Log("trigger");
            ph.GetComponent<PlayerHealth>().TakeDamage();
        }
    }


}
