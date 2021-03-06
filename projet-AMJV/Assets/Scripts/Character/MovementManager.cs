using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField]
    private float walkingSpeed = 25;
    [SerializeField]
    private float runningSpeed = 50;
    private Character character;
    private Animator anim;
    private float speed;
    
    private Rigidbody rb;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        character = GetComponent<Character>();
        speed = walkingSpeed;
        SetDefaultAnimation();
    }

    private void Update()
    {
        if (!character.IsImmobilised())
        {
            Vector3 vector = Vector3.zero;

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
            }
            else if (rb.velocity != Vector3.zero && speed == walkingSpeed)
            {
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsWalking", true);
            }
            else
            {
                SetDefaultAnimation();
            }

            vector.y = rb.velocity.y / speed;
            rb.velocity = vector * speed;
        }
    }

    private void FixedUpdate()
    {
        if (character.IsSpeedBoosted())
        {
            speed = runningSpeed;
        } 
        else
        {
            speed = walkingSpeed;
        }
    }

    public void SetDefaultAnimation()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsWalking", false);
    }
}
