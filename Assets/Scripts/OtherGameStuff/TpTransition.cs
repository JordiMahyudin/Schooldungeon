using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpTransition : MonoBehaviour
{
    [SerializeField]
    private Transform Tptarget;
    public GameObject player;
    [SerializeField]
    private GameObject UItransition;
    private bool TpTrue = false;
    public bool Coroutine = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something is colliding");

        TpTrue = true;
        if (TpTrue)
        {
            StartCoroutine(TransitionCoroutine());
            Coroutine = true;
        }
       
    }

    IEnumerator TransitionCoroutine()
    {
        if (TpTrue)
        {
            UItransition.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        player.transform.position = Tptarget.transform.position;
        UItransition.SetActive(false);
        TpTrue = false;
        Coroutine = false;

    }
}
