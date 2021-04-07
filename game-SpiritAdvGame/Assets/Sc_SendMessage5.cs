using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SendMessage5 : MonoBehaviour
{
    public static bool Button5;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button5 = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button5 = false;
        }
    }
}
