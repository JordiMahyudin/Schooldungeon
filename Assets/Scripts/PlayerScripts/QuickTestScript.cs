using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTestScript : MonoBehaviour
{

   public int health = 3;

    public bool Hittable = true;
    public float HitCooldown = 0.8f;

    [SerializeField]
    private Collider thisCollider;
    


    private void Update()
    {
        if (Hittable == false)
        {
            if (HitCooldown >= 0)
            {
                HitCooldown -= Time.deltaTime;
            }
            else if (HitCooldown <= 0)
            {

                Hittable = true;
                thisCollider.enabled = true;
            }

        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AttackHitbox")
        {
            if (Hittable)
            {
                health--;
                Hittable = false;
                thisCollider.enabled = false;
                HitCooldown = 0.8f;
            }
            
            if (health <= 0)
            {
                gameObject.SetActive(false);    
            }
        }
    }
}
