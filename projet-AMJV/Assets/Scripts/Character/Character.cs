using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Animator animator;
    protected int[] cooldowns;
    protected int[] durations;
    protected bool[] OnCooldown;
    protected bool castingSpell = false;
    protected Vector3 worldMousePos;

    protected List<string> spells = new List<string>(){ "MainSpell", "SecondarySpell", "MovementSpell" };

    protected virtual void OnAwake()
    {
        animator = GetComponentInChildren<Animator>();
    }

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

    protected IEnumerator StartSpellDuration(string spell)
    {
        animator.SetBool("Casting" + spell, true);
        castingSpell = true;
        yield return new WaitForSeconds(durations[spells.IndexOf(spell)]);
        castingSpell = false;
        animator.SetBool("Casting" + spell, false);
        
        // Need to cancel an invoke for the whirlwind
        if (GetComponent<PlayerManager>().getClass() == Classes.Knight)
        {
            CancelInvoke();
        }
        
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

    protected void GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 1000))
        {
            worldMousePos = hitData.point;
        }
    }
}
