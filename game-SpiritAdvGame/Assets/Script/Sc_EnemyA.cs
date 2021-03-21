﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Sc_EnemyA : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb2d;
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
        rb2d = GetComponent<Rigidbody2D>();
        InitValues();
    }
    void InitValues()
    {
        moveSpeed = 3f;
        fleeingMoveSpeed = 0.3f;
        aggroRange = 10f;
        originalPos = rb2d.position;
    }
    void Update()
    {
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
            else
            {
                rb2d.position = originalPos;
                rb2d.rotation = 0f; 
            }
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
            MoveAndLookTowardsPlayer();
        }
    }
    void MoveAndLookTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, target.position) > 0.25f)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb2d.rotation = angle;
            direction.Normalize();
            movement = direction;
            rb2d.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }

    void FleeFromPlayer()
    {
        Vector3 dir = transform.position - target.position;
        dir.Normalize();
        movement = dir;
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
}
