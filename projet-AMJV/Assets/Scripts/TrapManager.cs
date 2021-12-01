using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    private enum trapType
    {
        dammage,
        immobilisation,
        move
    }

    [SerializeField] private trapType trap;
    [SerializeField] private int dammages = 5;
    [SerializeField] private int immoTime = 2;
    

    private void OnCollisionEnter(Collision collision)
    {
        tag = collision.gameObject.tag;

        if (tag == "Player")
        {
            if (trap == trapType.dammage)
            {
                collision.gameObject.GetComponent<PlayerManager>().health = collision.gameObject.GetComponent<PlayerManager>().health - dammages;
            }

            if (trap == trapType.immobilisation)
            //the player is immobilized for 2sec
            {
                StartCoroutine(timer(immoTime));
                StopCoroutine(timer(immoTime));
                collision.rigidbody.velocity = new Vector3(0, 0, 0);
            }

            if (trap == trapType.move)
            //will send the player two units back on axis x
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 position = contact.point;
                Vector3 newPos = position + new Vector3(-2, 0, 0);
                collision.rigidbody.transform.position = newPos;
            }
        }

        if (tag == "Monster")
        {
            if (trap == trapType.dammage)
            {
                collision.gameObject.GetComponent<MonsterManager>().health = collision.gameObject.GetComponent<MonsterManager>().health - dammages;
            }

            if (trap == trapType.immobilisation)
            //the player is immobilized for 2sec
            {
                StartCoroutine(timer(immoTime));
                StopCoroutine(timer(immoTime));
                collision.rigidbody.velocity = new Vector3(0, 0, 0);
            }

            if (trap == trapType.move)
            //will send the player two units back on axis x
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 position = contact.point;
                Vector3 newPos = position + new Vector3(-2, 0, 0);
                collision.rigidbody.transform.position = newPos;
            }
        }
    }

    IEnumerator timer(int secs)
    {
        yield return new WaitForSeconds(secs);
    }
}
