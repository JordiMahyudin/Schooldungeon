using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTestScript : MonoBehaviour
{

    //THIS IS A QUICK TEST SCRIPT SO THE ENEMY CAN TAKE DAMAGE FOR TESTING (PLEASE DO NOT RELY ON THIS SCRIPT FOR ENEMY HEALTH BECAUSE I MADE IT WITHIN 2 MINUTES)

    [Tooltip("Material to switch to during the flash.")]
    [SerializeField]
    private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField]
    private float duration;

    public int health = 3;
    public bool Hittable = true;
    public float HitCooldown = 0.8f;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;

    public GameObject[] itemDrops;

    [SerializeField]
    private Collider thisCollider;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

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
                StartCoroutine(FlashRoutine());
                Hittable = false;
                thisCollider.enabled = false;
                HitCooldown = 0.8f;
            }

            if (health <= 0)
            {
                gameObject.SetActive(false);
                ItemDrop();
            }
        }
    }
    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }

    private void ItemDrop()
    {
        for (int i = 0; i < itemDrops.Length; i++)
        {
            Instantiate(itemDrops[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}
