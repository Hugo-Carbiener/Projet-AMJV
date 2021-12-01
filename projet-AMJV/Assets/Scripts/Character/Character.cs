using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int[] cooldowns;
    private int[] damages;
    private bool[] OnCooldown = { false, false, false };
    public bool castingSpell = false;
    public void CastSpell(string spell, int index)
    {
        if (OnCooldown[index] || castingSpell)
        {
            Debug.Log("On cooldown of" + cooldowns[index]);
            return;
        }
        else
        {
            StartCoroutine(spell);
        }
    }
}
