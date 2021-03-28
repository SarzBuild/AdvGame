using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_test : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    string directionFacing = "down";
    float moveLimiter = 0.7f;

    public float runSpeed = 4.0f;

    bool isAttacking = false;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        ManageDirectionFaced();
        Attack();
    }
    void ManageDirectionFaced()
    {
        if (horizontal < 0 )
        {
            directionFacing = "Left";
        }
        else if (horizontal > 0)
        {
            directionFacing = "Right";
        }
        else if (vertical > 0)
        {
            directionFacing = "Up";
        }
        else if (vertical < 0)
        {
            directionFacing = "Down";
        }
    }
    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) 
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
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
        transform.Find("Hitbox" + directionFacing).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.Find("Hitbox" + directionFacing).gameObject.SetActive(false);
        isAttacking = false;
    }
}