using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public AttackingArea aaScript;

    [Header("Movement Stuff")]
    [SerializeField]
    private float MovementSpeed = 5;
    [SerializeField]
    private float DashSpeed = 1f;
    //[SerializeField]
    //private bool currentlyMoving = false;
    //Temporarly disabled untill i want to balance fighting 

    [Header("Dashing Stuff")]
    [SerializeField]
    private Transform EndPosition;  //Eindpositie van de dash voor de lerp
    [SerializeField]
    private float DashingCooldown = 1.8f;
    public bool dashOnCooldown = false;
    float dashLerp;  //Value van 0-1 voor de lerp
    private float journeyLength; //Hoe ver de lerp moet gaan.

    [Header("Dash Location")]
    //All dash spots
    [SerializeField]
    private GameObject dashspot;
    [SerializeField]
    private GameObject dashspot1;
    [SerializeField]
    private GameObject dashspot2;
    [SerializeField]
    private GameObject dashspot3;

    [Header("AttackhitboxLocation")]
  
    public GameObject attackhitboxRight;
    public GameObject attackhitboxLeft;
    public GameObject attackhitboxUp;
    public GameObject attackhitboxDown;
 

    [Header("Attacking Stuff")]

    [SerializeField]
    private GameObject attackHitbox;
    private bool isAttacking = false;
    private bool AttackingCooldown = false;
    private float TimeToAttack = 1f;

    [Header("Perfect Dashing")]
    [SerializeField]
    GameObject DashHitbox;
    private bool hitboxDisabled;
    private float cooldown = 0.8f;
    private Animation anim;


    void Start()
    {
      //  anim = gameObject.GetComponent<Animation>();
        journeyLength = Vector3.Distance(transform.position, EndPosition.position);  //Hoe ver de dash moet gaan
        attackHitbox.SetActive(false);
        DashHitbox.SetActive(false);
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W))
        {
            aaScript.AttackCollider = attackhitboxUp;
            if (isAttacking != true)
            {
                attackHitbox = attackhitboxUp;
            }
            EndPosition = dashspot.transform; //Sets it to the position you need to dash to
            transform.position += transform.forward * MovementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            aaScript.AttackCollider = attackhitboxDown;
            if (isAttacking != true)
            {
                attackHitbox = attackhitboxDown;
            }
            EndPosition = dashspot3.transform; //Sets it to the position you need to dash to
            transform.position -= transform.forward * MovementSpeed * Time.deltaTime;
        }


        if (Input.GetKey(KeyCode.D))
        {
            aaScript.AttackCollider = attackhitboxRight;
            if (isAttacking != true)
            {
                attackHitbox = attackhitboxRight;
            }
            EndPosition = dashspot1.transform; //Sets it to the position you need to dash to
            transform.position += transform.right * MovementSpeed * Time.deltaTime;
        }
        

        if (Input.GetKey(KeyCode.A))
        {
            aaScript.AttackCollider = attackhitboxLeft;
            if (isAttacking != true)
            {
                attackHitbox = attackhitboxLeft;
            }
            EndPosition = dashspot2.transform; //Sets it to the position you need to dash to
            transform.position -= transform.right * MovementSpeed * Time.deltaTime;
        }
       


    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (dashOnCooldown == false)
            {
                StartCoroutine(Dashing());
                StartCoroutine(perfectDashing());
            }
        }
        if (dashOnCooldown == true)
        {
            DashingCooldown -= Time.deltaTime; //Start tijd van de player.
            if (DashingCooldown <= 0f)
            {
                dashOnCooldown = false;
                DashingCooldown = 1.8f;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
                isAttacking = true;
                //Add Animation stuff here :)
                StartCoroutine(DoAttack());
        }
    }
    IEnumerator DoAttack()
	{
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackHitbox.SetActive(false);
        isAttacking = false;
	}

    IEnumerator perfectDashing()
    {
        DashHitbox.SetActive(true);
        yield return new WaitForSeconds(cooldown);
        DashHitbox.SetActive(false);

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
            dashOnCooldown = true;
        }
        dashLerp = 0;
    }
}
