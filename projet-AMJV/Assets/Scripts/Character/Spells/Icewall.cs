using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icewall : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        for (float y = -10; y < 0; y += Time.deltaTime * 10)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(5);
        StartCoroutine(Descend());
        StartCoroutine(Decay());
        
    }
    private IEnumerator Descend()
    {
        for (float y = 0; y > -10; y -= Time.deltaTime)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return null;
        }
    }

    private IEnumerator Decay()
    {
        int childrenAmount = transform.childCount;
        for (int i = 0; i < childrenAmount; i++)
        {
            Debug.Log("Kill a cube");
            int rd = Random.Range(0, transform.childCount);
            Destroy(transform.GetChild(rd).gameObject);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(transform.parent.gameObject);
    }
}
