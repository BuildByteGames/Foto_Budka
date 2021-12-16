using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectSpawningHandler))]
public class ObjectSwapButton : MonoBehaviour
{
    [SerializeField]
    Button previosButton;
    [SerializeField]
    Button nextButton;
    ObjectSpawningHandler spawningHandler;
    private void Awake()
    {
        spawningHandler = GetComponent<ObjectSpawningHandler>();

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
