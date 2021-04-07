using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerUpperLevelCollidingManager : MonoBehaviour
{
    private GameObject player;
    private GameObject upperLvl;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        upperLvl = GameObject.Find("UpperLvl");
    }
    void Update()
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), upperLvl.GetComponent<Collider2D>(), true);
    }
}
