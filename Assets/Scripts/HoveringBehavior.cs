using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringBehavior : MonoBehaviour
{

    private float heightChange = 0.05f;
    private float speed = 1.5f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * heightChange + startPos.y;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
