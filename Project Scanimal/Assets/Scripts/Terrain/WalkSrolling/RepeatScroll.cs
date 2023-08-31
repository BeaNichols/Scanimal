using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatScroll : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    private void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    private void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        { 
            transform.position = startPos;
        }
    }
}
