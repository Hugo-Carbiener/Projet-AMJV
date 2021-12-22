using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float knockbackForce = 2;
    private Vector2 direction;

    public void SetDirection(Vector2 newDirection) => direction = newDirection;

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
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

}
