using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Assassin : Character
{

    [Header("Sprint variables")]
    [SerializeField]
    private float temps;
    [Header("PoisonFlask variables")]
    [SerializeField]
    private float temp2;
    [Header("Dash variables")]
    [SerializeField]
    private float duration = 0.1f;
    [SerializeField]
    private float lineHeight = 2;
    private bool isDashing = false;

    private float knockbackIntensity = 200;
    private SpriteRenderer spriteRdr;
    private GameObject smokePuff;

    private void Awake()
    {
        base.OnAwake();
        spriteRdr = GetComponentInChildren<SpriteRenderer>();
        smokePuff = Resources.Load("Smoke puff") as GameObject;
        initialHealth = 45;
        cooldowns = new int[] { 1, 5, 10 };
        durations = new int[] { 0, 0, 0 };
        OnCooldown = new bool[] { false, false, false };
    }

    public IEnumerator MainSpell()
    {
        StartCoroutine(StartSpellCooldown("MainSpell"));
        StartCoroutine(StartSpellDuration("MainSpell"));

        //StartCoroutine(HideSprite());
        StartCoroutine(Dash());
        yield return null;
    }

    public IEnumerator SecondarySpell()
    {
        StartCoroutine(StartSpellCooldown("SecondarySpell"));
        StartCoroutine(StartSpellDuration("SecondarySpell"));

        PoisonFlask();
        yield return null;
    }

    public IEnumerator MovementSpell()
    {
        StartCoroutine(StartSpellCooldown("MovementSpell"));
        StartCoroutine(StartSpellDuration("MovementSpell"));
        
        Sprint();
        yield return null;
    }

    private void Sprint()
    {
        SpeedBoost(2);
    }

    private void PoisonFlask()
    {

    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.isKinematic = true;
        isImmobilised = true;
        healthManager.SetInvulnerability(true);
        lineRdr.enabled = true;
        Instantiate(smokePuff, transform);

        // update marker
        groundMarker.SetActive(true);

        // pathpoints for line renderer
        Vector3[] pathPoints;

        Vector3 start = transform.position;
        Ray end = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 target = Vector3.zero;
        if (Physics.Raycast(end, out hit, Mathf.Infinity))
        {
            groundMarker.transform.position = hit.point;
            target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            for (float time = 0; time < duration; time += Time.deltaTime)
            {
                Vector3 pos = Vector3.Lerp(start, target, time / duration);
                pathPoints = new Vector3[] { new Vector3(start.x, start.y + lineHeight, start.z), new Vector3(transform.position.x, transform.position.y + lineHeight, transform.position.z) };
                lineRdr.SetPositions(pathPoints);
                transform.position = pos;
                yield return null;
            }
        }

        groundMarker.SetActive(false);

        lineRdr.enabled = false;
        Instantiate(smokePuff, transform);
        isImmobilised = false;
        rb.isKinematic = false;
        healthManager.SetInvulnerability(false);
        isDashing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isDashing && collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<Health>().Damage(5);
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.transform.position - transform.position) * knockbackIntensity);
            Debug.Log("Hit " + collision.gameObject.name);
        }
    }

    private IEnumerator HideSprite()
    {
        spriteRdr.enabled = false;
        yield return new WaitForSeconds(duration);
        spriteRdr.enabled = true;
    }
}