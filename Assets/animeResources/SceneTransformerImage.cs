using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransformerImage : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
 
    }
}
