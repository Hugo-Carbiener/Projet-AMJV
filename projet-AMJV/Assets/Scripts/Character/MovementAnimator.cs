using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MovementAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
    }

    public void SetOrientation()
    {
        int Xpos = 0;
        int Ypos = 0;

        //Convert the player to Screen coordinates
        Vector3 startingPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 offset = Input.mousePosition - startingPos;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;
        Debug.Log(angle);
        
        if ((angle >= 337.5 && angle <= 360) || (angle < 22.5 && angle >= 0))
        {
            Xpos = 1;
            Ypos = 0;
        }
        else if (angle >= 22.5 && angle < 67.5)
        {
            Xpos = 1;
            Ypos = 1;
        }
        else if (angle >= 67.5 && angle < 112.5)
        {
            Xpos = 0;
            Ypos = 1;
        }
        else if (angle >= 112.5 && angle < 157.5)
        {
            Xpos = -1;
            Ypos = 1;
        }
        else if (angle >= 157.5 && angle < 202.5)
        {
            Xpos = -1;
            Ypos = 0;
        }
        else if (angle >= 202.5 && angle < 247.5)
        {
            Xpos = -1;
            Ypos = -1;
        }
        else if (angle >= 247.5 && angle < 292.5)
        {
            Xpos = 0;
            Ypos = -1;
        }
        else if (angle >= 292.5 && angle < 337.5)
        {
            Xpos = 1;
            Ypos = -1;
        }

        //Debug.Log("X: " + Xpos + " Y: " + Ypos);
        animator.SetFloat("MoveX", Xpos);
        animator.SetFloat("MoveY", Ypos);
    }

    private void FixedUpdate()
    {
        SetOrientation();
    }
}
