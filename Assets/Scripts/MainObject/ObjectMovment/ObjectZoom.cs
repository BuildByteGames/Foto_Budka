using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectZoom : MonoBehaviour
{
    Camera mainCamera { get; set; }
    BoxCollider boxCollider { get; set; }
    [Header("Zoom Values")]
    [SerializeField]
    float zoomingSpeed = 1;

    [Header("Camped Values")]
    [SerializeField]
    float maxZoomIn = 0.0001f;
    [SerializeField]
    float maxZoomOut = Mathf.Infinity;

    private void Awake()
    {
        mainCamera = Camera.main;

        //Setting Up Colider Borders For Mouse Drag To Work Properly

        boxCollider = gameObject.GetComponent<BoxCollider>();
        float cameraSizeDubble = mainCamera.orthographicSize * 2;
        boxCollider.size = new Vector2(cameraSizeDubble * mainCamera.aspect, cameraSizeDubble);

        // Making Sure Max Values are Positive
        maxZoomIn = Mathf.Abs(maxZoomIn);
        maxZoomOut = Mathf.Abs(maxZoomOut);
    }

    private void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            mainCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomingSpeed * Time.deltaTime; // Subtracting insted of Adding because scrol direction is flipped

            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize,maxZoomIn,maxZoomOut);

            float cameraSizeDubble = mainCamera.orthographicSize * 2; 

            boxCollider.size = new Vector2(cameraSizeDubble * mainCamera.aspect, cameraSizeDubble); //Setting new collider size For Mouse Drag to work
        }

    }
}
