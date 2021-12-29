using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 50;
    private float explosionRadius = 5;
    private float knockbackIntensity = 200;
    private GameObject explosionPrefab;
    private float maxRange = 50;
    private Vector3 startingPos;

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private ParticleSystem fire;
    [SerializeField]
    private ParticleSystem smoke;

    private bool isImmobile;
    private bool hasExploded;

    private void Start()
    {
        StartCoroutine(WaitOnStartUp());
        hasExploded = false;
        startingPos = transform.position;
        if (!explosionPrefab) explosionPrefab = Resources.Load("Explosion") as GameObject;
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    private void FixedUpdate()
    {
        if (!isImmobile && !hasExploded)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
        if (Vector3.Distance(startingPos, transform.position) >= maxRange)
        {
            if (!hasExploded)
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
            if (!hasExploded)
            {
                Debug.Log("Hit " + other.gameObject.name);
                Explosion();
            }
        }
    }

    private void Explosion()
    {
        hasExploded = true;
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
        StartCoroutine(EndOfLife());
    }

    private IEnumerator EndOfLife()
    {
        ball.SetActive(false);
        fire.Stop();
        smoke.Stop();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private IEnumerator WaitOnStartUp()
    {
        isImmobile = true;
        yield return new WaitForSeconds(0.5f);
        isImmobile = false;
    }
}
