using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    [Header("Fireball variables")]
    [SerializeField]
    private float fireballRange = 10;

    private GameObject fireballPrefab;
    private GameObject icewallPrefab;

    private void Awake()
    {
        base.OnAwake();
        initialHealth = 40;
        cooldowns = new int[] { 1, 10, 5 };
        durations = new int[] { 0, 0, 0 };
        OnCooldown = new bool[] { false, false, false };

        fireballPrefab = Resources.Load("Fireball") as GameObject;
        icewallPrefab = Resources.Load("Icewall") as GameObject;
    }

    public IEnumerator MainSpell()
    {
        StartCoroutine(StartSpellCooldown("MainSpell"));
        StartCoroutine(StartSpellDuration("MainSpell"));

        Fireball();
        yield return null;
    }

    public IEnumerator SecondarySpell()
    {
        StartCoroutine(StartSpellCooldown("SecondarySpell"));
        StartCoroutine(StartSpellDuration("SecondarySpell"));

        Icewall();
        yield return null;
    }

    public IEnumerator MovementSpell()
    {
        StartCoroutine(StartSpellDuration("MovementSpell"));

        Transposition();
        yield return null;
    }

    private void Fireball()
    {
        GameObject fireball = Instantiate(fireballPrefab);
        float mouseAngle = MouseAngle.getMouseAngle();
        fireball.transform.position = new Vector3(gameObject.transform.position.x + fireballRange * Mathf.Cos(mouseAngle * Mathf.Deg2Rad), gameObject.transform.position.y + 5f, gameObject.transform.position.z + fireballRange * Mathf.Sin(mouseAngle * Mathf.Deg2Rad));
    }

    private void Icewall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Instantiate(icewallPrefab, hit.point, Quaternion.Euler(0, 0, 0));
        }
    }

    private void Transposition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction);
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.tag == "Monster")
            {
                StartCoroutine(StartSpellCooldown("MovementSpell"));
                Vector3 temp = hit.collider.gameObject.transform.position;
                hit.collider.gameObject.transform.position = transform.position;
                hit.collider.gameObject.transform.Translate(Vector3.up * 5);
                transform.position = temp;
            }
        }
    }

}
