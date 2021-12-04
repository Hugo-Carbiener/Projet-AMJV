using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private Character character;
    private Vector3 worldMousePos;

    private void Start()
    {
        character = GetComponent<PlayerManager>().getCharacter();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            character.CastSpell("MainSpell");
        }
        if (Input.GetMouseButtonDown(1))
        {
            character.CastSpell("SecondarySpell");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.CastSpell("MovementSpell");
        }
    }
}
