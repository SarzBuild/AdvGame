﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Sc_EnemyB : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb2d;
    private Vector2 movement;
    public float moveSpeed;
    public float fleeingMoveSpeed;
    public float aggroRange;
    public float aggroRangeAttacks;
    private Vector2 originalPos;

    private bool playerIsInAggroRange;
    private bool spiritColliding;
    private bool isSpirit;
    private bool hasLegs;
    private bool isFullBody;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        InitValues();
    }
    void InitValues()
    {
        moveSpeed = 3f;
        fleeingMoveSpeed = 2f;
        aggroRange = 10f;
        aggroRangeAttacks = 5f;
        originalPos = rb2d.position;
    }
    void Update()
    {
        rb2d.velocity = Vector2.zero;
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
                FleeFromPlayer();
            }
        }
        else if (!playerIsInAggroRange)
        {
            rb2d.position = originalPos; 
            rb2d.rotation = 0f;
        }
    }
    void EnemyBehaviourWhenCharacterHasBody()
    {
        if (playerIsInAggroRange)
        {
            FleeFromPlayer();
        }
    }
    void EnemyBehaviourWhenCharacterHasLegs()
    {
        if (playerIsInAggroRange)
        {
            if (aggroRangeAttacks > 5)
            {  
                RangedAttacks();
            }
            else
            {
                FleeFromPlayer();
            }
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

    void RangedAttacks()
    {
        // Attack Mechanics
    }
}