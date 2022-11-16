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
    private Transform EndPosition;  //Eindpositie van de dash voor de lerp
    private bool DashOnCooldown = false;
    private float startTime;
    float dashLerp;  //Value van 0-1 voor de lerp
    private float journeyLength; //Hoe ver de lerp moet gaan.


    private void Awake()
    {

    }
    void Start()
    {
        startTime = Time.time; //Start tijd van de player.
        journeyLength = Vector3.Distance(transform.position, EndPosition.position);  //Hoe ver de dash moet gaan
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
    IEnumerator Dashing() //Coroutine zodat de dash smoothe gaat. Omdat ik de movement heb gehardcode.
    {
        Vector3 endpos = EndPosition.position;  //Saved de eind positie
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
