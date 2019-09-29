using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Transform player;

    private float rotX;
    private float rotY;

    public float zoomSpeed;
    public float fovMin;
    public float fovMax;

    private Camera myCamera;

    private void Start()
    {
        Cursor.visible = false;
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Cursor.visible)
            Cursor.visible = false;

        // get rotation based on mouse position
        rotX += Input.GetAxis("Mouse X") * 4;
        rotY += Input.GetAxis("Mouse Y") * -4;

        // constrain look axis on y
        if (rotY > 90)
            rotY = 90;
        if (rotY < -90)
            rotY = -90;

        // Move
        if (!Cursor.visible)
            transform.eulerAngles = new Vector3(rotY, rotX, 0);

        // Zoom
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            myCamera.fieldOfView += zoomSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            myCamera.fieldOfView -= zoomSpeed;
        }
        myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView, fovMin, fovMax);

    }
}
