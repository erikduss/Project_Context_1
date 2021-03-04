using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //TODO: Check if the Camera sens and movement needs to be adjusted
    public float lookSense;
    public Transform target;
    public float dstFromTarget;
    private Vector2 pitchMinMax = new Vector2(20, 85);

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    void Start()
    {
    }

    void LateUpdate()
    {

        yaw -= Input.GetAxis("Horizontal") * lookSense;
        pitch += Input.GetAxis("Vertical") * lookSense;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;
    }
}
