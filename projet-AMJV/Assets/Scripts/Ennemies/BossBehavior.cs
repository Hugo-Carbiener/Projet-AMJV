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
    private float maxDistance = 10;
    [SerializeField]
    private int damageCharge = 10;
    [SerializeField]
    private float knockbackCharge = 5;
    [SerializeField]
    private float radiusCharge = 3;
    [SerializeField]
    private float firstChargeCooldown = 5;
    [SerializeField]
    private float secondChargeCooldown = 2;

    [Header("Immobilisaiton variables")]
    [SerializeField]
    private float immoTime;
    [SerializeField]
    private float staticCooldown;

    private float timer;
    private int previousPhase;
    private int actualPhase;

    private void Awake()
    {
        if (!player) player = GameObject.FindWithTag("Player");
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!healthManager) healthManager = GetComponent<Health>();
        if (!bulletPrefab) bulletPrefab = Resources.Load("Bullet") as GameObject;
    }

    private void Start()
    {
        Debug.Log("is in Start()");
        previousPhase = 1;
        actualPhase = 1;
        InvokeRepeating("Shoot", 0, firstShootCooldown);
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
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            timer = 0f;
        }

    }


    //--------------------------------------------------------------------------------------------------
    // Second third of life : charging

    private void Charge()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, maxDistance);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusCharge);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Player")
            {
                hitCollider.GetComponent<Health>().Damage(damageCharge);
                hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position - transform.position) * knockbackCharge);
                Debug.Log("Hit " + hitCollider.gameObject.name);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------
    // Last third of life

    private void Immobilize()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit) && hit.collider.tag == "Player") 
        {
            hit.rigidbody.velocity = Vector3.zero;
            StartCoroutine(immobilization(immoTime));
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit) && hit.collider.tag == "Player")
        {
            hit.rigidbody.velocity = Vector3.zero;
            StartCoroutine(immobilization(immoTime));
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit) && hit.collider.tag == "Player")
        {
            hit.rigidbody.velocity = Vector3.zero;
            StartCoroutine(immobilization(immoTime));
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit) && hit.collider.tag == "Player")
        {
            hit.rigidbody.velocity = Vector3.zero;
            StartCoroutine(immobilization(immoTime));
        }


        IEnumerator immobilization(float time)
        {
            yield return new WaitForSeconds(time);
        }
    }






}
