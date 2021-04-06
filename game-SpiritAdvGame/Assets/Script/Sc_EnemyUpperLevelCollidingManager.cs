using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sc_EnemyUpperLevelCollidingManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject upperLvl;
    public GameObject tileMap;
    void Start()
    {
        
        tileMap = GameObject.Find("Walls");
    }

    
    void Update()
    {
        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), upperLvl.GetComponent<Collider2D>(), !Sc_UpperLvlColliderListener.hasJumpedDown);
        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), tileMap.GetComponent<Collider2D>(), !Sc_UpperLvlColliderListener.hasJumpedDown);
        if (Sc_UpperLvlColliderListener.hasJumpedDown)
        {
            enemy.GetComponent<Renderer>().sortingOrder = 1;
        }
        else
        {
            enemy.GetComponent<Renderer>().sortingOrder = 1802;
        }
    }
}
