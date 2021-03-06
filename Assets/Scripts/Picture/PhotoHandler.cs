using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

enum PictureFormat {png,jpg}
public class PhotoHandler : MonoBehaviour
{
    int resolutionWidth { get; set;}
    int resolutionHeight { get; set;}

    [SerializeField]
    PictureFormat format;
    [SerializeField]
    string fileName = "Picture";

    bool takePicture;
    Camera pictureCamera;
    void Awake()
    {
        pictureCamera = GetComponent<Camera>();
    }

    public void PictureOnNextFrame(int width, int height)
    {
        resolutionWidth = width;
        resolutionHeight = height;

        takePicture = true;
    }
    private void OnPostRender()
    {
        if(takePicture == true)
        {
            takePicture = false;
            TakePicture(resolutionWidth, resolutionHeight);
        }
    }

    void TakePicture(int width,int height)
    {
        pictureCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        RenderTexture renderedTexture = pictureCamera.targetTexture;

        Texture2D pictureTexture = new Texture2D(renderedTexture.width, renderedTexture.height, TextureFormat.ARGB32, false);

        Rect rect = new Rect(0, 0, renderedTexture.width, renderedTexture.height);
        pictureTexture.ReadPixels(rect, 0, 0);

        byte[] byteArray;
        switch (format)
        {
            case (PictureFormat.png):
                byteArray = pictureTexture.EncodeToPNG();
                break;
            case (PictureFormat.jpg):
                byteArray = pictureTexture.EncodeToJPG();
                break;
            default:
                byteArray = pictureTexture.EncodeToPNG();
                break;
        }

        File.WriteAllBytes(CheckPath(fileName), byteArray);

        RenderTexture.ReleaseTemporary(renderedTexture);
        pictureCamera.targetTexture = null;

    }
    string CheckPath(string fileName)
    {
        //Checks Folder

          DirectoryInfo directoryInformation = new DirectoryInfo("Output");
        if (!directoryInformation.Exists)
            directoryInformation.Create();

        string saveingDirectory =  $"{directoryInformation.FullName}\\{fileName}";
        //Checks file name
        int index = 0;
        if (File.Exists(saveingDirectory + $".{ format}"))
        {
            while ( File.Exists(saveingDirectory + index.ToString() + $".{ format}"))
            {
                index++;
            }
                saveingDirectory += index.ToString();
        }

        string finalPath = saveingDirectory + $".{ format}";
        return finalPath;
    }
}
