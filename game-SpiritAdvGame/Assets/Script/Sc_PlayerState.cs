using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerState : MonoBehaviour
{
    public static bool isSpirit;
    public static bool hasLegs;
    public static bool isFullBody;
    public int playerState;
    
    void Update()
    {
        if (playerState == 0)
        {
            isSpirit = true;
        }
        else
        {
            isSpirit = false;
        }
        if (playerState == 1)
        {
            hasLegs = true;
        }
        else
        {
            hasLegs = false;
        }
        if (playerState == 2)
        {
            isFullBody = true;
        }
        else
        {
            isFullBody = false;
        }
    }
}
