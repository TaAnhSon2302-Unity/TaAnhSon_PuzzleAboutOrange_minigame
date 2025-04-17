using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridSwipeManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public static GridSwipeManager Instance;

    [SerializeField] public int rows = 4;
    [SerializeField] public int columns = 4;
    public float swipeThreshold = 30f;

    private Vector2 startTouch;
    private Vector2 endTouch;

    public GridSlot[,] slots;

    public CheckWinCondition checkWinCondition;
    private void OnEnable()
    {
        Instance = this;
        slots = new GridSlot[rows, columns];
        checkWinCondition = gameObject.GetComponent<CheckWinCondition>();
        checkWinCondition.onTimeChange += UIManager.Instance.UpdatecountDown;
    }

    public void RegisterSlot(int row, int col, GridSlot slot)
    {
        if (IsValid(row, col))
        {
            slots[row, col] = slot;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startTouch = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        endTouch = eventData.position;
        DetectSwipe();
        if (checkWinCondition.CheckWinningCodition())
        {
            StartCoroutine(CountDownWinScreenAppear());
        }
    }

    private void DetectSwipe()
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

    private void MoveAll(Vector2Int direction)
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < columns; c++)
                positions.Add(new Vector2Int(r, c));
        if (direction == Vector2Int.right)
            positions.Sort((a, b) => b.y.CompareTo(a.y));
        else if (direction == Vector2Int.left)
            positions.Sort((a, b) => a.y.CompareTo(b.y));
        else if (direction == Vector2Int.down)
            positions.Sort((a, b) => a.x.CompareTo(b.x));
        else if (direction == Vector2Int.up)
            positions.Sort((a, b) => b.x.CompareTo(a.x));

        bool[,] occupied = new bool[rows, columns];
        List<(GridSlot from, GridSlot to)> moves = new List<(GridSlot, GridSlot)>();


        for (int r = 0; r < rows; r++)
            for (int c = 0; c < columns; c++)
                if (slots[r, c] != null && slots[r, c].HasOrangePiece())
                    occupied[r, c] = true;

        foreach (var pos in positions)
        {
            GridSlot current = slots[pos.x, pos.y];
            if (current == null || !current.HasOrangePiece()) continue;

            int newRow = current.x + direction.y;
            int newCol = current.y + direction.x;


            if (IsValid(newRow, newCol))
            {
                GridSlot target = slots[newRow, newCol];

                if (target != null && !occupied[newRow, newCol])
                {
                    occupied[newRow, newCol] = true;
                    occupied[current.x, current.y] = false;
                    moves.Add((current, target));
                }
            }
        }

        foreach (var move in moves)
        {
            move.from.MovePieceTo(move.to);
        }
    }

    private bool IsValid(int row, int col)
    {
        return row >= 0 && row < rows && col >= 0 && col < columns;
    }
    IEnumerator CountDownWinScreenAppear()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.WinScreenActice();

    }
}
