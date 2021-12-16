using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Animator animator;
    protected MouseAngle MouseAngle;
    protected Rigidbody rb;
    protected Health healthManager;

    protected int initialHealth;
    protected int[] cooldowns;
    protected int[] durations;
    protected bool[] OnCooldown;
    protected bool isSpeedBoosted;
    protected bool castingSpell = false;
    protected Vector3 worldMousePos;

    protected List<string> spells = new List<string>(){ "MainSpell", "SecondarySpell", "MovementSpell" };

    protected virtual void OnAwake()
    {
        animator = GetComponentInChildren<Animator>();
        MouseAngle = GetComponentInParent<MouseAngle>();
        rb = GetComponent<Rigidbody>();
    }

    public int getIntialHealth() { return initialHealth; }
    public bool IsSpeedBoosted() { return isSpeedBoosted; }
    
    public void CastSpell(string spell)
    {
        int index = spells.IndexOf(spell);

        if (OnCooldown[index] || castingSpell)
        {
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
        /*if (GetComponent<PlayerManager>().getClass() == Classes.Knight)
        {
            CancelInvoke();
        }*/
        
    }

    protected IEnumerator StartSpellCooldown(string spell)
    {
        int spellIndex = spells.IndexOf(spell);
        OnCooldown[spellIndex] = true;
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

    protected IEnumerator SpeedBoost(float duration)
    {
        isSpeedBoosted = true;
        yield return new WaitForSeconds(duration);
        isSpeedBoosted = false;
    }

    protected IEnumerator Invulnerability(float duration)
    {
        healthManager.SetInvulnerability(true);
        yield return new WaitForSeconds(duration);
        healthManager.SetInvulnerability(false);
    }
}
