using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CheckWinCondition : MonoBehaviour
{
    public GridSwipeManager gridSwipeManager;
    public bool isOutOfTime;
    public static float  timeLimit = 45f;
    void Start()
    {
        gridSwipeManager = GridSwipeManager.Instance;
    }
    IEnumerator StartCoutDown()
    {
        yield return new WaitForSeconds(timeLimit);
        isOutOfTime = true;
    }
    public bool CheckWinningCodition()
    {
        for (int r = 0; r < gridSwipeManager.rows; r++)
        {
            for (int c = 0; c < gridSwipeManager.rows; c++)
            {
                GridSlot currentSlot = gridSwipeManager.slots[r, c];

                if (currentSlot == null || !currentSlot.HasOrangePiece()) continue;
                OrangeSlice currentSlice = currentSlot.GetOrangeSlice();
                //Id =1
                if (currentSlice.id == 1)
                {
                    if (IsValid(r, c + 1) && gridSwipeManager.slots[r, c + 1].HasOrangePiece() && gridSwipeManager.slots[r, c + 1].GetOrangeSlice().id == 2)
                    {
                        if (IsValid(r + 1, c) && gridSwipeManager.slots[r + 1, c].HasOrangePiece() && gridSwipeManager.slots[r + 1, c].GetOrangeSlice().id == 3)
                        {
                            return true;
                        }
                    }
                }
                else if (currentSlice.id == 2)
                {
                    if (IsValid(r, c - 1) && gridSwipeManager.slots[r, c - 1].HasOrangePiece() && gridSwipeManager.slots[r, c + 1].GetOrangeSlice().id == 1)
                    {
                        if (IsValid(r + 1, c) && gridSwipeManager.slots[r + 1, c].HasOrangePiece() && gridSwipeManager.slots[r + 1, c].GetOrangeSlice().id == 4)
                        {
                            return true;
                        }
                    }
                }
                else if (currentSlice.id == 3)
                {
                    if (IsValid(r, c + 1) && gridSwipeManager.slots[r, c + 1].HasOrangePiece() && gridSwipeManager.slots[r, c + 1].GetOrangeSlice().id == 4)
                    {
                        if (IsValid(r - 1, c) && gridSwipeManager.slots[r - 1, c].HasOrangePiece() && gridSwipeManager.slots[r - 1, c].GetOrangeSlice().id == 1)
                        {
                            return true;
                        }
                    }
                }
                else if (currentSlice.id == 4)
                {
                    if (IsValid(r, c - 1) && gridSwipeManager.slots[r, c - 1].HasOrangePiece() && gridSwipeManager.slots[r, c - 1].GetOrangeSlice().id == 4)
                    {
                        if (IsValid(r - 1, c) && gridSwipeManager.slots[r - 1, c].HasOrangePiece() && gridSwipeManager.slots[r - 1, c].GetOrangeSlice().id == 2)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    private bool IsValid(int row, int col)
    {
        return row >= 0 && row < gridSwipeManager.rows && col >= 0 && col < gridSwipeManager.columns;
    }
}
