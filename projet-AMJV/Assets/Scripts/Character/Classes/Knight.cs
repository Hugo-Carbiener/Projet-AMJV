using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Knight : Character
{
    [Header("Sword Strike variables")]
    [SerializeField]
    private float knockbackIntensity = 200;
    [SerializeField]
    private float swordStrikeRadius = 5;
    [SerializeField]
    private float attackRange = 5;

    [Header("Whirlwind variables")]
    [SerializeField]
    private float whirlwindRadius = 10;
    [Header("Jump variables")]
    [SerializeField]
    private float jumpDuration = 0.5f;
    [SerializeField]
    private float jumpHeight = 20;
    [SerializeField]
    private float jumpLandingRadius = 10;

    private void Awake()
    {
        base.OnAwake();
        initialHealth = 50;
        cooldowns = new int[] { 1, 5, 5 };
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

        StartCoroutine(Jump());
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

    private IEnumerator Jump()
    {
        rb.isKinematic = true;
        isImmobilised = true;
        healthManager.SetInvulnerability(true);

        // update marker
        groundMarker.SetActive(true);

        // jump
        Vector3 start = transform.position;
        Ray end = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(end, out hit, Mathf.Infinity))
        {
            groundMarker.transform.position = hit.point;
            for (float time = 0; time < jumpDuration; time += Time.deltaTime)
            {
                Vector3 pos = Vector3.Lerp(start, hit.point, time / jumpDuration);
                pos.y = Mathf.Log(1 + Mathf.PingPong((2 * jumpHeight / jumpDuration) * time, jumpHeight));
                transform.position = pos;
                yield return null;
            }
        }

        groundMarker.SetActive(false);
        // landing
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, jumpLandingRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Monster")
            {
                hitCollider.GetComponent<Health>().Damage(2);
                hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position - transform.position) * knockbackIntensity);
                Debug.Log("Hit " + hitCollider.gameObject.name);
            }
        }

        isImmobilised = false;
        rb.isKinematic = false;
        healthManager.SetInvulnerability(false);
    }
}