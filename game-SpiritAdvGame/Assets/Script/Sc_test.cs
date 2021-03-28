using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_test : MonoBehaviour
{
    public GameObject hitbox;
    public float speed = 5.0f;
    public float dashDistance = 10.0f;
    private float slideSpeed;

    private State state;
    private enum State
    {
        Normal, 
        Sliding
    }

    bool isAttacking = false;
    bool isIdle;
    private Vector3 lastMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleMovement();
                HandleDash();
                HandleSlide();
                Attack();
                break;
            case State.Sliding:
                HandleSliding();
                break;
            default:
                break;
        }
        
    }
    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        isIdle = moveX == 0 && moveY == 0;
        if (isIdle)
        {
            //Play idle animation
        }
        else
        {
            Vector3 moveDir = new Vector3(moveX, moveY).normalized;

            if (TryMove(moveDir, speed * Time.deltaTime))
            {
                //Walk Animation
            }
            else
            {
                //idle animation
            }
                       
        }
    }
    private bool TryMove(Vector3 baseMoveDir, float dist)
    {
        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, dist);
        if (!canMove)
        {
            moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
            canMove = moveDir.x != 0f && CanMove(moveDir, dist);
            if (!canMove)
            {
                moveDir = new Vector3(0f, baseMoveDir.y).normalized;
                canMove = moveDir.y != 0f && CanMove(moveDir, dist);
            }
        }
        if (canMove)
        {
            lastMoveDirection = moveDir;
            transform.position += moveDir * dist;
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CanMove(Vector3 dir, float dist)
    {
        return Physics2D.Raycast(transform.position, dir, dist).collider == null;
    }
   private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TryMove(lastMoveDirection, dashDistance);
            //transform.position += lastMoveDirection * dashDistance;
        }
    }
    private void HandleSlide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            state = State.Sliding;
            slideSpeed = 20f;
        }
    }
    private void HandleSliding()
    {
        //bool tryMove = 
        TryMove(lastMoveDirection, slideSpeed * Time.deltaTime);
        //Debug.Log(tryMove);
        //transform.position += lastMoveDirection * slideSpeed * Time.deltaTime;
        slideSpeed -= slideSpeed * 10f * Time.deltaTime;
        if (slideSpeed < 5f)
        {
            state = State.Normal;
        }
        
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(DoAttack());
        }
    }
    IEnumerator DoAttack()
    {
        //Vector3 attackPos = transform.position + lastMoveDirection * 1f;
        //Instantiate(hitbox, attackPos, transform.rotation);
        transform.Find("Hitbox").gameObject.SetActive(true);
        transform.Find("Hitbox").gameObject.transform.position += lastMoveDirection * 0.8f;
        yield return new WaitForSeconds(0.1f);
        transform.Find("Hitbox").gameObject.transform.position = transform.position;
        transform.Find("Hitbox").gameObject.SetActive(false);
        isAttacking = false;
    }
}
