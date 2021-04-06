using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Puzzle_TutorialButton : MonoBehaviour
{
    public static bool openDoor;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            openDoor = true;
        }
    }
}
