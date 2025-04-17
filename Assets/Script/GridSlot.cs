using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridSlot : MonoBehaviour
{
    public int x;
    public int y;
    [SerializeField] OrangeSlice orangePiece;
    [SerializeField] GameObject block;
    private void Start()
    {
        GridSwipeManager.Instance.RegisterSlot(x, y, this);
        Debug.Log("Assigned");
    }
    public bool HasOrangePiece() => orangePiece != null;
    public void RemoveOrangePiece()
    {
        orangePiece = null;
    }

    public void MovePieceTo(GridSlot targetSlot)
    {
        if (orangePiece == null || targetSlot == null || targetSlot.block !=null || targetSlot.orangePiece != null) return;
        targetSlot.orangePiece = orangePiece;
        orangePiece.transform.SetParent(targetSlot.transform);
        orangePiece.transform.DOMove(targetSlot.transform.position, 0.2f);
        orangePiece = null;
    }
    public OrangeSlice GetOrangeSlice()
    {
        return orangePiece;
    }
}
