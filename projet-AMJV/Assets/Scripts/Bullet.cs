using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    [SerializeField] private float knockbackForce = 1000;
    [SerializeField] private int dammages = 5;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 30);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Player")
            {
                hitCollider.GetComponent<Health>().Damage(dammages);
                hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position - transform.position) * knockbackForce);
                Debug.Log("Hit " + hitCollider.gameObject.name);
            }
        }
        Destroy(gameObject);
    }

}
