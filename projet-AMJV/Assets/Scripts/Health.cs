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

    public event Action OnDeath;
    public event Action OnHealthChange;

    private GameObject defeatPopUp;

    private void Start()
    {
        defeatPopUp = GameObject.Find("DefeatPopUpCanvas");
        defeatPopUp.SetActive(false);
    }
    public int getMaxHealth() { return maxHealth; }

    public int getHealth() { return health; }
    public void setMaxHealth(int amount) { maxHealth = amount; }
    public void setHealth(int amount) { health = amount; }
    public void SetInvulnerability(bool vuln) { isInvulnerable = vuln; }

    public void Damage(int dmg)
    {
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
            OnHealthChange?.Invoke();
        }
    }

    private IEnumerator HitIndicator()
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        Color baseColor = sr.color;
        Color newColor = Color.red;

        sr.color = newColor;
        yield return new WaitForSeconds(hitIndicatorDuration);
        sr.color = baseColor;
    }

    private void Death()
    {
        OnDeath?.Invoke();
        defeatPopUp.SetActive(true);
        Time.timeScale = 0;
    }
}
