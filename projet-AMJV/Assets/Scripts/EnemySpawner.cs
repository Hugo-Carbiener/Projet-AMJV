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
    [SerializeField] GameObject enemyPrefab;
    private List<GameObject> ennemies = new List<GameObject>();

    private void Start()
    {
        numberOfWave = Random.Range(4, 8);
        Debug.Log(ennemies);
        StartCoroutine(WaitThree());
        NewWave();
    }
    private void NewWave()
    {
        int indexOfSpawner = Random.Range(0, spawners.Length);
        for (int i=0; i<numberOfEnnemy; i++)
        {
            ennemies.Add(Instantiate(enemyPrefab, spawners[indexOfSpawner].transform.position, Quaternion.identity) as GameObject);
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

    IEnumerator WaitThree()
    {
        yield return new WaitForSeconds(3);
    }

    private void Update()
    {
        if (ennemies.Count == 0 && countNumberOfWave<numberOfWave)
        {
            NewWave();
        }
        if (countNumberOfWave == numberOfWave)
        {
            // pop-up de victoire
        }
    }




}
