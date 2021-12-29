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
    [SerializeField]
    private float fleeRange;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float shootDuration;

    private int cooldown = 5;
    private float counter;

    private void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!arrowPrefab) arrowPrefab = Resources.Load("Arrow") as GameObject;
        if (!healthManager) healthManager = GetComponent<Health>();

        healthManager.OnDeath += Death;
        healthManager.setHealth(10);
        healthManager.setMaxHealth(10);

    }

    private void Update()
    {
        counter -= Time.deltaTime;
        if(counter < 0)
        {
            ReconsiderLifeChoices();
        }
    }

    private void ReconsiderLifeChoices()
    {
        float playerMonsterDistance = Vector3.Distance(player.position, transform.position);
        if (playerMonsterDistance > fleeRange && playerMonsterDistance < attackRange)
        {
            StartCoroutine(ShootArrows());
        }
        else if (playerMonsterDistance < fleeRange)
        {
            transform.Translate((transform.position - player.position) * movementSpeed);
        }
        else
        {

        }

        counter = cooldown;
    }

    private IEnumerator ShootArrows()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.5f, (player.position - transform.position), out hit, attackRange))
        {
            if (hit.transform.tag == "Player")
            {
                int arrowNumber = Random.Range(0, 6);
                for (int i = 1; i <= arrowNumber; i++)
                {
                    Instantiate(arrowPrefab);
                    yield return new WaitForSeconds(shootDuration / arrowNumber);
                }
            }
        }
    }

    private bool SetDestination(Vector3 targetDestination)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetDestination, out hit, 1f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            return true;
        }
        return false;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
