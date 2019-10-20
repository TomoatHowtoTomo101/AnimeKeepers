using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This rotates the object that is being interacted with a touch
/// </summary>
public class RotateObject : MonoBehaviour
{
    public float rotationSpeed;

    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        //float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, -rotX);
        //transform.RotateAround(Vector3.right, rotY);
    }
}