using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjManager : MonoBehaviour
{
    private enum projType
    {
        dammage,
        knockback,
        fireball
    }

    private Vector3 direction;
    [SerializeField] private projType proj;
    [SerializeField] private float speed;
    [SerializeField] [Range(1, 10)] private float knockbackForce = 1;


    //setting a function to instantiate the direction of the projectile from another script
    public void SetDirection(Vector3 newDirection) => direction = newDirection;


    //making the projectile moving
    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }



    private void OnCollisionEnter(Collision collision)
    {
        tag = collision.gameObject.tag;

        if (tag == "Player")
        {
            //Testing if the rigidbody exist
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (!playerRigidbody) return; // early return if null

            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
            if (!playerManager) return; // early return if null

            //knockback
            if (proj == projType.knockback)
            { 
                playerRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
            }
            
        }

        Destroy(gameObject);
    }
}
