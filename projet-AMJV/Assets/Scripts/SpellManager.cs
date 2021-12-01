using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // MainSpell();
        }
        if (Input.GetMouseButtonDown(1))
        {
            // SecondarySpell();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            // MovementSpell();
        }
    }
}
