using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth;
    private int health;
    private bool isInvulnerable = false;
    private float hitIndicatorDuration = 0.2f;

    SpriteRenderer sr;

    public event Action OnDeath;
    public event Action OnHealthChange;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public int getMaxHealth() { return maxHealth; }

    public int getHealth() { return health; }
    public void setMaxHealth(int amount) { maxHealth = amount; }
    public void setHealth(int amount) { health = amount; }
    public void SetInvulnerability(bool vuln) { isInvulnerable = vuln; }

    public void Damage(int dmg)
    {
        float rd = UnityEngine.Random.value;
        bool isCriticalHit;

        if (rd < 0.1)
        {
            dmg *= 2;
            isCriticalHit = true;
        } else
        {
            isCriticalHit = false;
        }

        if (!isInvulnerable)
        {
            if (health - dmg <= 0)
            {
                health = 0;
                Death();
            }
            else
            {
                health -= dmg;
            }

            StartCoroutine("HitIndicator");
            DamagePopUp.Create(transform.position, dmg, isCriticalHit);
            OnHealthChange?.Invoke();
        }
    }

    private IEnumerator HitIndicator()
    {
        Color baseColor = sr.color;
        Color newColor = Color.red;

        sr.color = newColor;
        yield return new WaitForSeconds(hitIndicatorDuration);
        sr.color = baseColor;
    }

    private void Death()
    {
        OnDeath?.Invoke();
        Debug.Log("You are dead.");
    }
}
