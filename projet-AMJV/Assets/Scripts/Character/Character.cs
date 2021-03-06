using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    protected Animator animator;
    protected MouseAngle MouseAngle;
    protected Rigidbody rb;
    protected Health healthManager;
    protected GroundDetector gdDetector;

    protected int initialHealth;
    protected int[] cooldowns;
    protected int[] durations;
    protected bool[] OnCooldown;
    protected bool isSpeedBoosted;
    protected bool isImmobilised;
    protected bool castingSpell = false;
    protected Vector3 worldMousePos;
    protected GameObject groundMarker;
    protected LineRenderer lineRdr;
    protected SpriteRenderer spriteRdr;

    private GameObject defeatPopUp;

    private string character_choice;
    private GameObject mainCooldown;
    private GameObject secondaryCooldown;
    private GameObject movementCooldown;
    private Image mainCooldownImage;
    private Image secondaryCooldownImage;
    private Image movementCooldownImage;

    protected List<string> spells = new List<string>(){ "MainSpell", "SecondarySpell", "MovementSpell" };

    protected virtual void OnAwake()
    {
        Time.timeScale = 1;
        character_choice = PlayerPrefs.GetString("Character");
        animator = GetComponentInChildren<Animator>();
        MouseAngle = GetComponentInParent<MouseAngle>();
        rb = GetComponent<Rigidbody>();
        gdDetector = GetComponentInChildren<GroundDetector>();
        groundMarker = GameObject.Find("GroundMarker");
        healthManager = GetComponent<Health>();
        groundMarker.SetActive(false);

        mainCooldown = GameObject.Find("MainCooldown");
        secondaryCooldown = GameObject.Find("SecondaryCooldown");
        movementCooldown = GameObject.Find("MovementCooldown");
        mainCooldownImage = mainCooldown.GetComponent<Image>();
        secondaryCooldownImage = secondaryCooldown.GetComponent<Image>();
        movementCooldownImage = movementCooldown.GetComponent<Image>();

        lineRdr = GetComponent<LineRenderer>();
        lineRdr.positionCount = 2;
        lineRdr.enabled = false;
        spriteRdr = GetComponentInChildren<SpriteRenderer>();
        healthManager.OnDeath += Death;
    }

    public int getIntialHealth() { return initialHealth; }
    public bool IsSpeedBoosted() { return isSpeedBoosted; }

    public bool IsImmobilised() { return isImmobilised; }
    
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
        if (GetComponent<PlayerManager>().getClass() == Classes.Knight)
        {
            CancelInvoke();
        }
        
    }

    protected IEnumerator StartSpellCooldown(string spell)
    {
        int spellIndex = spells.IndexOf(spell);
        OnCooldown[spellIndex] = true;
        if (spell == "MainSpell")
        {
            var maintempColor = mainCooldownImage.color;
            maintempColor.a = 0.5f;
            mainCooldownImage.color = maintempColor;
        }
        if (spell == "SecondarySpell")
        {
            var secondtempColor = secondaryCooldownImage.color;
            secondtempColor.a = 0.5f;
            secondaryCooldownImage.color = secondtempColor;
        }
        if (spell == "MovementSpell")
        {
            var movtempColor = movementCooldownImage.color;
            movtempColor.a = 0.5f;
            movementCooldownImage.color = movtempColor;
        }
        yield return new WaitForSeconds(cooldowns[spellIndex]);
        if (spell == "MainSpell")
        {
            var maintempColor = mainCooldownImage.color;
            maintempColor.a = 0f;
            mainCooldownImage.color = maintempColor;
        }
        if (spell == "SecondarySpell")
        {
            var secondtempColor = secondaryCooldownImage.color;
            secondtempColor.a = 0f;
            secondaryCooldownImage.color = secondtempColor;
        }
        if (spell == "MovementSpell")
        {
            var movtempColor = movementCooldownImage.color;
            movtempColor.a = 0f;
            movementCooldownImage.color = movtempColor;
        }
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
        Color baseColor = spriteRdr.color;
        spriteRdr.color = Color.cyan;

        isSpeedBoosted = true;
        yield return new WaitForSeconds(duration);
        isSpeedBoosted = false;

        spriteRdr.color = baseColor;
    }

    protected IEnumerator Invulnerability(float duration)
    {
        healthManager.SetInvulnerability(true);
        yield return new WaitForSeconds(duration);
        healthManager.SetInvulnerability(false);
    }

    private void Death()
    {
        defeatPopUp.SetActive(true);
        Classes playerClass = GetComponent<PlayerManager>().getClass();
        PlayerPrefs.SetInt(playerClass + "NbOfGames", PlayerPrefs.GetInt(playerClass + "NbOfGames") + 1);
        Time.timeScale = 0;
    }

    public void setDefeatPopUp(GameObject defeatPopUp)
    {
        this.defeatPopUp = defeatPopUp;
    }
}
