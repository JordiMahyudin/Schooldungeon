using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingScript : MonoBehaviour
{
    private GameObject attackingZone = default;
    private bool Slashing = true;
    private float Timer = 0f;
    private float AttackingTime = 0.25f;

    void Start()
    {
        attackingZone = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Slashing)
        {
            Timer += Time.deltaTime;
        }
        if (Timer >= AttackingTime)
        {
            Timer = 0;
            Slashing = false;
            attackingZone.SetActive(Slashing);
        }
    }

    private void Attack()
    {

    }
}
