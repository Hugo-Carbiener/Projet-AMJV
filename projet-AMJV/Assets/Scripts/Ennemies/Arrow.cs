using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform player;
    private float speed = 20f;

    private float lifetime = 10;
    private float counter;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 initialPos = new Vector3(transform.position.x, player.position.y / 2, transform.position.z);
        transform.position = initialPos;
        transform.LookAt(player);
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        counter = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if(counter < 0)
        {
            Destroy(gameObject);
        }

        counter -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag != "Monster" && other.gameObject.name != "GroundDetector")
        {
            Debug.Log("arrow hit" + other.name);
            if(other.gameObject.tag == "Player")
            {
                player.GetComponent<Health>().Damage(5);
                player.GetComponent<Rigidbody>().AddForce((player.position - transform.position) * 100);
            }
            Destroy(gameObject);
        }
    }
}
