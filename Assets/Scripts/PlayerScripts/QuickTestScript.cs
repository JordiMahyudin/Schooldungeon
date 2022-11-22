using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTestScript : MonoBehaviour
{

   public int health = 3;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "AttackHitbox")
        {
        
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
