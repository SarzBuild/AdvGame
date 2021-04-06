using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class Sc_StaticIsometricSpriteRenderer : MonoBehaviour
{
    public GameObject player;
    void Awake()
    {
        GetComponent<TilemapRenderer>().sortingOrder = (int) (transform.parent.position.y * -10);
        UpdateRenderOrder();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void UpdateRenderOrder()
    {
        if (player.GetComponent<SpriteRenderer>().sortingOrder < gameObject.GetComponent<TilemapRenderer>().sortingOrder)
        {
            gameObject.GetComponent<TilemapRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<TilemapRenderer>().enabled = true;
        }
    }
}
