using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemBehavior : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Transform player;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private GameObject rockFallPrefab;
    [SerializeField]
    private GameObject buildUpPrefab;
    [SerializeField]
    private Health healthManager;

    [Header("Variables")]
    private float attackArea = 5;
    private float aggroArea = 50;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackRadius;
    private bool isOnCooldown;
    private States state;

    private enum States
    {
        idle,
        attacking,
        following
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        //if (!rockFallPrefab) rockFallPrefab = Resources.Load("RockFall") as GameObject;
        if (!healthManager) healthManager = GetComponent<Health>();

        agent.speed = movementSpeed;

        healthManager.OnDeath += Death;
        healthManager.setHealth(10);
        healthManager.setMaxHealth(10);

        isOnCooldown = false;
    }


    // Update is called once per frame
    void Update()
    {
        ReconsiderLifeChoices();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case States.idle:
                agent.ResetPath();
                break;
            case States.attacking:
                agent.ResetPath();
                if (!isOnCooldown)
                {
                    StartCoroutine(Attack());
                }
                break;
            case States.following:
                agent.SetDestination(player.position);
                break;
        }
    }

    private void ReconsiderLifeChoices()
    {
        float playerMonsterDistance = Vector3.Distance(player.position, transform.position);

        if (playerMonsterDistance < aggroArea)
        {
            state = States.attacking;
        }
        else if (playerMonsterDistance > attackArea && playerMonsterDistance < aggroArea)
        {
            state = States.following;
        }
        else
        {
            state = States.idle;
        }
    }

    private IEnumerator Attack()
    {
        StartCoroutine(Cooldown());
        // process attack angle
        Vector3 offset = player.transform.position - transform.position;
        float angle = Mathf.Atan2(offset.z, offset.x);
        Vector3 attackPos = new Vector3(attackRange * Mathf.Cos(angle), 1, attackRange * Mathf.Sin(angle));

        // build up
        Debug.Log("buildIp");
        Instantiate(buildUpPrefab, attackPos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        // smash
        Debug.Log("Smahs");
        Smash(attackPos);
        
        yield return new WaitForSeconds(0.5f);
        Instantiate(rockFallPrefab, attackPos, Quaternion.identity);
    }

    private void Smash(Vector3 attackPos)
    {
        Collider[] colliders = Physics.OverlapSphere(attackPos, attackRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                float playerAttackDistance = Vector3.Distance(attackPos, player.position);
                Debug.Log(attackDamage(playerAttackDistance, attackRadius));
                //player.GetComponent<Health>().Damage(attackDamage(playerAttackDistance, attackRadius));
                //player.GetComponent<Rigidbody>().AddForce((player.position - transform.position) * 200);

            }
        }
    }

    private int attackDamage(float distance, float attackRadius)
    {
        return (int) (-100/attackRadius * distance + 100);
    }

    private IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(2);
        isOnCooldown = false;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
