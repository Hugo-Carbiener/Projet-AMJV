using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    private Classes characterClass = Classes.Mage;
    private int health = 0;

    [Header("Fireball variables")]
    private float temp1;
    [Header("Icewall variables")]
    private float temp2;
    [Header("Transposition variables")]
    private float temp3;

    private void Awake()
    {
        base.OnAwake();
        cooldowns = new int[] { 2, 10, 5 };
        durations = new int[] { 0, 0, 0 };
        OnCooldown = new bool[] { false, false, false };
    }

    public IEnumerator MainSpell()
    {
        StartCoroutine(StartSpellCooldown("MainSpell"));
        StartCoroutine(StartSpellDuration("MainSpell"));
        Debug.Log("Main spell");


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
}
