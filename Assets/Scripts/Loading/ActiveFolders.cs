using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFolders : MonoBehaviour
{
    [SerializeField]
    public string folderInputName { get; set; }
    [SerializeField]
    public string folderOutputName { get; set; }
    private void Awake()
    {
        if(folderInputName == null)
        {
            folderInputName = "Input";
        }

        if (folderOutputName == null)
        {
            folderOutputName = "Output";
        }
    }

}
