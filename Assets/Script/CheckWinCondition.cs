using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CheckWinCondition : MonoBehaviour
{
    public GridSwipeManager gridSwipeManager;
    public bool isOutOfTime;
    public static float timeLimit = 45f;
    public event Action<float> onTimeChange;
    void OnEnable()
    {
        gridSwipeManager = GridSwipeManager.Instance;
        StartCoroutine(StartCoutDown());
    }
    IEnumerator StartCoutDown()
    {
        float time = timeLimit;
        while (time >= 0)
        {
            yield return new WaitForSeconds(1f);
            onTimeChange?.Invoke(time--);
        }
        UIManager.Instance.LoseScreenActice();
    }
    public bool CheckWinningCodition()
    {
        for (int r = 0; r < gridSwipeManager.rows - 1; r++)
        {
            for (int c = 0; c < gridSwipeManager.columns - 1; c++)
            {
                GridSlot slot1 = gridSwipeManager.slots[r, c];
                GridSlot slot2 = gridSwipeManager.slots[r, c + 1];
                GridSlot slot3 = gridSwipeManager.slots[r + 1, c];
                GridSlot slot4 = gridSwipeManager.slots[r + 1, c + 1];

                if (slot1 == null || slot2 == null || slot3 == null || slot4 == null) continue;
                if (!slot1.HasOrangePiece() || !slot2.HasOrangePiece() || !slot3.HasOrangePiece() || !slot4.HasOrangePiece()) continue;

                int id1 = slot1.GetOrangeSlice().id;
                int id2 = slot2.GetOrangeSlice().id;
                int id3 = slot3.GetOrangeSlice().id;
                int id4 = slot4.GetOrangeSlice().id;

                // Kiểm tra xem 4 mảnh có đúng vị trí không
                if (id1 == 1 && id2 == 2 && id3 == 3 && id4 == 4)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
