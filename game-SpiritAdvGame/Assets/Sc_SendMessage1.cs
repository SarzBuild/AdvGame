using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SendMessage1 : MonoBehaviour
{
    public static bool Button1;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button1 = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button1 = false;
        }
    }
}
