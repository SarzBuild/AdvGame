using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SendMessage4 : MonoBehaviour
{
    public static bool Button4;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button4 = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Button4 = false;
        }
    }
}
