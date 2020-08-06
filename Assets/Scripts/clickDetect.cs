
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class clickDetect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float start, end;
    private MineField minefield;
    public static bool clicking = false;


    void Start()
    {
        addPhysics2DRaycaster();
        minefield = GameManager.instance.GetComponent<MineField>();
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
        if (!CameraController.moving)
        {
            clicking = true;
        }
        else
        {
            clicking = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!CameraController.moving)
        {
            GameObject temp = eventData.pointerCurrentRaycast.gameObject;
            end = Time.time;
            if (end - start > 0.5f)//long
            {
                Debug.Log("Long clicked: " + temp.name);
                GameManager.instance.GetComponent<MineField>().longClick(temp);
            }
            else//short
            {
                Debug.Log("Clicked: " + temp.name);
                GameManager.instance.GetComponent<MineField>().normalClick(temp);
            }
        }
        clicking = false;
    }
}