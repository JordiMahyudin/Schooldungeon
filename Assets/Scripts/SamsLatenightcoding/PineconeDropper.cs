using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineconeDropper : MonoBehaviour
{
    [SerializeField]
    GameObject pineconeprefab;

    [SerializeField]
    float secondSpawn = 0.5f;

    [SerializeField]
    float minTras;
    [SerializeField]
    float maxTras;


    private void Start()
    {
        StartCoroutine(PineconeSpawn());
    }


    IEnumerator PineconeSpawn()
    {
        {
            while(true)
            {
                var wanted = Random.Range(minTras, maxTras);
                var position = new Vector3(wanted, transform.position.y);
                GameObject gameobject = Instantiate(pineconeprefab);
                yield return new WaitForSeconds(secondSpawn);
                Destroy(gameobject, 5f);
            }
        }
    }
}
