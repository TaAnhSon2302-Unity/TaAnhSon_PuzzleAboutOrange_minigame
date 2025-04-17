using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridSwipeManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public static GridSwipeManager Instance;

    [SerializeField] int rows = 4, columns = 4;
    [SerializeField] GameObject holderLocation;
    private Vector2 startTouch;
    private Vector2 endTouch;
    public float swipeThreshold = 30f;

    public GridSlot[,] slots;
    private void Awake()
    {
        Instance = this;
        slots = new GridSlot[rows, columns];
    }
    public void RegisterSlot(int row, int col, GridSlot slot)
    {
        slots[row, col] = slot;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        startTouch = eventData.position;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        endTouch = eventData.position;
        DectectSwipe();
    }
    void DectectSwipe()
    {
        Vector2 delta = endTouch - startTouch;
        if (delta.magnitude < swipeThreshold) return;
        Vector2 dir = delta.normalized;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
                MoveAll(Vector2Int.right);
            else
                MoveAll(Vector2Int.left);
        }
        else
        {
            if (dir.y > 0)
                MoveAll(Vector2Int.down);
            else
                MoveAll(Vector2Int.up);
        }
    }
    void MoveAll(Vector2Int direction)
    {
        bool moved = false;
        List<Vector2Int> positions = new List<Vector2Int>();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                positions.Add(new Vector2Int(r, c));
            }
        }
        if (direction == Vector2Int.right)
            positions.Sort((a, b) => b.y.CompareTo(a.y));
        else if (direction == Vector2Int.left)
            positions.Sort((a, b) => a.y.CompareTo(b.y));
        else if (direction == Vector2Int.down)
            positions.Sort((a, b) => b.x.CompareTo(a.x));
        else if (direction == Vector2Int.up)
            positions.Sort((a, b) => a.x.CompareTo(b.x));

        foreach (var pos in positions)
        {
            GridSlot current = slots[pos.x, pos.y];
            if (current.HasOrangePiece())
            {
                int newRow = current.x + direction.y;
                int newCol = current.y + direction.x;

                if (IsValid(newRow, newCol) && !slots[newRow, newCol].HasOrangePiece())
                {
                    GridSlot next = slots[newRow, newCol];
                    current.MovePieceTo(next);
                    moved = true;
                }
            }
        }
    }
    bool IsValid(int row, int col)
    {
        return row >= 0 && row < PlayScreenManager.Instance.rows && col >= 0 && col < PlayScreenManager.Instance.colloumns;
    }
}
