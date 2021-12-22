using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    [SerializeField] private float knockbackForce = 2;
    private Rigidbody rb;
    private Vector3 direction;

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
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (!playerRigidbody) return; // early return if null

            Character playerMouvementManger = collision.gameObject.GetComponent<Character>();
            if (!playerMouvementManger) return; // early return if null

            playerRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
        rb.AddForce(newDirection*1000);
    }

}
