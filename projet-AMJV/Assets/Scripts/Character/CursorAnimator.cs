using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimator : MonoBehaviour
{
    [SerializeField]
    private float radius;
    private MouseAngle MouseAngle;
    private Vector3 cursorPos;
    

    private void Start()
    {
        MouseAngle = GetComponentInParent<MouseAngle>();
    }

    private void Update()
    {
        float mouseAngle = MouseAngle.getMouseAngle() * Mathf.Deg2Rad;
        cursorPos = transform.parent.transform.position + new Vector3(radius * Mathf.Cos(mouseAngle), 1, radius * Mathf.Sin(mouseAngle));
        transform.position = cursorPos;
    }
}
