using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth;
    private int health;

    [SerializeField]
    private float hitIndicatorDuration = 1;

    private void Update()
    {
        Debug.Log(health);
    }

    public int getMaxHealth() { return maxHealth; }
    public void setMaxHealth(int amount) { maxHealth = amount; }
    public void setHealth(int amount) { health = amount; }
    public void Damage(int dmg)
    {
        StartCoroutine("HitIndicator");

        if (health - dmg <= 0)
        {
            health = 0;
            Death();
        } 
        else
        {
            health -= dmg;
        }
    
    }

    private IEnumerator HitIndicator()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color baseColor = sr.color;
        Color newColor = baseColor;
        newColor.g = 0;
        newColor.b = 0;

        sr.color = newColor;
        yield return new WaitForSeconds(hitIndicatorDuration);
        sr.color = baseColor;
    }

    private void Death()
    {
        Debug.Log("You are dead.");
    }
}
