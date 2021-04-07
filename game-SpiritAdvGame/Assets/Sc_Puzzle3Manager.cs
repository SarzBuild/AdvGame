using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Puzzle3Manager : MonoBehaviour
{
    void Update()
    {
        if (Sc_LegPuzzleManager.eventCounter == 2)
        {
            if (Sc_SendMessage4.Button4 && Sc_SendMessage5.Button5 && Sc_SendMessage6.Button6)
            {
                Sc_LegPuzzleManager.eventCounter++;
            }
        }
    }
}
