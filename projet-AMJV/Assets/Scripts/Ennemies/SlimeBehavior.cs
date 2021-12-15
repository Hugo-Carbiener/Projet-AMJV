using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private GroundDetector gdDetector;
    [SerializeField]
    private Health healthManager;

    [SerializeField]
    private int initialHealth = 10;
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float jumpCooldown = 1.5f;
    [SerializeField]
    private float horizontalForce = 2;
    [SerializeField]
    private float verticalForce = 3;

    private bool isGrounded;
    private int level = 4;
    public void setLevel(int lvl) { this.level = lvl; }
    public void setInitialHealth(int health) { this.initialHealth = health; }

    private void Awake()
    {
        if (!player) player = GameObject.FindWithTag("Player");
        if (!animator) animator = GetComponentInChildren<Animator>();
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!gdDetector) gdDetector = GetComponentInChildren<GroundDetector>();

        // set health and subscribe death event
        if (healthManager) healthManager = GetComponent<Health>();
        healthManager.OnDeath += Death;
        healthManager.setHealth(initialHealth);
        healthManager.setMaxHealth(initialHealth);
    }

    private void Start()
    {
        InvokeRepeating("Jump", 0, jumpCooldown);
    }

    private void Jump()
    {
        Vector3 velocity = player.transform.position - transform.position;
        velocity.y = 0;
        velocity = velocity.normalized;
        velocity = velocity * horizontalForce;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        rb.AddForce(Vector3.up * verticalForce, ForceMode.Impulse);
    }

    private void Death()
    {
        SlimeSpawner slimeSpawner = GameObject.Find("SlimeSpawner").GetComponent<SlimeSpawner>();
        Debug.Log("Slime is ded");
        if ( level > 1 ) {
            slimeSpawner.SpawnSlime(level - 1, gameObject);
            slimeSpawner.SpawnSlime(level - 1, gameObject);
        }
        
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        isGrounded = gdDetector.IsGrounded();
        animator.SetBool("IsJumping", !isGrounded);

        if(isGrounded)
        {
            Vector3 velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
        }
    }
}
