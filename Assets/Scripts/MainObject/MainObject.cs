using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObject : MonoBehaviour
{
     GameObject mainObject;
    public void SetMainObject(GameObject main)
    {
        mainObject = main;
    }
    public GameObject GetMainObject()
    {
        return mainObject;
    }
}
