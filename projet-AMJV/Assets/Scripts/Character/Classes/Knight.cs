using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Knight : Character
{
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
    [SerializeField]
    private float verticalForce = 3;
    [SerializeField]
    private float horizontalForce = 6;

    


    private void Awake()
    {
        base.OnAwake();
        initialHealth = 50;
        cooldowns = new int[] {1, 5, 0};
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

        Jump();
        yield return null;
    }

    private void SwordStrike()
    {
        GetMousePosition();
        Vector3 center = worldMousePos;
        center.y = transform.position.y;
        center = (center - transform.position).normalized * attackRange;
        center = transform.position + center;

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

    private void Jump()
    {
        rb.AddForce(verticalForce * Vector3.up, ForceMode.Impulse);
        float mouseAngle = MouseAngle.getMouseAngle() * Mathf.Deg2Rad;
        Vector3 mousePos = new Vector3(Mathf.Cos(mouseAngle), 1, Mathf.Sin(mouseAngle));
        rb.AddForce(horizontalForce * mousePos, ForceMode.Impulse);
    }
}