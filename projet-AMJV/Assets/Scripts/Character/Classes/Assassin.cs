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
    private float temp3;



    private void Awake()
    {
        base.OnAwake();
        initialHealth = 45;
        cooldowns = new int[] { 10, 5, 1 };
        durations = new int[] { 0, 0, 1 };
        OnCooldown = new bool[] { false, false, false };
    }

    public IEnumerator MainSpell()
    {
        StartCoroutine(StartSpellCooldown("MainSpell"));
        StartCoroutine(StartSpellDuration("MainSpell"));
        Debug.Log("Main spell");

        Sprint();
        yield return null;
    }

    public IEnumerator SecondarySpell()
    {
        StartCoroutine(StartSpellCooldown("SecondarySpell"));
        StartCoroutine(StartSpellDuration("SecondarySpell"));

        PoisonFlask();
        Debug.Log("Sec spell");
        yield return null;
    }

    public IEnumerator MovementSpell()
    {
        StartCoroutine(StartSpellCooldown("MovementSpell"));
        StartCoroutine(StartSpellDuration("MovementSpell"));
        Debug.Log("Movement spell");

        Dash();
        yield return null;
    }

    private void Sprint()
    {

    }

    private void PoisonFlask()
    {
    }

    private void Dash()
    {
    }
}