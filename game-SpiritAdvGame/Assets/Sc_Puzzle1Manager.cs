using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Puzzle1Manager : MonoBehaviour
{
    void Update()
    {
        if (Sc_LegPuzzleManager.eventCounter == 0)
        {
            if (Sc_SendMessage1.Button1 && Sc_SendMessage2.Button2 == true)
            {
                Sc_LegPuzzleManager.eventCounter++;
            }
        }
    }
}
