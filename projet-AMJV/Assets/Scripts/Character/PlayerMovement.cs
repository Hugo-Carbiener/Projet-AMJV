using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkingSpeed;
    [SerializeField]
    private float runningSpeed;
    private float speed;
    private Vector3 facing;

    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 vector = Vector3.zero;
        
        // sprint
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runningSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = walkingSpeed;
        }

        // movement
        if (Input.GetKey(KeyCode.Z))
        {
            vector += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vector += Vector3.back;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            vector += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vector += Vector3.right;
        }

        rb.velocity = vector * speed;
        facing = vector;
        
    }
}
