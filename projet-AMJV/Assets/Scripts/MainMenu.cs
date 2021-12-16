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
        if (mainVolumeSlider != null && PlayerPrefs.HasKey("volume"))
        {
            float wantedMainVolume = PlayerPrefs.GetFloat("mainVolume", 0);
            float wantedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 0);
            float wantedEffectsVolume = PlayerPrefs.GetFloat("effectsVolume", 0);
            mainVolumeSlider.value = wantedMainVolume;
            musicVolumeSlider.value = wantedMusicVolume;
            effectsVolumeSlider.value = wantedEffectsVolume;
            AudioListener.volume = wantedMainVolume;
            mixer.SetFloat(masterVolume, wantedMainVolume);
            mixer.SetFloat(musicVolume, wantedMusicVolume);
            mixer.SetFloat(soundEffectsVolume, wantedEffectsVolume);
            mainVolumeSlider.onValueChanged.AddListener(delegate { SelectGeneralVolume(); });
            mainVolumeSlider.onValueChanged.AddListener(delegate { SelectMusicVolume(); });
            mainVolumeSlider.onValueChanged.AddListener(delegate { SelectSoundEffectsVolume(); });

        }
    }

    public void GoToCharacterSelect()
    {
        characterSelect.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ChooseAssassin()
    {
        PlayerPrefs.SetString("Character","Assassin");
        indexArena = Random.Range(0, 3);
        SceneManager.LoadScene(arenas[indexArena]);
    }

    public void ChooseKnight()
    {
        PlayerPrefs.SetString("Character", "Knight");
        indexArena = Random.Range(0, 3);
        SceneManager.LoadScene(arenas[indexArena]);
    }

    public void ChooseMage()
    {
        PlayerPrefs.SetString("Character", "Mage");
        indexArena = Random.Range(0, 3);
        SceneManager.LoadScene(arenas[indexArena]);
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

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
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

    public void StartOnClick()
    {
        SceneManager.LoadScene("test");
    }
}
