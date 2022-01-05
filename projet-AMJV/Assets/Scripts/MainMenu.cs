using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject characterSelect;
    private GameObject parametersMenu;
    [SerializeField] Slider mainVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider effectsVolumeSlider;
    [SerializeField] TMPro.TMP_Dropdown resolutionDropdown;
    [SerializeField] TMPro.TMP_Dropdown qualityDropdown;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource click_fx;
    private string masterVolume = "MasterVolume";
    private string musicVolume = "MusicVolume";
    private string soundEffectsVolume = "SoundEffectsVolume";
    private string[] arenas = { "Arena", "Arena2", "Arena3" };
    private int indexArena;
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
        click_fx.Play();
    }

    public void ChooseAssassin()
    {
        PlayerPrefs.SetString("Character","Assassin");
        indexArena = Random.Range(0, 3);
        SceneManager.LoadScene(arenas[indexArena]);
        click_fx.Play();
    }

    public void ChooseKnight()
    {
        PlayerPrefs.SetString("Character", "Knight");
        indexArena = Random.Range(0, 3);
        SceneManager.LoadScene(arenas[indexArena]);
        click_fx.Play();
    }

    public void ChooseMage()
    {
        PlayerPrefs.SetString("Character", "Mage");
        indexArena = Random.Range(0, 3);
        SceneManager.LoadScene(arenas[indexArena]);
        click_fx.Play();
    }

    public void GoToParameters()
    {
        parametersMenu.SetActive(true);
        mainMenu.SetActive(false);
        float mainVolume;
        mixer.GetFloat(masterVolume, out mainVolume);
        mainVolumeSlider.value = mainVolume;
        float mVolume;
        mixer.GetFloat(musicVolume, out mVolume);
        musicVolumeSlider.value = mVolume;
        float effectsVolume;
        mixer.GetFloat(soundEffectsVolume, out effectsVolume);
        effectsVolumeSlider.value = effectsVolume;
        click_fx.Play();

    }

    public void BackToMainMenuFromCharSelect()
    {
        mainMenu.SetActive(true);
        characterSelect.SetActive(false);
        click_fx.Play();
    }

    public void BackToMainMenuFromParameters()
    {
        mainMenu.SetActive(true);
        parametersMenu.SetActive(false);
        click_fx.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
        click_fx.Play();
    }

    public void SelectGeneralVolume()
    {
        mixer.SetFloat(masterVolume, mainVolumeSlider.value);
    }

    public void SelectMusicVolume()
    {
        mixer.SetFloat(musicVolume, musicVolumeSlider.value);
    }

    public void SelectSoundEffectsVolume()
    {
        mixer.SetFloat(soundEffectsVolume, effectsVolumeSlider.value);
    }

    public void SelectResolution()
    {
        if (resolutionDropdown.value == 0)
        {
            Screen.SetResolution(1680, 1050, true);
            Debug.Log("1680x1050");
        }
        if (resolutionDropdown.value == 1)
        {
            Screen.SetResolution(1440, 900, true);
            Debug.Log("1440x900");
        }
        if (resolutionDropdown.value == 2)
        {
            Screen.SetResolution(1280, 800, true);
            Debug.Log("1280x800");
        }
    }

    public void SelectQuality()
    {
        if (qualityDropdown.value == 0)
        {
            QualitySettings.SetQualityLevel(5, true);
            int qualityLevel = QualitySettings.GetQualityLevel();
            Debug.Log(qualityLevel);
        }
        if (qualityDropdown.value == 1)
        {
            QualitySettings.SetQualityLevel(4, true);
            int qualityLevel = QualitySettings.GetQualityLevel();
            Debug.Log(qualityLevel);
        }
        if (qualityDropdown.value == 2)
        {
            QualitySettings.SetQualityLevel(3, true);
            int qualityLevel = QualitySettings.GetQualityLevel();
            Debug.Log(qualityLevel);
        }
        if (qualityDropdown.value == 3)
        {
            QualitySettings.SetQualityLevel(2, true);
            int qualityLevel = QualitySettings.GetQualityLevel();
            Debug.Log(qualityLevel);
        }
        if (qualityDropdown.value == 4)
        {
            QualitySettings.SetQualityLevel(1, true);
            int qualityLevel = QualitySettings.GetQualityLevel();
            Debug.Log(qualityLevel);
        }
        if (qualityDropdown.value == 5)
        {
            QualitySettings.SetQualityLevel(0, true);
            int qualityLevel = QualitySettings.GetQualityLevel();
            Debug.Log(qualityLevel);
        }
    }
}
