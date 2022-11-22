using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTestScript : MonoBehaviour
{

   public int health = 3;

    public bool Hittable = true;
    public float HitCooldown = 1.5f;

    [SerializeField]
    private Collider thisCollider;
    


    private void Update()
    {
        if (Hittable == false)
        {
            HitCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AttackHitbox")
        {
            if (Hittable)
            {
                thisCollider.enabled = true;
                health--;
                Hittable = false;
                thisCollider.enabled = false;
                HitCooldown = 1.5f;
            }
            else if (HitCooldown <= 0)
            {
                Hittable = true;
            }
            if (health <= 0)
            {
                gameObject.SetActive(false);    
            }
        }
    }
}
