using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Sc_EnemyC : MonoBehaviour
{
    public Transform target;
    public List<GameObject> waypointList;
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
    [SerializeField] private GameObject pfFieldOfView;
    private Sc_FieldOfView _fieldOfView;
    private float fov = 90f;
    private float viewDistance = 5f;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        _fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<Sc_FieldOfView>();
        //transform.DetachChildren();
        //pfFieldOfView.transform.position = new Vector3(pfFieldOfView.transform.position.x, pfFieldOfView.transform.position.y, 0);
        rb2d = GetComponent<Rigidbody2D>();
        
        InitValues();
    }

    void InitValues()
    {
        waypointIndex = 0;
        moveSpeed = 3f;
        originalPos = rb2d.position;
        _fieldOfView.SetFoV(fov);
        _fieldOfView.SetViewDistance(viewDistance);
        _fieldOfView.gameObject.SetActive(true);
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
        if (_fieldOfView.gameObject.activeInHierarchy)
        {
            _fieldOfView.gameObject.SetActive(false);
        }
        else
        {
            
        }
    }

    void EnemyBehaviourWhenCharacterHasLegs()
    {
        if (!_fieldOfView.gameObject.activeInHierarchy)
        {
            _fieldOfView.gameObject.SetActive(true);
        }
        else
        {
            _fieldOfView.SetOrigin(gameObject.transform.position);
            _fieldOfView.SetAimDirection(GetAimDir());
            Patrol();
        }
    }

    void AggroRangeDistance()
    {
        if (Vector3.Distance(gameObject.transform.position, target.position) < viewDistance)
        {
            Vector3 directionToPlayer = (target.position - gameObject.transform.position).normalized;
            if (Vector3.Angle(GetAimDir(), directionToPlayer) < fov / 2f)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(gameObject.transform.position, directionToPlayer, viewDistance);
                if (raycastHit2D.collider != null)
                {
                    Attacks();
                }
            }
        }
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
            Vector3 waypoint = waypointList[waypointIndex].transform.position;
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

    void FoundPlayer()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox")
        {
            Destroy(gameObject);
        }
    }
}
