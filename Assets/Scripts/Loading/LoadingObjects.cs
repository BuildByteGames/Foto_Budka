using Dummiesman;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class LoadingObjects : MonoBehaviour
{
    public static List<GameObject> UpdateObjests(List<GameObject> oldObjects)
    {
        string folderName;
        if (FindObjectOfType<ActiveFolders>().folderInputName == null)
        {
            folderName = "Input";
        }
        else
        {
            folderName = FindObjectOfType<ActiveFolders>().folderInputName;
        }

        DirectoryInfo directoryInformation = new DirectoryInfo(folderName);
        if (!directoryInformation.Exists)
            directoryInformation.Create();
        FileInfo[] fileList = directoryInformation.GetFiles();

        List<GameObject> newGameObjectList = new List<GameObject>();
        
        foreach (FileInfo file in fileList)
        {
            if (file.Extension == ".obj")
            {
                GameObject gameobject = new OBJLoader().Load(
                    $"{directoryInformation.FullName}\\{file.Name}",
                    $"{directoryInformation.FullName}\\{file.Name.Replace(file.Extension, ".mtl")}");
                    if (! oldObjects.Contains(gameobject))
                    {
                        newGameObjectList.Add(gameobject);
                    }
            }
        }

        newGameObjectList.OrderBy(x => (x.name));

        return newGameObjectList;
    }

}
