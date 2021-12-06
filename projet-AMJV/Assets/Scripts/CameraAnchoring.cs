using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchoring : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0, 4, -4);

    private void Update()
    {
        transform.position = GameObject.FindGameObjectsWithTag("Player")[0].transform.position + offset;
    }
}
