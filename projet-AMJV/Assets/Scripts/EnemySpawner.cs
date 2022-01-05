using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawners;
    private int numberOfEnnemy = 1;
    private int previousNumberOfEnnemy = 1;
    private int numberOfWave;
    private int countNumberOfWave = 0;
    private GameObject player;
    private Vector3 start_position;
    private int indexOfSpawner;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject bossPrefab;
    private int count;
    private GameObject victoryPopUp;
    private GameObject defeatPopUp;
    private float timer;
    private float min;
    private float sec;
    [SerializeField] private TMPro.TextMeshProUGUI textCurrentWave;
    [SerializeField] private TMPro.TextMeshProUGUI textTimer;
    [SerializeField] private TMPro.TextMeshProUGUI timeVictory;
    [SerializeField] private TMPro.TextMeshProUGUI timeDefeat;
    [SerializeField] private TMPro.TextMeshProUGUI waveDefeat;

    private void Start()
    {
        Time.timeScale = 1;
        victoryPopUp = GameObject.Find("VictoryPopUpCanvas");
        victoryPopUp.SetActive(false);
        defeatPopUp = GameObject.Find("DefeatPopUpCanvas");
        defeatPopUp.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        start_position = player.transform.position;
        numberOfWave = Random.Range(4, 8);
        NewWave();
    }
    private void NewWave()
    {
        indexOfSpawner = Random.Range(0, spawners.Length);
        for (int i=0; i<numberOfEnnemy; i++)
        {
            Instantiate(enemyPrefab, spawners[indexOfSpawner].transform.position, Quaternion.identity);
            StartCoroutine(WaitOne());
        }
        int temp = numberOfEnnemy;
        numberOfEnnemy = numberOfEnnemy + previousNumberOfEnnemy;
        previousNumberOfEnnemy = temp;
        countNumberOfWave++;
    }

    IEnumerator WaitOne()
    {
        yield return new WaitForSeconds(1);
    }

    private void Update()
    {
        timer = Time.timeSinceLevelLoad;
        min = Mathf.Floor(timer / 60);
        sec = Mathf.RoundToInt(timer % 60);
        textTimer.text = string.Format("{0:00}:{1:00}", min, sec);
        count = GameObject.FindGameObjectsWithTag("Monster").Length;
        if ( count == 0 && countNumberOfWave<numberOfWave-1 && countNumberOfWave>0)
        {
            player.transform.position = start_position;
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.setHealth(playerHealth.getMaxHealth());
            Debug.Log("nouvelle vague");
            NewWave();
            textCurrentWave.text = countNumberOfWave.ToString();

        }
        if (countNumberOfWave == numberOfWave-1)
        {
            indexOfSpawner = Random.Range(0, spawners.Length);
            Instantiate(bossPrefab, spawners[indexOfSpawner].transform.position, Quaternion.identity);
            if (count == 0)
            {
                countNumberOfWave++;
            }
        }
        if (countNumberOfWave == numberOfWave)
        {
            player.transform.position = start_position;
            timeVictory.text = string.Format("{0:00}:{1:00}", min, sec);
            Time.timeScale = 0;
            victoryPopUp.SetActive(true);
        }

        if (Time.timeScale == 0 && countNumberOfWave != numberOfWave)
        {
            timeDefeat.text = string.Format("{0:00}:{1:00}", min, sec);
            waveDefeat.text = countNumberOfWave.ToString();
        }
    }




}
