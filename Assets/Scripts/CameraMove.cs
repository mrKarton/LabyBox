using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public enum ViewType
    {
        panorama,
        action
    }

    public Transform target;
    public float speed;
    public float radius;

    [Header("Panorama Settings")]
    public Vector3 panoramaPos;
    public Vector3 panoramaRotation;

    [Header("Action Settings")]
    public Vector3 actionPos;
    public Vector3 actionRotation;

    private Vector3 overrideVector;
    private Vector3 reqRotation;
    private ViewType viewType;

    private void Start()
    {
        overrideVector = actionPos;
        reqRotation = actionRotation;

        viewType = ViewType.action;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (viewType == ViewType.panorama) viewType = ViewType.action;
            else viewType = ViewType.panorama;

            
        }
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            viewType = ViewType.action;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            viewType = ViewType.panorama;
        }
        if (viewType == ViewType.panorama)
        {
            overrideVector = panoramaPos;
            reqRotation = panoramaRotation;
        }

        else
        {
            overrideVector = actionPos;
            reqRotation = actionRotation;
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + overrideVector;

        Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        transform.position = targetPosition + Vector3.ClampMagnitude(newPos - targetPosition, radius);

        if(transform.rotation.eulerAngles != reqRotation)
        {
            Vector3 newRot = Vector3.Lerp(transform.rotation.eulerAngles, reqRotation, speed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(newRot);
        }
    }
}
