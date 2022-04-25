using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragShape : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private Vector3 temp;
    private void OnMouseDrag()
    {
        temp = GetMouseWorldPos() + mOffset;
        temp.y = 0; //The user should not be able to move the object in the y-axis. A gameplay choice.
        transform.position = temp;

    }

    private void OnEnable()
    {
        EventBroadcaster.shapesCollidedInfo += OnMouseUp;
    }

    private void OnDisable()
    {
        EventBroadcaster.shapesCollidedInfo -= OnMouseUp;
    }
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }

    private void OnMouseUp()
    {

    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void shapesMatchedEvent()
    {
        //Still on it.
    }
}
