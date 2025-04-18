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
    void Start()
    {
        if (orangePiece == null)
        {
            orangePiece = GetComponentInChildren<OrangeSlice>();
        }
        if (block == null)
        {
            Transform blockTransform = transform.Find("Block");
            if (blockTransform != null)
            {
                block = blockTransform.gameObject;
            }
        }
        GridSwipeManager.Instance.RegisterSlot(x, y, this);
    }
    public bool HasOrangePiece() => orangePiece != null;
    public void RemoveOrangePiece()
    {
        orangePiece = null;
    }

    public void MovePieceTo(GridSlot targetSlot)
    {
        if (orangePiece == null || targetSlot == null || targetSlot.block != null || targetSlot.orangePiece != null) return;
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
