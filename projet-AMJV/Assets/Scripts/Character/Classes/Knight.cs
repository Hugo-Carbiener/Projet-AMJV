using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Knight : Character
{
    [Header("Sword Strike variables")]
    [SerializeField]
    private float knockbackIntensity = 500;
    [SerializeField]
    private float swordStrikeRadius = 1f;
    [SerializeField]
    private float attackRange = 0.75f;

    [Header("Whirlwind variables")]
    [SerializeField]
    private float whirlwindRadius = 2f;
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
        StartCoroutine(StartSpellCooldown("MainSpell"));
        StartCoroutine(StartSpellDuration("MainSpell"));

        SwordStrike();
        yield return null;
    }

    public IEnumerator SecondarySpell()
    {
        StartCoroutine(StartSpellCooldown("SecondarySpell"));
        StartCoroutine(StartSpellDuration("SecondarySpell"));

        SpeedBoost(durations[spells.IndexOf("SecondarySpell")]);
        Invulnerability(durations[spells.IndexOf("SecondarySpell")]);
        InvokeRepeating("Whirlwind", 0, 0.5f);

        yield return null;
    }

    public IEnumerator MovementSpell()
    {
        StartCoroutine(StartSpellCooldown("MovementSpell"));
        StartCoroutine(StartSpellDuration("MovementSpell"));

        Jump();
        yield return null;
    }

    private void SwordStrike()
    {
        GetMousePosition();
        Vector3 center = worldMousePos;
        center = (center - transform.position).normalized * attackRange;
        center = transform.position + center;
        center.y = 1;

        Collider[] hitColliders = Physics.OverlapSphere(center, swordStrikeRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Monster")
            {
                Debug.Log(hitCollider.name);
                hitCollider.GetComponent<Health>().Damage(5);
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
            if (hitCollider.gameObject.tag == "Monster")
            {
                hitCollider.GetComponent<Health>().Damage(1);
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