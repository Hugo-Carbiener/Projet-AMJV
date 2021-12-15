using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void Update()
    {
        if (player == null)
        {
            //pop-up de game over
        }


    }
}
