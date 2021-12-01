using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Knight : Character
{
    [SerializeField] GameObject SwordStrike;

    private Classes characterClass = Classes.Knight;
    private int[] cooldowns = { 1, 5, 5 };
    private int[] damages = { 5, 2, 3 };
    private bool[] OnCooldown = { false, false, false };


    private int health = 50  ;

    
    public IEnumerator MainSpell()
    {
        base.castingSpell = true;
        
        Debug.Log("Main spell");
        castingSpell = false;
        OnCooldown[0] = true;
        yield return new WaitForSeconds(cooldowns[0]);
        OnCooldown[0] = false;
    }

    public IEnumerator SecondarySpell()
    {
        castingSpell = true;

        Debug.Log("Sec spell");
        castingSpell = false;
        OnCooldown[1] = true;
        yield return new WaitForSeconds(cooldowns[1]);
        OnCooldown[1] = false;
    }

    public IEnumerator MovementSpell()
    {
        castingSpell = true;

        Debug.Log("Movement spell");
        castingSpell = false;
        OnCooldown[2] = true;
        yield return new WaitForSeconds(cooldowns[2]);
        OnCooldown[2] = false;
    }
}


/*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(SwordStrike, );*/
