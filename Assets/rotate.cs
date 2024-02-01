using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    private float rotateSpeed = 30f; // Speed of rotation. You can adjust this in the inspector.

    // Update is called once per frame
    void Update()
    {
        // Rotate the object along Y-axis at rotateSpeed degrees per second.
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}
