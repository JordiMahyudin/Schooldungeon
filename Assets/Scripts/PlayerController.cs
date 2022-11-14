using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed = 5;
    [SerializeField]
    private float DashSpeed = 1f;

    [SerializeField]
    private Transform EndPosition;
    private bool DashOnCooldown = false;
    public float m_Thrust = 10f;

    private float startTime;
    float dashLerp;
    private float journeyLength;


    private void Awake()
    {

    }
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, EndPosition.position);
        
        

    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * MovementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * MovementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * MovementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * MovementSpeed * Time.deltaTime;
        }

     

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Dashing());
        }

    }
    IEnumerator Dashing()
    {
        Vector3 endpos = EndPosition.position;
        while (dashLerp < 1)
        {
            
            float fractionOfJourney = dashLerp / journeyLength;
            dashLerp += Time.deltaTime * DashSpeed;
            transform.position = Vector3.Lerp(transform.position, endpos, fractionOfJourney);
            yield return new WaitForSeconds(0.001f);
        }
        dashLerp = 0;
    }
}
