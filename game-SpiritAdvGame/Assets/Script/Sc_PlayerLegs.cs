using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerLegs : MonoBehaviour
{
    Animator legsAnimator;
    Sc_PlayerControler Sc_PlayerControler;
    SpriteRenderer sr;

    bool isIdle;
    bool isWalking;
    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;
        isWalking = false;
        legsAnimator = GetComponent<Animator>();
        Sc_PlayerControler = FindObjectOfType<Sc_PlayerControler>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageAnimator(Sc_PlayerControler.moveX, Sc_PlayerControler.moveY, Sc_PlayerControler.lastMoveDirection);
    }
    public void ManageAnimator(float x, float y, Vector3 lastMoveDir)
    {
        isIdle = x == 0 && y == 0;
        if (x < 0 || lastMoveDir.x < 0)
        {

            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        if (isIdle)
        {
            //Play idle animation
            legsAnimator.SetBool("isWalking", false);
            legsAnimator.SetFloat("X", lastMoveDir.x);
            legsAnimator.SetFloat("Y", lastMoveDir.y);
        }
        else
        {
            Vector3 moveDir = new Vector3(x, y).normalized;
            isWalking = true;

            if (Sc_PlayerControler.TryMove(moveDir, Sc_PlayerControler.speed * Time.deltaTime))
            {
                //Walk Animation
                legsAnimator.SetBool("isWalking", true);
                legsAnimator.SetFloat("X", x);
                legsAnimator.SetFloat("Y", y);
            }
            else
            {
                //idle animation
                legsAnimator.SetBool("isWalking", false);
                legsAnimator.SetFloat("X", lastMoveDir.x);
                legsAnimator.SetFloat("Y", lastMoveDir.y);
            }

        }
        if (Sc_PlayerControler.isSliding)
        {
            legsAnimator.SetBool("isDashing", true);
        }
        else
        {
            legsAnimator.SetBool("isDashing", false);
        }
        if (Sc_PlayerControler.gotHit)
        {
            legsAnimator.SetBool("gotHit", true);
        }
        else
        {
            legsAnimator.SetBool("gotHit", false);
        }
        /*if (Sc_PlayerControler.isDead)
        {
            legsAnimator.SetBool("isDying", true);
        }
        else
        {
            legsAnimator.SetBool("isDying", true);
        }*/
    }
    public void Test()
    {
        Debug.Log("test");
    }
}
