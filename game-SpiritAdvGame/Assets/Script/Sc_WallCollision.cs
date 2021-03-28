using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Sc_WallCollision : MonoBehaviour
{
    public static bool spiritColliding;
    
    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("oi");
            spiritColliding = true;
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        Debug.Log("xau");
        spiritColliding = false;
    }
}
