using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateDepth : MonoBehaviour
{
    Transform trans;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        trans = this.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -10);
    }
}
