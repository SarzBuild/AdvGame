using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DoorOpenerManager : MonoBehaviour
{
    public static bool Button6;
    public GameObject wall;
    public GameObject wall2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (Sc_PlayerState.hasLegs)
        {
            if (other.tag == "Player")
            {
                Destroy(wall);
                Destroy(wall2);
            }
        }
    }
}
