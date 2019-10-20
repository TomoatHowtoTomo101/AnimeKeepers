using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Camera cam;
    public float dragSpeed;

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
            transform.position = new Vector3(transform.position.x,
                                            transform.position.y,
                                            transform.position.z + 0.2f);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            transform.position = new Vector3(transform.position.x,
                                transform.position.y,
                                transform.position.z - 0.1f);
        }
    }
}
