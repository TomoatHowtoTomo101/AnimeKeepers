using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Camera cam;
    public float panSpeed = 0.005f;
    private bool scrollWheelHeld;
    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            if (transform.position.z < -8.8f)
            {
                transform.position = new Vector3(transform.position.x,
                                transform.position.y,
                                transform.position.z + 0.2f);
            }

            if (panSpeed > 0.001f)
            {
                panSpeed -= 0.0005f;
            }
            else
            {
                panSpeed = 0.001f;
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            if (transform.position.z > -10f)
            {
                transform.position = new Vector3(transform.position.x,
                                    transform.position.y,
                                    transform.position.z - 0.1f);
            }

            if (panSpeed < 0.005f)
            {
                panSpeed += 0.0005f;
            }
            else
            {
                panSpeed = 0.005f;
            }
        }
    }
}
