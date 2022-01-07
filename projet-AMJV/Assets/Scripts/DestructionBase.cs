using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescructionBase : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
