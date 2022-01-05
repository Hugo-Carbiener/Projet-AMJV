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
    [SerializeField]
    private int bossHealth;
    [SerializeField]
    private int bossMaxHealth;
    [SerializeField]
    private float reloadTimer = 0.05f;

    [Header("Shoot variables")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float firstShootCooldown = 2;
    [SerializeField]
    private float secondShootCooldown = 1;

    [Header("Charge variables")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float searchRadius = 10;
    [SerializeField]
    private int buildingCharge = 5;
    [SerializeField]
    private float duration = 0.1f;
    [SerializeField]
    private int damageCharge = 10;
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
    private float staticCooldown;
    [SerializeField]
    private float immoRayRadius = 0.5f;


    private float timer;
    private int previousPhase;
    private int actualPhase;
    private bool isDashing;
    private bool isImmobilised;

    private void Awake()
    {
        if (!player) player = GameObject.FindWithTag("Player");
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!healthManager) healthManager = GetComponent<Health>();
        if (!bulletPrefab) bulletPrefab = Resources.Load("Bullet") as GameObject;
        if (!animator) animator = GetComponentInChildren<Animator>();

    }

    private void Start()
    {
        Debug.Log("is in Start()");
        previousPhase = 1;
        actualPhase = 1;
        //InvokeRepeating("Shoot", 0, firstShootCooldown);
        //InvokeRepeating("ChargeManager", 0, firstChargeCooldown);
        InvokeRepeating("Immobilize", 0, staticCooldown);
        //Instantiate(slimePrefab);
        //healthManager.OnHealthChange += phaseManager;
    }


    private void phaseManager()
    {
        bossHealth = healthManager.getHealth();
        bossMaxHealth = healthManager.getMaxHealth();

        if (bossHealth < bossMaxHealth * (2/3) &&  bossHealth > bossMaxHealth *(1/3))
        {
            actualPhase = 2;
        }

        if (bossHealth <= bossMaxHealth * (1/3))
        {
            actualPhase = 3;
        }

        if (actualPhase != previousPhase)
        {
            if (actualPhase == 2)
            {
                CancelInvoke();
                InvokeRepeating("Shoot", 0, secondShootCooldown);
                InvokeRepeating("Charge", 0, firstChargeCooldown);
                previousPhase = 2;
            }
            else
            {
                CancelInvoke();
                InvokeRepeating("Charge", 0, secondChargeCooldown);
                InvokeRepeating("Immobilize", 0, staticCooldown);
                previousPhase = 3;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------
    // First third of life : shooting items

    private void Shoot()
    {
        Debug.Log("is in Shoot");
        if (!player) return;
        timer += Time.deltaTime;
        Debug.Log("timer: " + timer);
        if (true)
        {
            Debug.Log("in timer");
            animator.SetBool("IsFiring", true);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            timer = 0f;
            animator.SetBool("IsFiring", false);
        }

    }


    //--------------------------------------------------------------------------------------------------
    // Second third of life : charging

    private void ChargeManager()
    {
        Debug.Log("in charge manager");
        StartCoroutine(Charge());
    }


    private IEnumerator Charge()
    {
        Vector3 target = new Vector3();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Player")
            {
                target = hitCollider.gameObject.transform.position;
            }
        }
        Debug.Log("is in charge");
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
            Debug.Log("Hit " + collision.gameObject.name);
        }
    }

    //-------------------------------------------------------------------------------------------------------
    // Last third of life

    private void Immobilize()
    {
        Debug.Log("is in immonilize");

        animator.SetBool("IsSummoning", true);
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, immoRayRadius, transform.TransformDirection(Vector3.forward), out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.cyan, 80);
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
            Debug.Log("in immo coroutine");
            yield return new WaitForSeconds(time);
        }
    }






}
