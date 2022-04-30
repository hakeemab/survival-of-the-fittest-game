using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public float smooothSpeed = 0.125f;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPos = Target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smooothSpeed);
        transform.position = smoothPos;

     }
}
