using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camtoobj : MonoBehaviour
{
    public float speed = 3f; // Speed at which the camera moves towards the object
    private GameObject targetObject = null; // The object that we want the camera to focus on

    void Update()
    {
        // On left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Casts the ray and get the first game object hit
            if (Physics.Raycast(ray, out hit))
            {
                targetObject = hit.transform.gameObject;
            }
        }

        // If we have a target object
        if (targetObject != null)
        {
            // Move the camera towards the target object
            transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, Time.deltaTime * speed);

            // Rotate the camera to look at the target object
            Vector3 relativePos = targetObject.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        }
    }
}
