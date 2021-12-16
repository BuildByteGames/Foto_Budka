using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField]
    float objectRotationSpeed = 5f;
    [SerializeField]
    GameObject mainObject; 
    private void OnMouseDrag()
    {
        float Xaxis = Input.GetAxis("Mouse X") * objectRotationSpeed;
        float Yaxis = Input.GetAxis("Mouse Y") * objectRotationSpeed;

        mainObject.transform.Rotate(Vector3.down, Xaxis,Space.World);
        mainObject.transform.Rotate(Vector3.right, Yaxis, Space.World);
    }

}
