using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAngle : MonoBehaviour
{
    private Camera cam;
    private float mouseAngle;
    public float getMouseAngle() { return this.mouseAngle; }

    private void Start()
    {
        cam = Camera.main;
        mouseAngle = 0;
    }

    private void Update()
    {
        mouseAngle = CalculateMouseAngle();
    }

    private float CalculateMouseAngle()
    {
        Vector3 startingPos = cam.WorldToScreenPoint(gameObject.transform.position);
        Vector3 offset = Input.mousePosition - startingPos;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;
        return angle;
    }
}
