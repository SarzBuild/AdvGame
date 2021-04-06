using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_IsometricSpriteRenderer : Sc_StaticIsometricSpriteRenderer
{
    void Update()
    {
        UpdateRenderOrder();
    }
}
