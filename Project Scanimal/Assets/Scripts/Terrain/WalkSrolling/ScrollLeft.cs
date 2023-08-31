using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollLeft : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
    }
}
