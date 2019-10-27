using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This rotates the object that is being interacted with a touch
/// </summary>
public class RotateObject : MonoBehaviour
{
    public float rotationSpeed;
    public CameraControls cameraController;
    private Vector3 lastMousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            var mouseDelta = Input.mousePosition - lastMousePosition;
            transform.Translate(-mouseDelta.x * cameraController.panSpeed, mouseDelta.y * cameraController.panSpeed, 0);
            lastMousePosition = Input.mousePosition;
        }
    }

    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        //float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, -rotX);
        //transform.RotateAround(Vector3.right, rotY);
    }
}