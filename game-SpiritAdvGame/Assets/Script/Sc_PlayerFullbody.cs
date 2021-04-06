using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerFullbody : MonoBehaviour
{
    Animator fullbodyAnimator;
    Sc_PlayerControler Sc_PlayerControler;

    bool isIdle;
    bool isWalking;
    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;
        isWalking = false;
        fullbodyAnimator = GetComponent<Animator>();
        Sc_PlayerControler = FindObjectOfType<Sc_PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageAnimator(Sc_PlayerControler.moveX, Sc_PlayerControler.moveY, Sc_PlayerControler.lastMoveDirection);
    }
    public void ManageAnimator(float x, float y, Vector3 lastMoveDir)
    {
        isIdle = x == 0 && y == 0;
        if (isIdle)
        {
            //Play idle animation
            fullbodyAnimator.SetBool("isWalking", false);
            fullbodyAnimator.SetFloat("X", lastMoveDir.x);
            fullbodyAnimator.SetFloat("Y", lastMoveDir.y);
        }
        else
        {
            Vector3 moveDir = new Vector3(x, y).normalized;
            isWalking = true;

            if (Sc_PlayerControler.TryMove(moveDir, Sc_PlayerControler.speed * Time.deltaTime))
            {
                //Walk Animation
                fullbodyAnimator.SetBool("isWalking", true);
                fullbodyAnimator.SetFloat("X", x);
                fullbodyAnimator.SetFloat("Y", y);
            }
            else
            {
                //idle animation
                fullbodyAnimator.SetBool("isWalking", false);
                fullbodyAnimator.SetFloat("X", lastMoveDir.x);
                fullbodyAnimator.SetFloat("Y", lastMoveDir.y);
            }

        }
        if (Sc_PlayerControler.isSliding)
        {
            fullbodyAnimator.SetBool("isDashing", true);
        }
        else
        {
            fullbodyAnimator.SetBool("isDashing", false);
        }
    }
    public void Test()
    {
        Debug.Log("test");
    }
}
