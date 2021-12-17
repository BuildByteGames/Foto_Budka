using Dummiesman;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class LoadingObjects : MonoBehaviour
{
    public static GameObject[] Load(string foladerName)
    {
        List<GameObject> gameObjectList = new List<GameObject>();
        DirectoryInfo directoryInformation = new DirectoryInfo(foladerName);
        if (!directoryInformation.Exists)
            directoryInformation.Create();
        FileInfo[] fileList = directoryInformation.GetFiles();


        foreach (FileInfo file in fileList)
        {
            if (file.Extension == ".obj")
            {
                GameObject gameobject = new OBJLoader().Load(
                    $"{directoryInformation.FullName}\\{file.Name}",
                    $"{directoryInformation.FullName}\\{file.Name.Replace(file.Extension, ".mtl")}");
                gameObjectList.Add(gameobject);
            }
        }

        return gameObjectList.ToArray();
    }
}
