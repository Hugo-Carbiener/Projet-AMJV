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
    private Health healthManager;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject buildUpObject;
    [Header("Variables")]
    private float attackArea = 15;
    private float aggroArea = 50;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackRadius;
    private int initialHealth = 50;
    private bool isOnCooldown;
    private States state;

    private enum States
    {
        idle,
        attacking,
        following
    }

    private void Awake()
    {
        if (!healthManager) healthManager = GetComponent<Health>();
        healthManager.OnDeath += Death;
        healthManager.setHealth(initialHealth);
        healthManager.setMaxHealth(initialHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!rockFallPrefab) rockFallPrefab = Resources.Load("RockFall") as GameObject;
        if (!healthManager) healthManager = GetComponent<Health>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!buildUpObject) buildUpObject = transform.Find("BuildUpMarker").gameObject;
        buildUpObject.SetActive(true);

        agent.speed = movementSpeed;

        

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
                agent.isStopped = true;
                agent.ResetPath();
                anim.SetBool("IsAttacking", false);
                anim.SetBool("IsWalking", false);
                break;
            case States.attacking:
                agent.isStopped = true;
                agent.ResetPath();
                if (!isOnCooldown)
                {
                    StartCoroutine(Attack());
                    anim.SetBool("IsWalking", false);
                }
                break;
            case States.following:
                agent.isStopped = false;
                agent.SetDestination(player.position);
                anim.SetBool("IsAttacking", false);
                anim.SetBool("IsWalking", true);
                break;
        }
    }

    private void ReconsiderLifeChoices()
    {
        float playerMonsterDistance = Vector3.Distance(player.position, transform.position);

        if (playerMonsterDistance > aggroArea)
        {
            state = States.idle;
        }
        else if (playerMonsterDistance > attackArea && playerMonsterDistance < aggroArea)
        {
            state = States.following;
        }
        else
        {
            state = States.attacking;
        }
    }

    private IEnumerator Attack()
    {
        StartCoroutine(Cooldown());

        // process attack angle
        Vector3 offset = player.transform.position - transform.position;
        float angle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;
        angle *= Mathf.Deg2Rad;
        Vector3 attackPos = transform.position + new Vector3(attackRange * Mathf.Cos(angle), 1 - transform.position.y, attackRange * Mathf.Sin(angle));

        // build up
        //Debug.Log("buildIp");
        buildUpObject.SetActive(true);
        anim.Play("Attack");
        Vector3 markerPos = new Vector3(attackPos.x, buildUpObject.transform.position.y, attackPos.z);
        buildUpObject.transform.position = markerPos;
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(0.5f);
        buildUpObject.SetActive(false);


        // smash
        //Debug.Log("Smahs");
        Smash(attackPos);
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
                //Debug.Log(attackDamage(playerAttackDistance, attackRadius));
                player.GetComponent<Health>().Damage(attackDamage(playerAttackDistance, attackRadius));
                player.GetComponent<Rigidbody>().AddForce((player.position - transform.position) * 200);
            }
        }
    }

    private int attackDamage(float distance, float attackRadius)
    {
        int value = (int)(-100 / attackRadius * distance + 100);
        if (value < 0) return 0;
        else return (int) (-100/attackRadius * distance + 100);
    }

    private IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(7);
        isOnCooldown = false;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
