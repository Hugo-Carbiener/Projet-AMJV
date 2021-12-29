using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform player;
    private float speed = 10;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            player.GetComponent<Health>().Damage(5);
            player.GetComponent<Rigidbody>().AddForce((player.position - transform.position) * 200);
        }

        if(other.gameObject.tag != "Monster")
        {
            Destroy(gameObject);
        }
    }
}
