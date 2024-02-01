using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseSelector : MonoBehaviour
{
    public float speed = 1f; // speed of camera movement
    public Vector3 offset; // offset to ensure camera doesn't clip with the house
    private Vector3 offsetCam;

    public float interval = 0.02f; // Interval between clicks.0.5
    private float clickTimer = 0f; // Time since last click.
    private int clickCount = 0; // Number of clicks in current click sequence.

    private Transform selectedHouse = null; // the house that is currently selected
    public Canvas menuCanvas;
    public ScrollAndZoom script;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private int x = 0;
    public string hs_name;
    private Transform firstClickedObject;
    private bool canvasActive;
    private int a=1;
    private Vector3 posDirection;

    private void Start()
    {
   
        canvasActive = menuCanvas.gameObject.activeSelf;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked.
        {
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == ("House"))
            {
                if (clickCount == 0)
                {
                    firstClickedObject = hit.transform;
                    clickTimer = 0f;
                }

                else if (clickCount == 1)
                {
                    Debug.Log("clicktimer  : "+clickTimer+"  interval  : "+interval);
                    if (hit.transform == firstClickedObject && clickTimer <  interval) // check if the same object was clicked
                    {
                        if (firstClickedObject.CompareTag("House") && !menuCanvas.gameObject.activeSelf) // only select if the clicked object has the "House" tag
                        {   
                                originalPosition = transform.position;
                                originalRotation = transform.rotation;

                            // selectedHouse = hit.transform;
                            selectedHouse = firstClickedObject;

                            if (selectedHouse.name == "Restaurant")
                            {
                                //offset = new Vector3(33, 15, 11);
                             //   Quaternion specificRotation = Quaternion.Euler(45f, 90f, 0f);
                                posDirection = new Vector3(184, 16, -8);
                            }
                            else if (selectedHouse.name == "Coffe")
                            {
                                //offset = new Vector3(-69, 26, -48);
                                posDirection = new Vector3(152, 13, 68);
                            }
                            else
                            {
                                //offset = new Vector3(-1, 18, -10);
                                posDirection = new Vector3(191,19,-71);
                            }
                        }
                    }
                    firstClickedObject = null; // reset the first clicked object
                    clickCount = 0; // reset click count
                  //  Debug.Log("update");
                }
                firstClickedObject = hit.transform;
                clickCount++;
                //  clickTimer = 0f; // Reset timer.
            }
            else
            {
                clickCount = 0;
            }
        }
       
        if (selectedHouse != null && clickTimer < interval)
        {
            // move the camera towards the house
            Vector3 targetPosition = selectedHouse.position + offset;
            transform.position = Vector3.Lerp(transform.position, posDirection, Time.deltaTime * speed);

            // make the camera look at the house
            transform.LookAt(selectedHouse);
            if (x == 0)
            {
                x = 1;
                Invoke("housenull", 3f);
            }
        }
        // Check if the Canvas was activated or deactivated

            // Update the canvasActive flag
            canvasActive = menuCanvas.gameObject.activeSelf;
    }


    public void housenull()
    {
        script.enabled = false;
        menuCanvas.gameObject.SetActive(true);
        selectedHouse = null;
    }
    public void actvscript()
    {
        script.enabled = true;
        menuCanvas.gameObject.SetActive(false);
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        x = 0;
    }
     
}

