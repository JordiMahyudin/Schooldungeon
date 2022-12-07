using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //public Transform[] m_wayPoints;
    public int m_speed;
    //private int MwayPointIndex;
    //private float Mdist;

    public float m_radius;
    [Range(0, 360)]
    public float m_angle;

    public GameObject m_playerRef;
    public Transform m_player;

    public LayerMask m_targetMask;
    public LayerMask m_obstructionMask;

    public bool m_canSeePlayer;

    public NavMeshAgent m_Enemy;

    [SerializeField]
    private GameObject[] HitPoints; //Physical ingame lifes
    public int Lifes; // Value van de levens

    private bool m_playerInRange = false;
    private bool m_canAttackPlayer = true;
    [SerializeField] private float m_enemyCooldown;
    [SerializeField] private int m_damage = 0;

    private Animator anim;

    [SerializeField] private bool m_ableToMove = true;
    [SerializeField] private GameObject m_attack;
    [SerializeField] private BoxCollider boxCollider;

    public NavMeshAgent m_agent;
    [SerializeField] private float range;
    [SerializeField] private Transform centrepoint;

    private PlayerHealth ph;

    [SerializeField] private int health;
    [SerializeField] private int playerDamage;

    private void Start()
    {
        //MwayPointIndex = 0;
        //transform.LookAt(m_wayPoints[MwayPointIndex].position);
        m_playerRef = GameObject.FindGameObjectWithTag("Target");
        StartCoroutine(FOVRoutine());
        //m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);
        Lifes = HitPoints.Length; //Sets lifes equal to the hitpoints
        anim = GetComponent<Animator>();
        m_attack.SetActive(false);
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.speed = m_speed;
        ph = GameObject.FindGameObjectWithTag("Target").GetComponent<PlayerHealth>();
        boxCollider.isTrigger = false;
    }

    private void Update()
    {
        //movement
        if (m_canSeePlayer && m_ableToMove == true)
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

            if (m_canAttackPlayer == true)
            {
                
                StartCoroutine(AttackCooldown());
            }
        }

        if (m_canSeePlayer == false)
        {
            //Mdist = Vector3.Distance(transform.position, m_wayPoints[MwayPointIndex].position);

            //if (Mdist < 2f)
            //{
            //    IncreaseIndex();
            //}
            if (m_ableToMove == true)
            {
                if (m_agent.remainingDistance <= m_agent.stoppingDistance)
                {
                Vector3 point;
                if (RandomPoint(centrepoint.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    m_agent.SetDestination(point);
                }
                }

            }
        }

        if (m_ableToMove == false)
        {
            Stun();
        }

        if (health <= 0)
        {
            Death();
        }

    }

    void IncreaseIndex()
    {
        //if (m_ableToMove == true)
        //{
        //    MwayPointIndex++;

        //    if (MwayPointIndex >= m_wayPoints.Length)
        //    {
        //    MwayPointIndex = 0;
        //    }
        //    //transform.LookAt(wayPoints[wayPointIndex].position);
        //    m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);

        //}
    }

    private IEnumerator FOVRoutine()
    {
        //will constantly check if player is in line of sights
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private IEnumerator AttackCooldown()
    {
        //enemy will attack with these things happening
        m_canAttackPlayer = false;
        yield return new WaitForSeconds(m_enemyCooldown);
        Attack();
        yield return new WaitForSeconds(1f);
        m_attack.SetActive(false);
        boxCollider.isTrigger = false;
        m_ableToMove = true;
        m_canAttackPlayer = true;
        m_agent.speed = m_speed;
        yield return new WaitForSeconds(m_enemyCooldown);
    }

    private void FieldOfViewCheck()
    {
        //enemy has a radius and a field of view, the enemy will walk towards the player when in line of sights
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, m_radius, m_targetMask);
        if (m_ableToMove == true)
        {

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
                    //m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);
                }
            }
            else
            {
                m_canSeePlayer = false;

                //m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);
            }
            }
            else if (m_canSeePlayer)
            {
            m_canSeePlayer = false;
            //m_Enemy.SetDestination(m_wayPoints[MwayPointIndex].position);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        //mark random point and will walk to this location
        Vector3 randompoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randompoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    private void Attack()
    {
        m_ableToMove = false;
        boxCollider.isTrigger = true;
        m_attack.SetActive(true);
        

    }

    public void ReduceLife(int damage)
    {
        //enemy takes health bij specific number, and dies when belowe 1
        if (Lifes >= 1)
        {
            Lifes -= damage; //Takes a life when damage taken (use damage for enemies)
            Destroy(HitPoints[Lifes].gameObject); //Destroys hitpoint when damage was taken
            if (Lifes < 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Stun()
    {
        //enemy unable to move
        m_agent.speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //player takes damage
        if (other.gameObject.CompareTag("Target"))
        {
            Debug.Log("enemy");
            ph.GetComponent<PlayerHealth>().TakeDamage(1);
        }

        if (other.gameObject.CompareTag("Attack"))
        {
            TakeDamage(playerDamage);
        }
    }

    //takes damage
    void TakeDamage(int damage)
    {
        health -= damage;
    }

    //destroy object
    void Death()
    {
        Destroy(gameObject);
    }
}
