using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Sc_StaticIsometricSpriteRenderer : MonoBehaviour
{
    void Awake()
    {
        UpdateRenderOrder();
    }

    protected void UpdateRenderOrder()
    {
        GetComponent<Renderer>().sortingOrder = (int) (transform.position.y * -10);
    }
}
