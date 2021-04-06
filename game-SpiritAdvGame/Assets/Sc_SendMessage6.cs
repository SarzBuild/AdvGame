using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SendMessage6 : MonoBehaviour
{
    public static bool Button6;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button6 = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button6 = false;
        }
    }
}
