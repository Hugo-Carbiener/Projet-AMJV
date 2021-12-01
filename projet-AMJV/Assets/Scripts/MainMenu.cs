using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject characterSelect;
    private GameObject parametersMenu;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("MainMenuContainer");
        characterSelect = GameObject.Find("CharacterSelectContainer");
        parametersMenu = GameObject.Find("ParametersContainer");
        characterSelect.SetActive(false);
        parametersMenu.SetActive(false);
    }

    public void GoToCharacterSelect()
    {
        characterSelect.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void GoToParameters()
    {
        parametersMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackToMainMenuFromCharSelect()
    {
        mainMenu.SetActive(true);
        characterSelect.SetActive(false);
    }

    public void BackToMainMenuFromParameters()
    {
        mainMenu.SetActive(true);
        parametersMenu.SetActive(false);
    }

}
