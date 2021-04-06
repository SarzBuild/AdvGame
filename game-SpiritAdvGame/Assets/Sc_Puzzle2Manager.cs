using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Puzzle2Manager : MonoBehaviour
{
    void Update()
    {
        if (Sc_LegPuzzleManager.eventCounter == 1)
        {
            if (Sc_SendMessage3.Button3)
            {
                Sc_LegPuzzleManager.eventCounter++;
            }
        }
    }
}
