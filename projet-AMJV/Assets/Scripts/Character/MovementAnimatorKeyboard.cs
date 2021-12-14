using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MovementAnimatorKeyboard : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private RuntimeAnimatorController KnightController;
    [SerializeField]
    private RuntimeAnimatorController MageController;
    [SerializeField]
    private RuntimeAnimatorController AssassinController;

    private int Xpos;
    private int Ypos;

    private void Start()
    {
        // Set class animator
        Classes playerClass = GetComponent<PlayerManager>().getClass();
        switch(playerClass)
        {
            case Classes.Knight:
                animator.runtimeAnimatorController = KnightController;
                break;
            case Classes.Mage:
                animator.runtimeAnimatorController = MageController;
                break;
            case Classes.Assassin:
                animator.runtimeAnimatorController = AssassinController;
                break;
        }

        // Face camera on start
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
    }

    public void SetOrientation()
    {   
        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.D))
        {
            Xpos = 1;
            Ypos = 1;
        }
        else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.Q))
        {
            Xpos = -1;
            Ypos = 1;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            Xpos = 1;
            Ypos = -1;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Q))
        {
            Xpos = -1;
            Ypos = -1;
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            Xpos = 0;
            Ypos = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Xpos = 0;
            Ypos = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Xpos = 1;
            Ypos = 0;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            Xpos = -1;
            Ypos = 0;
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
