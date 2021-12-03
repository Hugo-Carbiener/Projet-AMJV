using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField]
    private float walkingSpeed;
    [SerializeField]
    private float runningSpeed;
    [SerializeField]
    private Animator anim;
    private float speed;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        SetDefaultAnimation();
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

        if (rb.velocity != Vector3.zero && speed == runningSpeed)
        {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsWalking", false);
        } else if (rb.velocity != Vector3.zero && speed == walkingSpeed)
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsWalking", true);
        }
        else 
        {
            SetDefaultAnimation();
        }

        rb.velocity = vector * speed;
    }

    public void SetDefaultAnimation()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsWalking", false);
    }
}
