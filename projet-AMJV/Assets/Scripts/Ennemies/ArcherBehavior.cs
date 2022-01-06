using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ArcherBehavior : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Transform player;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Health healthManager;

    [Header("Variables")]
    private float fleeRange = 20;
    private float attackRange = 50;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float shootDuration;
    private bool isOnCooldown;
    private States state;

    private enum States
    {
        following,
        fleeing,
        shooting
    }
    //private float counter;

    private void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!arrowPrefab) arrowPrefab = Resources.Load("Arrow") as GameObject;
        if (!healthManager) healthManager = GetComponent<Health>();

        agent.speed = movementSpeed;

        healthManager.OnDeath += Death;
        healthManager.setHealth(10);
        healthManager.setMaxHealth(10);

        isOnCooldown = false;
    }

    private void Update()
    {
        ReconsiderLifeChoices();
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case States.shooting:
                agent.ResetPath();
                break;
            case States.fleeing:
                agent.ResetPath();
                Vector3 dir = transform.position - player.position;
                dir.y = 0;
                transform.Translate(dir.normalized * movementSpeed * Time.deltaTime);
                break;
            case States.following:
                agent.SetDestination(player.position);
                break;
        }
    }
    
    private void ReconsiderLifeChoices()
    {
        float playerMonsterDistance = Vector3.Distance(player.position, transform.position);

        if (playerMonsterDistance > fleeRange && playerMonsterDistance < attackRange)
        {
            state = States.shooting;
            if (!isOnCooldown)
            {
                StartCoroutine(ShootArrows());
            }
        }
        else if (playerMonsterDistance < fleeRange)
        {
            state = States.fleeing;
        }
        else
        {
            state = States.following;
        }
    }
    
    private IEnumerator ShootArrows()
    {
        isOnCooldown = true;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.5f, (player.position - transform.position), out hit, attackRange))
        {
            if (hit.transform.tag == "Player")
            {
                int arrowNumber = Random.Range(0, 6);
                for (int i = 1; i <= arrowNumber; i++)
                {
                    Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(shootDuration / arrowNumber);
                }
            }
        }
        yield return new WaitForSeconds(5);
        isOnCooldown = false;
    }

    private IEnumerator WaitAtStart()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(10);
        isOnCooldown = false;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
