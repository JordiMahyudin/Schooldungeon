using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] m_wayPoints;
    public int m_speed;
    private int MwayPointIndex;
    private float Mdist;

    public float m_radius;
    [Range(0, 360)]
    public float m_angle;

    public GameObject m_playerRef;
    public Transform m_player;

    public LayerMask m_targetMask;
    public LayerMask m_obstructionMask;

    public bool m_canSeePlayer;

    public NavMeshAgent m_Enemy;

    //[SerializeField] private float fireRate = 0.1f;
    //private float fireCountDown = 0.5f;
    //[SerializeField] private GameObject knifePrefab;
    //[SerializeField] private Transform throwPoint;

    private void Start()
    {
        MwayPointIndex = 0;
        transform.LookAt(m_wayPoints[MwayPointIndex].position);
        m_playerRef = GameObject.FindGameObjectWithTag("test");
        StartCoroutine(FOVRoutine());
        m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);
    }

    private void Update()
    {
        if (m_canSeePlayer == true)
        {
            Vector3 dir = m_player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            m_Enemy.SetDestination(m_player.position);

            //if (fireCountDown <= 0f)
            //{
            //    Attack();
            //    fireCountDown = 1f / fireRate;
            //}
            //fireCountDown -= Time.deltaTime;
        }

        if (m_canSeePlayer == false)
        {
            Mdist = Vector3.Distance(transform.position, m_wayPoints[MwayPointIndex].position);

            if (Mdist < 2f)
            {
                IncreaseIndex();
            }
        }

    }

    void IncreaseIndex()
    {
        MwayPointIndex++;

        if (MwayPointIndex >= m_wayPoints.Length)
        {
            MwayPointIndex = 0;
        }
        //transform.LookAt(wayPoints[wayPointIndex].position);
        m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, m_radius, m_targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < m_angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, m_obstructionMask))
                    m_canSeePlayer = true;
                else
                {
                    m_canSeePlayer = false;
                    m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);
                }
            }
            else
            {
                m_canSeePlayer = false;

                m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);
            }
        }
        else if (m_canSeePlayer)
        {
            m_canSeePlayer = false;
            m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);
        }
    }

    private void Attack()
    {
        //GameObject bulletGo = (GameObject)Instantiate(knifePrefab, throwPoint.position, throwPoint.rotation);
        //knife shot = bulletGo.GetComponent<knife>();

        //if (shot != null)

        //    shot.Seek(player);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("player"))
        {

        }
    }

   
}
