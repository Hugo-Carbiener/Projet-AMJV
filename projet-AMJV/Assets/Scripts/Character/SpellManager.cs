using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private Character character;
    private string character_choice;
    private Classes characterClass;
    private Vector3 worldMousePos;
    [SerializeField] private AudioSource knightMainSpell;
    [SerializeField] private AudioSource mageMainSpell;
    [SerializeField] private AudioSource mageSecondarySpell;
    [SerializeField] private AudioSource mageMovementSpell;


    private void Start()
    {
        character = GetComponent<PlayerManager>().getCharacter();
        character_choice = PlayerPrefs.GetString("Character");
        if (character_choice == "Assassin")
        {
            characterClass = Classes.Assassin;
        }
        else if (character_choice == "Knight")
        {
            characterClass = Classes.Knight;
        }
        else if (character_choice == "Mage")
        {
            characterClass = Classes.Mage;
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            character.CastSpell("MainSpell");
            if (characterClass == Classes.Knight)
            {
                knightMainSpell.Play();
            }
            else if (characterClass == Classes.Mage)
            {
                mageMainSpell.Play();
            }
            else if (characterClass == Classes.Assassin)
            {
                
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            character.CastSpell("SecondarySpell");
            if (characterClass == Classes.Knight)
            {
                
            }
            else if (characterClass == Classes.Mage)
            {
                mageSecondarySpell.Play();
            }
            else if (characterClass == Classes.Assassin)
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.CastSpell("MovementSpell");
            if (characterClass == Classes.Knight)
            {
                
            }
            else if (characterClass == Classes.Mage)
            {
                mageMovementSpell.Play();
            }
            else if (characterClass == Classes.Assassin)
            {

            }
        }
    }
}
