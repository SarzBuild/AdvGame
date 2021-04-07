using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SendMessage3 : MonoBehaviour
{
    public static bool Button3;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button3 = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button3 = false;
        }
    }
}
