using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{ 
    public bool stunned = false;
    public bool dead = false;
    public EnemyAttack Attack;

    private Animator animator;
    private NavMeshAgent navAgent;
    private float stunTimer = 0f;
    private float stunLength = 500f;
    private GameObject player;
    private Bounds wanderBounds;
    private float xPos;
    private float zPos;
    private float distFromPlayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        distFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        animator.SetFloat("MoveY", navAgent.velocity.magnitude / 3.5f);

        if (dead)
        {
            animator.SetBool("dead", true);
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(gameObject, 5);
        }
        else if (stunned && stunTimer <= stunLength)
        {
            animator.SetBool("stunned", true);
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            stunTimer++;
        }
        else
        {
            animator.SetBool("stunned", false);
            animator.SetBool("dead", false);
            stunned = false;
            stunTimer = 0;
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        }

        target();
        animator.SetBool("attacking", Attack.isAttacking);
    }

    void target()
    {
        if (dead || stunned)
        {
            Attack.isAttacking = false;
        }
        else if (distFromPlayer > 15.0f)
        {
            Attack.isAttacking = false;
            if (navAgent.pathStatus != NavMeshPathStatus.PathComplete || navAgent.remainingDistance <= 0.7f)
            {
                xPos = Random.Range(wanderBounds.min.x, wanderBounds.max.x);
                zPos = Random.Range(wanderBounds.min.z, wanderBounds.max.z);
                navAgent.SetDestination(new Vector3(xPos, transform.position.y, zPos));
            }
        }
        else if (distFromPlayer <= 0.7f)
        {
            navAgent.destination = player.transform.position;
            transform.rotation = Quaternion.LookRotation((player.transform.position - transform.position).normalized);
            Attack.isAttacking = true;
        }
        else
        {
            navAgent.destination = player.transform.position;
            Attack.isAttacking = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "WanderArea")
        {
            wanderBounds = col.gameObject.GetComponent<BoxCollider>().bounds;
        }
    }

}
