using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    [Header("Fireball variables")]
    private float fireballRange = 1;
    [Header("Icewall variables")]
    private float temp2;
    [Header("Transposition variables")]
    private float temp3;

    private GameObject fireballPrefab;

    private void Awake()
    {
        base.OnAwake();
        initialHealth = 40;
        cooldowns = new int[] { 0, 10, 5 };
        durations = new int[] { 0, 0, 0 };
        OnCooldown = new bool[] { false, false, false };

        fireballPrefab = Resources.Load("Fireball") as GameObject;
    }

    public IEnumerator MainSpell()
    {
        StartCoroutine(StartSpellCooldown("MainSpell"));
        StartCoroutine(StartSpellDuration("MainSpell"));
        Debug.Log("Main spell");

        Fireball();
        yield return null;
    }

    public IEnumerator SecondarySpell()
    {
        StartCoroutine(StartSpellCooldown("SecondarySpell"));
        StartCoroutine(StartSpellDuration("SecondarySpell"));
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

    private void Fireball()
    {
        GameObject fireball = Instantiate(fireballPrefab);
        float mouseAngle = MouseAngle.getMouseAngle();
        Debug.Log(mouseAngle);
        fireball.transform.position = new Vector3(gameObject.transform.position.x + fireballRange * Mathf.Cos(mouseAngle * Mathf.Deg2Rad), gameObject.transform.position.y + 0.5f, gameObject.transform.position.z + fireballRange * Mathf.Sin(mouseAngle * Mathf.Deg2Rad));
        //fireball.GetComponent<Fireball>().SetAngle(mouseAngle);
    }
}
