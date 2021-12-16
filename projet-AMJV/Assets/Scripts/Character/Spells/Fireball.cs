using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float explosionRadius = 1;
    [SerializeField]
    private float knockbackIntensity = 200;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    private void Update()
    {
        transform.Translate(- Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explosion();
        Destroy(gameObject);
    }

    private void Explosion()
    {
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
