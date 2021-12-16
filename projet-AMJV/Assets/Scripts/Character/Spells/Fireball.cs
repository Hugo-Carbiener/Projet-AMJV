using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 direction;

    public void SetDirection(Vector3 newDirection) => direction = newDirection;

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
