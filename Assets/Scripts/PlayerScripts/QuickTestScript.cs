using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTestScript : MonoBehaviour
{

    //THIS IS A QUICK TEST SCRIPT SO THE ENEMY CAN TAKE DAMAGE FOR TESTING (PLEASE DO NOT RELY ON THIS SCRIPT FOR ENEMY HEALTH BECAUSE I MADE IT WITHIN 2 MINUTES)

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
    //Again please do not use this for the real game its simply just a test (take inspiration but the script isnt polished enough to be used :) )
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
