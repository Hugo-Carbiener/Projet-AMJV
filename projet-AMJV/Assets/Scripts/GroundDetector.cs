using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GroundDetector : MonoBehaviour
{
    [SerializeField]
    private SphereCollider sphCollider;
    private bool isGrounded;
    public bool IsGrounded() { return isGrounded; }

    private void Start()
    {
        if (!sphCollider) sphCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            // Debug.Log("Ground enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
            // Debug.Log("Ground exit");
        }
    }
}
