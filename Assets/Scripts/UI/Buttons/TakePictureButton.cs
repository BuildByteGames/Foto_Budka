using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePictureButton : MonoBehaviour
{

    PhotoHandler photoHandler;

    private void Awake()
    {
        photoHandler = FindObjectOfType<PhotoHandler>();
    }
    public void TakePicture()
    {
        photoHandler.PictureOnNextFrame(Screen.width, Screen.height);
    }
}
