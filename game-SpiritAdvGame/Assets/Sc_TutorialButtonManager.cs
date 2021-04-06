using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_TutorialButtonManager : MonoBehaviour
{
    public static bool Button6;
    public GameObject wall;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(wall);
        }
    }
}
