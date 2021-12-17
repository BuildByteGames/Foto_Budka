using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [Header("Rotation Values")]
    [SerializeField]
    float objectRotationSpeed = 5f;
    GameObject mainObject; 
    
    void Awake()
    {
        GetNewMainObject();
    }

    private void OnMouseDrag()
    {
        float Xaxis = Input.GetAxis("Mouse X") * objectRotationSpeed;
        float Yaxis = Input.GetAxis("Mouse Y") * objectRotationSpeed;

        if (mainObject != null)
        {
            mainObject.transform.Rotate(Vector3.down, Xaxis, Space.World);
            mainObject.transform.Rotate(Vector3.right, Yaxis, Space.World);
        }
        else
        {
            GetNewMainObject();
        }
    }
    public void GetNewMainObject()
    {
        mainObject = FindObjectOfType<MainObject>().GetMainObject();
    }

}
