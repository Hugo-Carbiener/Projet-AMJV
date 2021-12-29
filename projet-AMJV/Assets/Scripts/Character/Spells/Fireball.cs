using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float explosionRadius = 10;
    [SerializeField]
    private float knockbackIntensity = 200;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float maxRange = 10;
    private Vector3 startingPos;

    private bool isImmobile;
    private bool hasExplosed;

    private void Start()
    {
        StartCoroutine(WaitOnStartUp());
        hasExplosed = false;
        startingPos = transform.position;
        if (!explosionPrefab) explosionPrefab = Resources.Load("Explosion") as GameObject;
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    private void FixedUpdate()
    {
        if (!isImmobile)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
        if (Vector3.Distance(startingPos, transform.position) >= maxRange)
        {
            if (!hasExplosed)
            {
                Debug.Log("Went too far");
                Explosion();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Ground" && other.gameObject.tag != "Player" && other.gameObject.name != "GroundDetector")
        {
            if (!hasExplosed)
            {
                Debug.Log("Hit " + other.gameObject.name);
                Explosion();
            }
        }
    }

    private void Explosion()
    {
        hasExplosed = true;
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Monster" || hitCollider.gameObject.tag == "Player")
            {
                hitCollider.GetComponent<Health>().Damage(5);
                hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position - transform.position) * knockbackIntensity);
            }
        }
        Destroy(gameObject);
    }

    private IEnumerator WaitOnStartUp()
    {
        isImmobile = true;
        yield return new WaitForSeconds(0.5f);
        isImmobile = false;
    }
}
