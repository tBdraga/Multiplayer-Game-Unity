using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1;
    public float maxDistance = 4;
    public float smooth = 10;
    Vector3 dollyDirection;
    public Vector3 dollyDirectionAdjusted;
    public float distance;

    // Start is called before the first frame update
    void Awake()
    {   
        //the cameras normal position
        dollyDirection = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCameraPosition = transform.parent.TransformPoint(dollyDirection * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out hit)) {
            distance = Mathf.Clamp((hit.distance * 0.85f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDirection * distance, Time.deltaTime * smooth);
    }
}
