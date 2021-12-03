using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int[] cooldowns;
    protected bool[] OnCooldown;
    protected bool castingSpell = false;

    protected List<string> spells = new List<string>(){ "MainSpell", "SecondarySpell", "MovementSpell" };
    public void CastSpell(string spell)
    {
        int index = spells.IndexOf(spell);

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

    protected IEnumerator StartSpellDuration(int duration)
    {
        castingSpell = true;
        yield return new WaitForSeconds(duration);
        castingSpell = false;
    }

    protected IEnumerator StartSpellCooldown(string spell)
    {
        int spellIndex = spells.IndexOf(spell);
        Debug.Log("Start cooldown for spell " + spellIndex);
        OnCooldown[spellIndex] = true;
        Debug.Log("cooldown duration " + cooldowns[spellIndex]);
        yield return new WaitForSeconds(cooldowns[spellIndex]);
        OnCooldown[spellIndex] = false;
    }
}
