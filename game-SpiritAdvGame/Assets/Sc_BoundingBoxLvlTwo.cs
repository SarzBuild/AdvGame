using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BoundingBoxLvlTwo : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        while (!Sc_UpperLvlColliderListener.isStillOnTop)
        {
            if (other.tag == "Enemy")
            {
                
                
            }
        }
    }
}
