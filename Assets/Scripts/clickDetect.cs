
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class clickDetect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float start, end;
    private MineField minefield;

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
            }
            else//short
            {
                Debug.Log("Clicked: " + temp.name);
                if((GameManager.instance.state == GameManager.GameState.END_WIN) || (GameManager.instance.state == GameManager.GameState.END_LOSE))
                {
                    return;
                }
                if(GameManager.instance.state == GameManager.GameState.BEGIN)
                {
                    GameManager.instance.state = GameManager.GameState.RUNNING;
                }
                if (minefield.mineAt(temp))
                {
                    minefield.clickAll();
                    GameManager.instance.endGame();
                }
                minefield.clickOne(temp.GetComponent<Mine>().x, temp.GetComponent<Mine>().y);
                if (minefield.isWin())
                {
                    GameManager.instance.endGame();
                }
            }
        }
    }
}