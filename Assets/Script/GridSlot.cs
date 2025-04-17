using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public int x;
    public int y;
    private void Start()
    {
        GridSwipeManager.Instance.RegisterSlot(x, y, this);
    }
    public bool HasOrangePiece()
    {
        return transform.childCount > 0;
    }

    public void MovePieceTo(GridSlot targetSlot)
    {
        Transform piece = transform.GetChild(0);
        piece.SetParent(targetSlot.transform);
        piece.localPosition = Vector3.zero;
    }
}
