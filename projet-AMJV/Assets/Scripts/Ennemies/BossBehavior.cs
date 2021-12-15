using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Health healthManager;
    [SerializeField]
    private GameObject slimePrefab;

    [SerializeField]
    private int initialHealth = 10;
    [SerializeField]
    private int bossHealth;
    [SerializeField]
    private int bossMaxHealth;
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float reloadTimer = 2;
    [SerializeField]
    private ProjManager bulletPrefab;
    [SerializeField]
    private float shootCooldown = 2;


    private float timer;
    private int previousPhase;
    private int actualPhase;

    private void Awake()
    {
        if (!player) player = GameObject.FindWithTag("Player");
        if (!animator) animator = GetComponentInChildren<Animator>();
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!healthManager) healthManager = GetComponent<Health>();
    }

    private void Start()
    {
        previousPhase = 1;
        actualPhase = 1;
        InvokeRepeating("Shoot", 0, shootCooldown);
        Instantiate(slimePrefab);
        healthManager.OnHealthChange += phaseManager;
    }


    private void phaseManager()
    {
        bossHealth = healthManager.getHealth();
        bossMaxHealth = healthManager.getMaxHealth();

        if (bossHealth < bossMaxHealth * (2/3) &&  bossHealth > bossMaxHealth *(1/3))
        {
            actualPhase = 2;
        }

        if (bossHealth < bossMaxHealth * (1/3))
        {
            actualPhase = 3;
        }

        if (actualPhase != previousPhase)
        {
            if (actualPhase == 2)
            {
                CancelInvoke();
                InvokeRepeating("SecondSpell", 0, cooldown);
                previousPhase = 2;
            }
            else
            {
                CancelInvoke();
                InvokeRepeating("ThirdSpell", 0, cooldownBis);
                previousPhase = 3;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------
    // First third of life : shooting items

    private void Shoot()
    {
        if (!player) return;
        timer += Time.deltaTime;
        if (timer >= reloadTimer)
        {
            ProjManager bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetDirection(GetShootDirection());
            timer = 0f;
        }

    }

    private Vector3 GetShootDirection()
    {
        if (player.transform.position.x > transform.position.x)
        {
            return Vector3.right;
        }
        return Vector3.left;
    }

}
