using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockfall : MonoBehaviour
{
    [SerializeField]
    private Sprite rock1;
    [SerializeField]
    private Sprite rock2;
    [SerializeField]
    private Sprite rock3;
    [SerializeField]
    private GameObject rockPrefab;

    private void Start()
    {        
        if (!rockPrefab) rockPrefab = Resources.Load("Rock") as GameObject;
        InvokeRepeating("RockFall", 0, 1);
    }

    private void RockFall()
    {
        GameObject rock = Instantiate(rockPrefab);
        float rdDegree = Random.Range(0, 360) * Mathf.Deg2Rad;
        float rdRange = Random.Range(0, 10);
        int rdSprite = Random.Range(0, 3);
        rock.transform.position = transform.position + new Vector3(rdRange * Mathf.Cos(rdDegree), -5.4f, rdRange * Mathf.Sin(rdRange));
        Sprite sprite = rock1;
        switch(rdSprite)
        {
            case 0:
                sprite = rock1;
                break;
            case 1:
                sprite = rock2;
                break;
            case 2:
                sprite = rock3;
                break;
        }
        rock.transform.Find("FallingRock").GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
