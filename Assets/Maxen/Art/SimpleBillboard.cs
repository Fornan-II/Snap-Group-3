using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBillboard : MonoBehaviour
{
    private static Transform CameraTransform;

    public bool LockXZRotation = true;

    // Start is called before the first frame update
    void Start()
    {
        if(!CameraTransform)
        {
            CameraTransform = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LockXZRotation)
        {
            Vector3 oldRotation = transform.rotation.eulerAngles;
            transform.forward = -CameraTransform.forward;
            transform.rotation = Quaternion.Euler(oldRotation.x, transform.rotation.eulerAngles.y, oldRotation.z);
        }
        else
        {
            transform.forward = -CameraTransform.forward;
        }
    }
}
