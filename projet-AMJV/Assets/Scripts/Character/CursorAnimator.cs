using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimator : MonoBehaviour
{
    [SerializeField]
    private float radius;

    private Vector3 cursorPos;

    private void Update()
    {
        float mouseAngle = GetComponentInParent<MouseAngle>().getMouseAngle() * Mathf.Deg2Rad;
        cursorPos = new Vector3(radius * Mathf.Cos(mouseAngle),transform.position.y, radius * Mathf.Sin(mouseAngle));
        transform.position = cursorPos;
    }
}
