using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Sc_EnemyC : MonoBehaviour
{
    public Transform target;
    public List<Vector3> waypointList;
    private Rigidbody2D rb2d;
    private Vector2 movement;
    public float moveSpeed;
    private float waitTimer;
    private Vector3 lastMoveDir;
    
    
    
    private Vector2 originalPos;
    private int waypointIndex;
    private float distance;

    private bool playerIsInAggroRange;
    private bool spiritColliding;
    private bool isSpirit;
    private bool hasLegs;
    private bool isFullBody;
    [SerializeField] private Transform pfFieldOfView;
    private Sc_FieldOfView _fieldOfView;

    void Start()
    {
        _fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<Sc_FieldOfView>();
        rb2d = GetComponent<Rigidbody2D>();
        InitValues();
    }

    void InitValues()
    {
        waypointIndex = 0;
        moveSpeed = 3f;
        originalPos = rb2d.position;
    }

    void Update()
    {
        rb2d.velocity = Vector2.zero;
        CharacterState();
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
        Patrol();
    }

    void EnemyBehaviourWhenCharacterHasBody()
    {

    }

    void EnemyBehaviourWhenCharacterHasLegs()
    {
        _fieldOfView.SetOrigin(transform.position);
        _fieldOfView.SetAimDirection(GetAimDir());
        Patrol();
    }

    void AggroRangeDistance()
    {
        
    }

    Vector3 GetAimDir()
    {
        return lastMoveDir;
    }

    void Patrol()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0f)
        {
            Vector3 waypoint = waypointList[waypointIndex];
            Vector3 waypointDir = (waypoint - transform.position).normalized;
            lastMoveDir = waypointDir;
            
            float distanceBefore = Vector3.Distance(transform.position, waypoint);
            transform.position = transform.position + waypointDir * moveSpeed * Time.deltaTime;
            float distanceAfter = Vector3.Distance(transform.position, waypoint);
            float arriveDistance = .1f;
            if (distanceAfter < arriveDistance || distanceBefore <= distanceAfter) 
            {
                waitTimer = 4f;
                waypointIndex = (waypointIndex + 1) % waypointList.Count;
            }
        }
    }

    void Attacks()
    {
        // Attack Mechanics
    }
}
