using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_IsometricSpriteRenderer : MonoBehaviour
{
    public GameObject[] tileMaps;
    private GameObject tileMapToUpdate;

    void Start()
    {
        tileMaps = GameObject.FindGameObjectsWithTag("UsedByRenderingTileMap");
    }
    void Update()
    {
        GetComponent<Renderer>().sortingOrder = (int) (transform.position.y * -10);
        foreach (GameObject tileMap in tileMaps)
        {
            tileMap.GetComponentInChildren<Sc_StaticIsometricSpriteRenderer>().UpdateRenderOrder();
        }
    }

    /*public GameObject individualTilemap(GameObject tileMap)
    {
        tileMapToUpdate = tileMap;
        return tileMapToUpdate;
    }*/
}
