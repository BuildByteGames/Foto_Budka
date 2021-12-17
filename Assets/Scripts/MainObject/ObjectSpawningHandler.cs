using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawningHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] AllObjects; //TODO: Load Object From Input
    [SerializeField]
    Material transparentMateral;
    [Header("TransitionOptions")]
    [SerializeField]
    float fadeingTime = 1;
    [SerializeField]
    float movingTime = 1;
    [SerializeField]
    float movingPosition = 30;
    int mainObjectIndex { get; set; }

    private void Awake()
    {
        GetLastVievObject();

        PreSpawnObjects(AllObjects.Length);

        ObjectSpawn(mainObjectIndex, 0);

    }
    public bool MoreNextObjects()
    {
        if (mainObjectIndex < AllObjects.Length - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool MorePreviosObjects()
    {
        if (mainObjectIndex > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void NextObject()
    {
        ObjectDespawn(mainObjectIndex,1);
        mainObjectIndex++;
        ObjectSpawn(mainObjectIndex,-1);

    }
    public void PreviousObject()
    {
        ObjectDespawn(mainObjectIndex,-1);
        mainObjectIndex--;
        ObjectSpawn(mainObjectIndex,1);
    }
    void ObjectDespawn(int objectIndex,int fadeDirection)
    {
        GameObject gameObject = AllObjects[objectIndex];

        //Hiding gameObject
        LeanTween.moveX(gameObject, movingPosition * fadeDirection, movingTime);

        LeanTween.alpha(gameObject, 0, fadeingTime).setOnComplete(() => 
        {
            gameObject.SetActive(false); 

        });
    }
    void ObjectSpawn(int objectIndex, int fadeDirection)
    {
        GameObject gameObject = AllObjects[objectIndex];

        //Prepering Gameobject To Show
        gameObject.SetActive(true);

        LeanTween.moveX(gameObject, fadeDirection * movingPosition, 0);

        //Showing it
        LeanTween.moveX(gameObject, 0, movingTime);

        LeanTween.alpha(gameObject, 1, fadeingTime).setOnComplete(() =>
        {
            gameObject.SetActive(true);
            SetObjectAsMain(gameObject);
        });
    }
    void GetLastVievObject()
    {
        if (PlayerPrefs.HasKey("mainObjectIndex"))
        {
            mainObjectIndex = PlayerPrefs.GetInt("mainObjectIndex", 0);
        }
        else
        {
            mainObjectIndex = 0;
            PlayerPrefs.SetInt("mainObjectIndex", 0);
        }
    }
    void PreSpawnObjects(int objectAmount)
    {
        for (int i = 0; i < objectAmount; i++)
        {
            GameObject gameObject = Instantiate(AllObjects[i]);
            gameObject.GetComponent<Renderer>().material = transparentMateral;
            gameObject.SetActive(false);
            LeanTween.alpha(gameObject, 0, 0);
            AllObjects[i] = gameObject;
        }
    }
    void SetObjectAsMain(GameObject gameObject)
    {
        FindObjectOfType<MainObject>().SetMainObject(gameObject);
        FindObjectOfType<ObjectRotation>().GetNewMainObject();
    }
}
