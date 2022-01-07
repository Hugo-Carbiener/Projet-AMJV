using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionBase : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Health>().Damage(100);
    }
}
