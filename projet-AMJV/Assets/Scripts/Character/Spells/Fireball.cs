using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float explosionRadius = 5;
    [SerializeField]
    private float knockbackIntensity = 200;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float maxRange = 10;
    private Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;
        if (!explosionPrefab) explosionPrefab = Resources.Load("Explosion") as GameObject;
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    private void FixedUpdate()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        if (Vector3.Distance(startingPos, transform.position) >= maxRange)
        {
            Debug.Log("Went too far");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground" && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
                Debug.Log("Boom");
        }
    }

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Monster")
            {
                hitCollider.GetComponent<Health>().Damage(5);
                hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position - transform.position) * knockbackIntensity);
                Debug.Log("Hit " + hitCollider.gameObject.name);
            }
        }
    }
}
