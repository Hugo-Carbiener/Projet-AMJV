using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStrikeManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            /*collision */
        }
    }
}