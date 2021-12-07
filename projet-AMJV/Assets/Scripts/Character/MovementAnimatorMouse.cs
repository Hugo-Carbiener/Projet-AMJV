using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MovementAnimatorMouse : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private RuntimeAnimatorController KnightController;
    [SerializeField]
    private RuntimeAnimatorController MageController;
    [SerializeField]
    private RuntimeAnimatorController NinjaController;

    private MouseAngle mouseAngle;

    private void Awake()
    {
        // Set class animator
        Classes playerClass = GetComponentInParent<PlayerManager>().getClass();
        switch (playerClass)
        {
            case Classes.Knight:
                animator.runtimeAnimatorController = KnightController;
                break;
            case Classes.Mage:
                animator.runtimeAnimatorController = MageController;
                break;
            case Classes.Ninja:
                animator.runtimeAnimatorController = NinjaController;
                break;
        }

        mouseAngle = GetComponentInParent<MouseAngle>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
    }

    public void SetOrientation()
    {
        int Xpos = 0;
        int Ypos = 0;

        float angle = mouseAngle.getMouseAngle();
        
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
