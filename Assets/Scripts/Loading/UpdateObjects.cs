using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateObjects : MonoBehaviour
{
    [SerializeField]
    KeyCode keyCode;
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            FindObjectOfType<ObjectSpawningHandler>().UpdateObjects(); 
        }
    }
}
