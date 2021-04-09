using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Sc_EnemyB : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb2d;
    public GameObject laserPrefab;
    public Transform firePoint;
    private Vector2 movement;
    private Animator myAnim;
    private Transform childTransform;
    //STATE VARIABLES
    private bool spiritColliding;
    private bool isSpirit;
    private bool hasLegs;
    private bool isFullBody;
    //RANGE VARIABLES
    public float moveSpeed;
    public float fleeingMoveSpeed;
    public float aggroRange;
    public float minAggroRangeAttacks;
    public float maxAggroRangeAttacks;
    private Vector2 originalPos;
    private float distFromPlayer;
    private bool playerIsInAggroRange;
    //ATTACK VARIABLES
    public float laserForce = 1000f;
    private float lastAttackTime;
    public float attackDelay = 2;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        childTransform = GetComponentInChildren<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        InitValues();
    }
    void InitValues()
    {
        moveSpeed = 3f;
        fleeingMoveSpeed = 2f;
        aggroRange = 8f;
        minAggroRangeAttacks = 5f;
        maxAggroRangeAttacks = 7f;
        originalPos = rb2d.position;
    }
    void Update()
    {
        rb2d.velocity = Vector2.zero;
        /*transform.rotation = Quaternion.identity;
        rb2d.velocity = Vector2.zero;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        Quaternion childTransformVector = childTransform.rotation;
        childTransformVector.z = -transform.rotation.z;*/
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
            FleeFromPlayer();
        }
    }
    void EnemyBehaviourWhenCharacterHasLegs()
    {
        if (playerIsInAggroRange)
        {
            if (distFromPlayer >= minAggroRangeAttacks && distFromPlayer <= maxAggroRangeAttacks)
            {  
                RangedAttacks();
            }
            else if (distFromPlayer <= minAggroRangeAttacks)
            {
                FleeFromPlayer();
            }
            else
            {
                myAnim.SetBool("isWalking", false);
                myAnim.SetFloat("X", movement.x);
                myAnim.SetFloat("Y", movement.y);
            }
        }
    }

    void FleeFromPlayer()
    {
        Vector2 dir = (transform.position - target.position);
        dir = dir.normalized;
        transform.Translate(dir * fleeingMoveSpeed * Time.deltaTime);
        myAnim.SetBool("isWalking", true);
        myAnim.SetFloat("X", dir.x);
        myAnim.SetFloat("Y", dir.y);
    }

    void AggroRangeDistance()
    {
        distFromPlayer = Vector2.Distance(transform.position, target.position);
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

        Vector2 targetDir = target.transform.position - firePoint.transform.position;
        targetDir.Normalize();
        //float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90 * Time.deltaTime);

        if (Time.time > lastAttackTime + attackDelay)
        {            
            GameObject laser = Instantiate(laserPrefab, firePoint.position, transform.rotation);
            //laser.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(100f, laserForce));
            laser.GetComponent<Rigidbody2D>().velocity = targetDir * laserForce;
            lastAttackTime = Time.time;
            myAnim.SetBool("isAttacking", true);
            myAnim.SetFloat("X", movement.x);
            myAnim.SetFloat("Y", movement.y);
        }
        else
        {
            myAnim.SetBool("isAttacking", false);
        }
        /*if (Time.time > lastAttackTime + attackDelay)
        {
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, maxAggroRangeAttacks);
            Vector2 dir = -(transform.position - target.position);
            GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
            rb.AddForce(dir * laserForce, ForceMode2D.Impulse);
            //rb.AddRelativeForce(new Vector2(0f, laserForce));
            lastAttackTime = Time.time;
            /*
            
            Vector2 dir = -(transform.position - target.position);
            rb.AddForce(dir * laserForce, ForceMode2D.Impulse);*/
            /*if (hit.transform == target)
            {
                Debug.Log("attack!");
            }
            else
            {
                Debug.Log("NO attack!");
            }
        }     */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox")
        {
            myAnim.SetBool("isDead", true);
        }
    }    public void Death()
    {
        Destroy(gameObject);
    }
}
