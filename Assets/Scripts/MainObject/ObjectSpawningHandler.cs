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
    [SerializeField]
    int mainObjectIndex = 0;

    private void Awake()
    {
        LoadingObjects();
        if (AllObjects.Length >= 0)
        {
             GetLastVievObject();
             ObjectShowing(mainObjectIndex, 0);
        }
        else
        {
            Debug.LogWarning("There are no Object/s to Render");
        }

    }

    private void LoadingObjects()
    {
        AllObjects = global::LoadingObjects.Load("Input");

        foreach (GameObject gameObject in AllObjects)
        {
            gameObject.SetActive(false);
            LeanTween.moveX(gameObject, 0, 0);
        }
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
        ObjectHideing(mainObjectIndex,1);
        mainObjectIndex++;
        PlayerPrefs.SetInt("mainObjectIndex",mainObjectIndex);
        ObjectShowing(mainObjectIndex,-1);

    }
    public void PreviousObject()
    {
        ObjectHideing(mainObjectIndex,-1);
        mainObjectIndex--;
        PlayerPrefs.SetInt("mainObjectIndex", mainObjectIndex);
        ObjectShowing(mainObjectIndex,1);
    }
    void ObjectHideing(int objectIndex,int fadeDirection)
    {
        GameObject gameObject = AllObjects[objectIndex];

        //Hiding gameObject
        LeanTween.moveX(gameObject, movingPosition * fadeDirection, movingTime).setOnComplete(() =>
        {
            gameObject.SetActive(false);

        });
    }
    void ObjectShowing(int objectIndex, int fadeDirection)
    {
        GameObject gameObject = AllObjects[objectIndex];

        //Prepering Gameobject To Show
        gameObject.SetActive(true);

        LeanTween.moveX(gameObject, fadeDirection * movingPosition, 0);

        //Showing it
        LeanTween.moveX(gameObject, 0, movingTime).setOnComplete(() =>
        {
            gameObject.SetActive(true);
            SetObjectAsMain(gameObject);
        });
    }
    void GetLastVievObject()
    {
        if (PlayerPrefs.HasKey("mainObjectIndex"))
        {
            if (PlayerPrefs.GetInt("mainObjectIndex") > AllObjects.Length - 1)
            {
                //There is less 3d objects then last time using application
                mainObjectIndex = AllObjects.Length - 1;
                PlayerPrefs.SetInt("mainObjectIndex",AllObjects.Length - 1);
            }
            else
            {
                //Normal Set
                mainObjectIndex = PlayerPrefs.GetInt("mainObjectIndex", 0);
            }
        }
        else
        {
            //First Boot
            mainObjectIndex = 0;
            PlayerPrefs.SetInt("mainObjectIndex", 0);
        }
    }
    void SetObjectAsMain(GameObject gameObject)
    {
        FindObjectOfType<MainObject>().SetMainObject(gameObject);
        FindObjectOfType<ObjectRotation>().GetNewMainObject();
    }
}
