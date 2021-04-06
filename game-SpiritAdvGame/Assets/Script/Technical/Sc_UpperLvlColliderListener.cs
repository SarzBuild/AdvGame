using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_UpperLvlColliderListener : MonoBehaviour
{
    public static bool hasJumpedDown;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            hasJumpedDown = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            hasJumpedDown = true;
        }
        
    }
}
