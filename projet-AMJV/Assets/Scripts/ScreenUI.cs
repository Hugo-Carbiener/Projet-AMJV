using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUI : MonoBehaviour
{
    private string character_choice;
    [SerializeField] private GameObject knightSpells;
    [SerializeField] private GameObject mageSpells;
    [SerializeField] private GameObject assassinSpells;

    private void Start()
    {
        character_choice = PlayerPrefs.GetString("Character");
        if (character_choice == "Knight")
        {
            mageSpells.SetActive(false);
            assassinSpells.SetActive(false);
        }
        else if (character_choice == "Mage")
        {
            knightSpells.SetActive(false);
            assassinSpells.SetActive(false);
        }
        else if (character_choice == "Assassin")
        {
            knightSpells.SetActive(false);
            mageSpells.SetActive(false);
        }
    }
}
