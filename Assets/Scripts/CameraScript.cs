using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    float height;

    // Start is called before the first frame update
    void Start()
    {
        height = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = target.position - transform.position;
        Vector3 hDispacement = new Vector3(displacement.x, 0, displacement.z);
        Vector3 facing = transform.rotation.eulerAngles;
        float distance = displacement.magnitude;
        float hAngleDiff = Vector3.Angle(hDispacement, transform.rotation * Vector3.left) - 90;
        float vAngleDiff = Vector3.Angle(displacement, Vector3.up) - 90 - facing.x + (facing.x > 180 ? 360:0);
        float hFactor = Mathf.Abs(hAngleDiff / 200);
        float vFactor = Mathf.Abs(vAngleDiff / 40);

        Debug.Log(vAngleDiff + " vs " + facing.x);

        if (distance > 20) transform.position += hDispacement.normalized * (distance - 20);
        if (distance < 10) transform.position += hDispacement.normalized * (distance - 10);
        transform.position += new Vector3(0, height - transform.position.y,0);
        transform.rotation = Quaternion.Euler(vAngleDiff * vFactor + facing.x, hAngleDiff * hFactor + facing.y, 0);
        //transform.LookAt(target);
    }
}
