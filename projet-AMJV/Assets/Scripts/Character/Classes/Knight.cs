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
    [SerializeField]
    private float whirlwindRadius = 1f;
    [Header("Jump variables")]
    private float temp2;


    private void Awake()
    {
        base.OnAwake();
        cooldowns = new int[] {1, 5, 5};
        durations = new int[] { 0, 3, 0 };
        OnCooldown = new bool[] { false, false, false };
    }

    public IEnumerator MainSpell()
    {
        //StartCoroutine(StartSpellCooldown("MainSpell"));
        StartCoroutine(StartSpellDuration("MainSpell"));
        Debug.Log("Main spell");
       

        SwordStrike();
        yield return null;
    }

    public IEnumerator SecondarySpell()
    {
        StartCoroutine(StartSpellCooldown("SecondarySpell"));
        StartCoroutine(StartSpellDuration("SecondarySpell"));
        InvokeRepeating("Whirlwind", 0, 0.5f);
        Debug.Log("Sec spell");
        yield return null;
    }

    public IEnumerator MovementSpell()
    {
        StartCoroutine(StartSpellCooldown("MovementSpell"));
        StartCoroutine(StartSpellDuration("MovementSpell"));
        Debug.Log("Movement spell");
        yield return null;
    }

    private void SwordStrike()
    {
        GetMousePosition();
        Vector3 center = worldMousePos;
        center.y = transform.position.y;
        center = (center - transform.position).normalized * attackRange;
        center = transform.position + center;
        Debug.Log(center);
        //Instantiate(testSphere, center, Quaternion.Euler(0, 0, 0));

        Collider[] hitColliders = Physics.OverlapSphere(center, swordStrikeRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag != "Player")
            {
                // hitCollider.GetComponent<Ennemy>().dealDamage(5);
                hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position - center) * knockbackIntensity);
                Debug.Log("Hit " + hitCollider.gameObject.name);
            }
        }
    }

    private void Whirlwind()
    {   
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, whirlwindRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag != "Player")
            {
                // hitCollider.GetComponent<Ennemy>().dealDamage(1);
                hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position - transform.position) * knockbackIntensity);
                Debug.Log("Hit " + hitCollider.gameObject.name);
            }
        }
    }
}