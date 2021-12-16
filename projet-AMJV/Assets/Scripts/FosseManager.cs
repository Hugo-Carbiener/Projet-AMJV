using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FosseManager : MonoBehaviour
{
    [SerializeField] private int deathTime;


    private void OnCollisionEnter(Collision collision)
    {
        /*

        tag = collision.gameObject.tag;

        if (tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerManager>().isPushed == true)
            {
                collision.gameObject.GetComponent<PlayerManager>().isPushed = false;

                //on attend un peu avant de tuer le joueur
                StartCoroutine(timer(deathTime));
                StopCoroutine(timer(deathTime));

                Destroy(collision.gameObject);
            }

        }

        if (tag == "Monster")
        {
            if (collision.gameObject.GetComponent<MonsterManager>().isPushed == true)
            {
                collision.gameObject.GetComponent<MonsterManager>().isPushed = false;

                StartCoroutine(timer(deathTime));
                StopCoroutine(timer(deathTime));

                Destroy(collision.gameObject);
            }
        }*/

    }

    IEnumerator timer(int secs)
    {
        yield return new WaitForSeconds(secs);
    }
}
