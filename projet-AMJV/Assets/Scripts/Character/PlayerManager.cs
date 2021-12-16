using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Classes characterClass;
    [SerializeField]
    private Health healthManager;
    private Character character;
    private int maxHealth;
    private int health;
    private string character_choice;

    public Character getCharacter() { return this.character; }
    public Classes getClass()
    {
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
        return this.characterClass;
    }

    private void Awake()
    {
        if (!healthManager) healthManager = GetComponent<Health>();

        if (characterClass == Classes.Knight)
        {
            character = (Knight)gameObject.AddComponent(typeof(Knight));
        }
        else if (characterClass == Classes.Mage)
        {
            character = (Mage)gameObject.AddComponent(typeof(Mage));
        }
        else if (characterClass == Classes.Assassin)
        {
            character = (Assassin)gameObject.AddComponent(typeof(Assassin));
        }

        healthManager.setMaxHealth(character.getIntialHealth());
        healthManager.setHealth(character.getIntialHealth());
    }
}
