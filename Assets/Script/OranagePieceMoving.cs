using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OranagePieceMoving : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 startTouch;
    private Vector2 endTouch;

    private GridSlot currentSlot;
    private Transform gridHolder;

    private void Start()
    {
        currentSlot = GetComponentInParent<GridSlot>();
        gridHolder = transform.parent.parent;
    }
    public float swipeThreshole = 20f;
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        startTouch = eventData.position;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        endTouch = eventData.position;
        DetectSwipe();
    }

    void DetectSwipe()
    {
        Vector2 delta = endTouch - startTouch;
        if (delta.magnitude < swipeThreshole) return;
        Vector2 dir = delta.normalized;
        int targetRow = currentSlot.x;
        int targetCol = currentSlot.y;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            targetCol += dir.x > 0 ? 1 : -1;
        }
        else
        {
            targetRow += dir.y > 0 ? -1 : 1;
        }
        if(IsValidPosition(targetRow,targetCol))
        {
            GridSlot targetSlot = FindSlotAt(targetRow,targetCol);
            if(targetSlot !=null && targetSlot.transform.childCount ==0)
            {
               transform.SetParent(targetSlot.transform);
               transform.localPosition = Vector3.zero;
               currentSlot = targetSlot;
            }
        }
    }
    bool IsValidPosition(int row, int col)
    {
        return row >= 0 && row < 4 && col >= 0 && col < 4;
    }
    GridSlot FindSlotAt(int row, int col)
    {
        foreach (Transform slot in gridHolder)
        {
            GridSlot gs = slot.GetComponent<GridSlot>();
            if (gs != null && gs.x == row && gs.y == col)
            {
                return gs;
            }
        }
        return null;
    }
}
