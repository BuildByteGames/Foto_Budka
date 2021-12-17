using UnityEngine;
using UnityEngine.UI;

public class ObjectSwapButton : MonoBehaviour
{
    [SerializeField]
    Button previosButton;
    [SerializeField]
    Button nextButton;
    ObjectSpawningHandler spawningHandler;
    private void Start()
    {
        spawningHandler = FindObjectOfType<ObjectSpawningHandler>();

        previosButton.interactable = spawningHandler.MorePreviosObjects();

        nextButton.interactable = spawningHandler.MoreNextObjects();
    }
    public void ObjectSwap(bool goBack)
    {
        if (goBack)
        {
            spawningHandler.PreviousObject();
        }
        else
        {
            spawningHandler.NextObject();
        }

        previosButton.interactable = spawningHandler.MorePreviosObjects();

        nextButton.interactable = spawningHandler.MoreNextObjects();

    }
}
