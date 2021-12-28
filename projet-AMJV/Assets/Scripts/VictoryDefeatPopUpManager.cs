using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VictoryDefeatPopUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
