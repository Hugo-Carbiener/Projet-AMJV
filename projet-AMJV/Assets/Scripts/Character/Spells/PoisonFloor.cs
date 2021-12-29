using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonFloor : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awakening");
        InvokeRepeating("PoisonDamage", 0, 0.2f);
    }

    private void PoisonDamage()
    {
        Debug.Log("Hitting");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        foreach(Collider collider in colliders)
        {
            if(collider.gameObject.tag == "Monster" || collider.gameObject.tag == "Player")
            {
                collider.gameObject.GetComponent<Health>().Damage(1);
            }
        }
    }
}
