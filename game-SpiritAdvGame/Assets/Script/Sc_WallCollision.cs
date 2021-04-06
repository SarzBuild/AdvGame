using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Sc_WallCollision : MonoBehaviour
{
    public static bool spiritColliding;

    void Start()
    {
        
    }
    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            spiritColliding = true;
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        spiritColliding = false;
    }
}
