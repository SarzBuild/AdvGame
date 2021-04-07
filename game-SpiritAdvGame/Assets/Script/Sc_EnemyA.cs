using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Sc_EnemyA : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb2d;
    private Animator myAnim;
    private Sc_PlayerControler playerControler;
    private Transform childTransform;
    private Vector2 movement;
    public float moveSpeed;
    public float fleeingMoveSpeed;
    public float aggroRange;
    private Vector2 originalPos;

    private bool playerIsInAggroRange;
    private bool spiritColliding;
    private bool isSpirit;
    private bool hasLegs;
    private bool isFullBody;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        childTransform = GetComponentInChildren<Transform>();
        myAnim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        InitValues();
    }
    void InitValues()
    {
        moveSpeed = 3f;
        fleeingMoveSpeed = 2f;
        aggroRange = 20f;
        originalPos = rb2d.position;
    }
    void Update()
    {
        transform.rotation = Quaternion.identity;
        rb2d.velocity = Vector2.zero;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        Quaternion childTransformVector = childTransform.rotation;
        childTransformVector.z = -transform.rotation.z;
        CharacterState();
        SpiritCollision();
        AggroRangeDistance();
        if (hasLegs)
        {
            EnemyBehaviourWhenCharacterHasLegs();
        }
        else if (isFullBody)
        {
            EnemyBehaviourWhenCharacterHasBody();
        }
        else
        {
            EnemyBehaviourWhenCharacterSpirit();
        }
    }
    void CharacterState()
    {
        isSpirit = Sc_PlayerState.isSpirit;
        hasLegs = Sc_PlayerState.hasLegs;
        isFullBody = Sc_PlayerState.isFullBody;
    }
    void EnemyBehaviourWhenCharacterSpirit()
    {
        if (spiritColliding)
        {
            if (playerIsInAggroRange)
            {
                MoveAndLookTowardsPlayer();
            }
        }
        else if (!playerIsInAggroRange)
        {
            rb2d.position = originalPos; 
            rb2d.rotation = 0f;
        }
        else
        {
            myAnim.SetBool("isWalking", false);
            myAnim.SetFloat("X", movement.x);
            myAnim.SetFloat("Y", movement.y);
        }
    }
    void EnemyBehaviourWhenCharacterHasBody()
    {
        if (playerIsInAggroRange)
        {
            MoveAndLookTowardsPlayer();
        }
    }
    void EnemyBehaviourWhenCharacterHasLegs()
    {
        if (playerIsInAggroRange)
        {
            MoveAndLookTowardsPlayer();
        }
    }
    void MoveAndLookTowardsPlayer()
    {
        Vector3 direction = target.position - transform.position;
        if (Vector2.Distance(transform.position, target.position) > 0.25f)
        {
            //Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb2d.rotation = angle;
            direction.Normalize();
            movement = direction;
            rb2d.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
            
            bool isIdle = direction.x == 0 && direction.y == 0;
            myAnim.SetBool("isWalking", true);
            myAnim.SetFloat("X", direction.x);
            myAnim.SetFloat("Y", direction.y);
            
        }
    }

    void FleeFromPlayer()
    {
        Vector2 dir = transform.position - target.position;
        dir = dir.normalized;
        transform.Translate(dir * fleeingMoveSpeed * Time.deltaTime);
        
    }

    void AggroRangeDistance()
    {
        float distFromPlayer = Vector2.Distance(transform.position, target.position);
        if (distFromPlayer < aggroRange)
        {
            playerIsInAggroRange = true;
        }
        else
        {
            playerIsInAggroRange = false;
        }
    }

    void SpiritCollision()
    {
        spiritColliding = Sc_WallCollision.spiritColliding;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && hasLegs || collision.collider.tag == "Player" && isFullBody)
        {
            //attack animator
            myAnim.SetBool("isAttacking", true);
            myAnim.SetFloat("X", movement.x);
            myAnim.SetFloat("Y", movement.y);
            playerControler = FindObjectOfType<Sc_PlayerControler>();
            playerControler.GotHit();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && hasLegs || collision.collider.tag == "Player" && isFullBody)
        {
            //attack animator
            myAnim.SetBool("isAttacking", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox")
        {
            myAnim.SetBool("isDead", true);            
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
