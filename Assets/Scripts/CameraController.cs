using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static bool moving = false;

    public float zoomMin = 1;
    public float zoomMax = 10;
    public float zoomSpeed = 5;
    public float touchZoomSpeed = 0.2f;
    Vector3 start;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.touchCount == 2)
        {
            moving = true;
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1Prev = touch1.position - touch1.deltaPosition;
            Vector2 touch2Prev = touch2.position - touch2.deltaPosition;

            float prevMagnitude = (touch1Prev - touch2Prev).magnitude;
            float currMagnitude = (touch1.position - touch2.position).magnitude;

            float diff = currMagnitude - prevMagnitude;

            zoom(diff * touchZoomSpeed);
            moving = false;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = start - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
            if(direction.x != 0 || direction.y != 0)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
        }
        zoom(Input.GetAxis("Mouse ScrollWheel")* zoomSpeed);
    }

    void zoom(float z)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - z, zoomMin, zoomMax);
    }
}
