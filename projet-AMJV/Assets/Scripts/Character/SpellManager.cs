using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private Character character;

    private void Start()
    {
        character = GetComponent<PlayerManager>().getCharacter();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            character.CastSpell("MainSpell", 0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            character.CastSpell("SecondarySpell", 1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.CastSpell("MovementSpell", 2);
        }
    }
}
