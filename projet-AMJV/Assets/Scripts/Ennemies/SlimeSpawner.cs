using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;
    private int[]  slimeHealth = { 2, 5, 10, 20 };
    private Color[] slimeColor = { Color.white, Color.cyan, Color.green, Color.black };

    private void Start()
    {
        if(!slimePrefab) slimePrefab = Resources.Load("Prefabs/Slime") as GameObject;
    }

    public void SpawnSlime(int level, GameObject parentSlime)
    {
        GameObject newSlime = Instantiate(slimePrefab);
        newSlime.transform.position = parentSlime.transform.position;
        SlimeBehavior newSlimeBehavior = newSlime.GetComponent<SlimeBehavior>();
        newSlimeBehavior.setLevel(level);
        newSlimeBehavior.setInitialHealth(slimeHealth[level - 1]);
        newSlime.GetComponentInChildren<SpriteRenderer>().color = slimeColor[level - 1];

        Debug.Log("Instantiate new slime");
    }
}
