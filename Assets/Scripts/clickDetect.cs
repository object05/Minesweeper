
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class clickDetect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float start, end;

    void Start()
    {
        addPhysics2DRaycaster();
    }

    void addPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = GameObject.FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        start = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!CameraController.moving)
        {
            end = Time.time;
            if (end - start > 0.5f)//long
            {
                Debug.Log("Long clicked: " + eventData.pointerCurrentRaycast.gameObject.name);


            }
            else//short
            {
                Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
            }
        }
    }
}