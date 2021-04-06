using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerControler : MonoBehaviour
{
    Sc_SelectChildObject playerState;
    public static float speed = 3.5f;
    Animator ghostAnimator;
    SpriteRenderer spriteGhost;
    SpriteRenderer spriteLegs;
    SpriteRenderer spriteFull;

    SpriteRenderer sr;
    public float dashDistance = 5.0f;
    private float slideSpeed;
    public float moveX = 0f;
    public float moveY = 0f;
    private State state;
    private enum State
    {
        Normal,
        Sliding
    }

    public bool isAttacking = false;
    bool isIdle;
    bool isWalking;
    public bool isSliding;
    public bool gotHit;
    public Vector3 lastMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;        
        playerState = GetComponent<Sc_SelectChildObject>();        
        ghostAnimator = GetComponent<Animator>();
        spriteGhost = GetComponent<SpriteRenderer>();
        spriteLegs = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteFull = transform.GetChild(1).GetComponent<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleMovement();
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
        moveX = 0f;
        moveY = 0f;

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
            spriteGhost.flipX = false;
            spriteLegs.flipX = false;
            spriteFull.flipX = false;
            moveX = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            spriteGhost.flipX = true;
            spriteLegs.flipX = true;
            spriteFull.flipX = true;
            moveX = -1f;
        }
        isIdle = moveX == 0 && moveY == 0;
        if (moveY < 0)
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
            ghostAnimator.SetBool("isWalking", false);
            ghostAnimator.SetFloat("X", lastMoveDirection.x);
            ghostAnimator.SetFloat("Y", lastMoveDirection.y);        
        }
        else
        {
            Vector3 moveDir = new Vector3(moveX, moveY).normalized;
            isWalking = true;

            if (TryMove(moveDir, speed * Time.deltaTime))
            {
                //Walk Animation
                ghostAnimator.SetBool("isWalking", true);
                ghostAnimator.SetFloat("X", moveX);
                ghostAnimator.SetFloat("Y", moveY);                
            }
            else
            {
                //idle animation
                ghostAnimator.SetBool("isWalking", false);
                ghostAnimator.SetFloat("X", lastMoveDirection.x);
                ghostAnimator.SetFloat("Y", lastMoveDirection.y);
            }

        }
    }
    public bool TryMove(Vector3 baseMoveDir, float dist)
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

    private void HandleSlide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && playerState.hasLegs)
        {
            state = State.Sliding;
            slideSpeed = 20f;
        }
    }
    private void HandleSliding()
    {
        TryMove(lastMoveDirection, slideSpeed * Time.deltaTime);
        isSliding = true;
        slideSpeed -= slideSpeed * 10f * Time.deltaTime;
        if (slideSpeed < 5f)
        {
            state = State.Normal;
            isSliding = false;
        }

    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && playerState.hasArms)
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
        transform.Find("Hitbox").gameObject.transform.position += lastMoveDirection * 1.5f;
        yield return new WaitForSeconds(0.1f);
        transform.Find("Hitbox").gameObject.transform.position = transform.position;
        transform.Find("Hitbox").gameObject.SetActive(false);
        isAttacking = false;
    }
    public void GotHit()
    {
        Debug.Log("Ouch2");
        gotHit = true;
        //StartCoroutine(Hit());
    }
    IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.1f);
        gotHit = false;
    }
}