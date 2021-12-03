using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Knight : Character
{
    private Classes characterClass = Classes.Knight;
    private int health = 50  ;

    [SerializeField]
    private float knockbackIntensity;

    private void Start()
    {
        cooldowns = new int[] {1, 5, 5};
        OnCooldown = new bool[] { false, false, false };
    }

    public IEnumerator MainSpell()
    {
        StartCoroutine(StartSpellCooldown("MainSpell"));
        Debug.Log("Main spell");
        yield return null;
    }

    public IEnumerator SecondarySpell()
    {
        StartCoroutine(StartSpellCooldown("SecondarySpell"));
        Debug.Log("Sec spell");
        yield return null;
    }

    public IEnumerator MovementSpell()
    {
        StartCoroutine(StartSpellCooldown("MovementSpell"));
        
        Debug.Log("Movement spell");
        yield return null;
    }

    private void SwordStrike(Vector3 center, float radius)
    {
        Gizmos.DrawSphere(center, radius);

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            // hitCollider.GetComponent<Ennemy>().dealDamage(5);
            hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position - center) * knockbackIntensity);
            Debug.Log("Hit " + hitCollider.gameObject.name);
        }
    }
}