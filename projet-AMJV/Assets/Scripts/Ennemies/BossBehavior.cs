using System.Collections;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [Header("Prefab variables")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Health healthManager;
    [SerializeField]
    private GameObject slimePrefab;

    [Header("Boss variables")]
    private int bossHealth = 100;
    private int bossMaxHealth = 100;

    [Header("Shoot variables")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float timeShoot = 0.48f;
    [SerializeField]
    private float firstShootCooldown = 2;

    [Header("Charge variables")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int buildingCharge = 5;
    [SerializeField]
    private float duration = 0.1f;
    [SerializeField]
    private float knockbackCharge = 5;
    [SerializeField]
    private float firstChargeCooldown = 5;
    [SerializeField]
    private float secondChargeCooldown = 2;

    [Header("Immobilisaiton variables")]
    [SerializeField]
    private float immoTime;
    [SerializeField]
    private float immoRayRadius = 10;


    private float timer;
    private int previousPhase;
    private int actualPhase;
    private bool isDashing;

    private void Awake()
    {
        if (!player) player = GameObject.FindWithTag("Player");
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!healthManager) healthManager = GetComponent<Health>();
        healthManager.setHealth(bossHealth);
        healthManager.setMaxHealth(bossMaxHealth);
        healthManager.OnDeath += Death;
        if (!bulletPrefab) bulletPrefab = Resources.Load("Bullet") as GameObject;
        if (!animator) animator = GetComponentInChildren<Animator>();

    }

    private void Start()
    {
        //Debug.Log("is in Start()");
        previousPhase = 1;
        actualPhase = 1;
        InvokeRepeating("Shoot", 0, firstShootCooldown);
        //InvokeRepeating("ChargeManager", 0, firstChargeCooldown);
        //InvokeRepeating("Immobilize", 0, staticCooldown);
        Instantiate(slimePrefab);
        healthManager.OnHealthChange += phaseManager;
    }


    private void phaseManager()
    {
        //Debug.Log("in phaseManager");
        //Debug.Log("actual phase before change = " + actualPhase);
        bossHealth = healthManager.getHealth();
        //Debug.Log("boss health = " + bossHealth);
        int oneThird = 33;
        int twoThird = 66;

        if (bossHealth <= twoThird)
        {
            if(bossHealth > oneThird)
            {
                actualPhase = 2;
            }

            else
            {
                actualPhase = 3;
            }
        }

        //Debug.Log("actual phase after changer = " + actualPhase);
        //Debug.Log("previous phase = " + previousPhase);

        if (actualPhase != previousPhase)
        {
            if (actualPhase == 2)
            {
                //Debug.Log("in phase 2");
                CancelInvoke();
                InvokeRepeating("ChargeManager", 0, firstChargeCooldown);
                previousPhase = 2;
            }
            else
            {
                //Debug.Log("in phase 3");
                CancelInvoke();
                //Immobilize();
                InvokeRepeating("Charge", 0, secondChargeCooldown);
                previousPhase = 3;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------
    // First third of life : shooting items

    private void Shoot()
    {
        //Debug.Log("is in Shoot");
        if (!player) return;
        //Debug.Log("timer: " + timer);
        animator.Play("Fire");
        while (timer <= timeShoot)
        {
            //Debug.Log("in timer");
            timer += Time.deltaTime;
        }
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        timer = 0f;
        animator.SetBool("IsFiring", false);

    }


    //--------------------------------------------------------------------------------------------------
    // Second third of life : charging

    private void ChargeManager()
    {
        //Debug.Log("in charge manager");
        StartCoroutine(Charge());
    }


    private IEnumerator Charge()
    {
        Vector3 target = new Vector3();
        target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //Debug.Log("is in charge");
        isDashing = true;
        rb.isKinematic = true;

        animator.SetBool("IsBuildingUpCharge", true);
        yield return new WaitForSeconds(buildingCharge);
        animator.SetBool("IsBuildingUpCharge", false);

        animator.SetBool("IsCharging", true);
        Vector3 start = transform.position;
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            Vector3 pos = Vector3.Lerp(start, target, time / duration);
            transform.position = pos;
            yield return null;
        }
        animator.SetBool("IsCharging", false);
        rb.isKinematic = false;
        isDashing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDashing && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().Damage(5);
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.transform.position - transform.position) * knockbackCharge);
            //Debug.Log("Hit " + collision.gameObject.name);
        }
    }

    //-------------------------------------------------------------------------------------------------------
    // Last third of life

    private void Immobilize()
    {
        //Debug.Log("is in immonilize");

        animator.Play("Summon");
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, immoRayRadius, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.collider.tag == "Player")
        {
            //Debug.DrawLine(transform.position, hit.point, Color.cyan, 80);
            hit.rigidbody.velocity = Vector3.zero;
            StartCoroutine(immobilization(immoTime));
        }

        if (Physics.SphereCast(transform.position, immoRayRadius, transform.TransformDirection(Vector3.back), out hit) && hit.collider.tag == "Player")
        {
            hit.rigidbody.velocity = Vector3.zero;
            StartCoroutine(immobilization(immoTime));
        }

        if (Physics.SphereCast(transform.position, immoRayRadius, transform.TransformDirection(Vector3.right), out hit) && hit.collider.tag == "Player")
        {
            hit.rigidbody.velocity = Vector3.zero;
            StartCoroutine(immobilization(immoTime));
        }

        if (Physics.SphereCast(transform.position, immoRayRadius, transform.TransformDirection(Vector3.left), out hit) && hit.collider.tag == "Player")
        {
            hit.rigidbody.velocity = Vector3.zero;
            StartCoroutine(immobilization(immoTime));
        }

        animator.SetBool("IsSummoning", false);


        IEnumerator immobilization(float time)
        {
            //Debug.Log("in immo coroutine");
            yield return new WaitForSeconds(time);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }




}
