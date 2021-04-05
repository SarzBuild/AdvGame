using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ConveyorBelt : MonoBehaviour
{
    private Vector2 direction;
    private GameObject target;
    public float speed;
    public bool goUp;
    public bool goLeft;
    public bool goRight;
    public bool goDown;

    void Start()
    {
        speed = 5f;
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (goUp)
        {
            direction = Vector2.up;
        }
        else if (goLeft)
        {
            direction = Vector2.left;
        }
        else if (goRight)
        {
            direction = Vector2.right;
        }
        else if (goDown)
        {
            direction = Vector2.down;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Sc_PlayerControler.speed = 0f;
            other.transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Sc_PlayerControler.speed = 5f;
        }
    }
}