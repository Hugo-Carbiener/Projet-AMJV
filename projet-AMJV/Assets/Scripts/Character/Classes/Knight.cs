using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Knight : Character
{
    private Classes characterClass = Classes.Knight;
    private int health = 50  ;

    [Header("Sword Strike variables")]
    [SerializeField]
    private float knockbackIntensity = 100;
    [SerializeField]
    private float swordStrikeRadius = 0.5f;
    [SerializeField]
    private float attackRange = 0.5f;

    [Header("Whirlwind variables")]
    private float temp;
    [Header("Jump variables")]
    private float temp2;
    private void Start()
    {
        cooldowns = new int[] {1, 5, 5};
        OnCooldown = new bool[] { false, false, false };
    }

    public IEnumerator MainSpell()
    {
        StartCoroutine(StartSpellCooldown("MainSpell"));
        Debug.Log("Main spell");
       

        SwordStrike(swordStrikeRadius, attackRange);
        yield return null;
    }

    public IEnumerator SecondarySpell()
    {
        StartCoroutine(StartSpellCooldown("SecondarySpell"));
        StartCoroutine(StartSpellDuration(3));
        Debug.Log("Sec spell");
        yield return null;
    }

    public IEnumerator MovementSpell()
    {
        StartCoroutine(StartSpellCooldown("MovementSpell"));
        
        Debug.Log("Movement spell");
        yield return null;
    }

    private void SwordStrike(float radius, float range)
    {
        GetMousePosition();
        Vector3 center = worldMousePos;
        center.y = transform.position.y;
        center = (center - transform.position).normalized * range;
        center = transform.position + center;
        Debug.Log(center);
        //Instantiate(testSphere, center, Quaternion.Euler(0, 0, 0));

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            // hitCollider.GetComponent<Ennemy>().dealDamage(5);
            hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position - center) * knockbackIntensity);
            Debug.Log("Hit " + hitCollider.gameObject.name);
        }
    }
}