using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Puzzle_TutorialDoor : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Sc_Puzzle_TutorialButton.openDoor)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            //Knockback and Damage
        }
    }
}
