using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SendMessage2 : MonoBehaviour
{
    public static bool Button2;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button2 = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button2 = false;
        }
    }
}
