using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LegPuzzleManager : MonoBehaviour
{
    public static int eventCounter = 0;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject magicWallRed;
    public GameObject magicWallGreen;
    public GameObject magicWallPurple;

    void Update()
    {
        if (eventCounter == 1)
        {
            Destroy(wall1);
            Destroy(magicWallRed);
        }
        else if (eventCounter == 2)
        {
            Destroy(wall2);
            Destroy(magicWallGreen);
        }
        else if (eventCounter == 3)
        {
            Destroy(magicWallPurple);
        }
    }
}
