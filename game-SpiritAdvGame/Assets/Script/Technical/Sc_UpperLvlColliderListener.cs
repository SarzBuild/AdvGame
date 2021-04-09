using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_UpperLvlColliderListener : MonoBehaviour
{
    public static bool hasJumpedDown;
    public static bool isStillOnTop;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            hasJumpedDown = false;
            isStillOnTop = true;
        }
    }
}
