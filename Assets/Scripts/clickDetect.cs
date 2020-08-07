
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class clickDetect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float start, end;
    private MineField minefield;
    public static bool clicking = false;
    private GameObject last;


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
        last = eventData.pointerCurrentRaycast.gameObject;
        start = Time.time;
        CameraController.logMove();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject temp = eventData.pointerCurrentRaycast.gameObject;
        if (!CameraController.moving && CameraController.moved == false)
        {
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
        if(CameraController.moved == true)
        {
            CameraController.moved = false;
        }

    }
}