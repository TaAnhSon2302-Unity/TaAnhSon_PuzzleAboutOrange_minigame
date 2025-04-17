using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayScreenManager : MonoBehaviour
{
    public static PlayScreenManager Instance;
    [SerializeField] GameObject gridslot;
    [SerializeField] public int rows = 4, colloumns = 4;
    [SerializeField] GameObject holderLocation;
    OrangePieceSpawner orangePieceSpawner;
    void Start()
    {
        Instance = this;
        orangePieceSpawner = GetComponent<OrangePieceSpawner>();
        SpamGrid();
        orangePieceSpawner.SpawnOranagePiece();
    }
    private void SpamGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < colloumns; col++)
            {
                GameObject slot = Instantiate(gridslot, holderLocation.transform);
                GridSlot gridSlot = slot.GetComponent<GridSlot>();
                gridSlot.x = row;
                gridSlot.y = col;
            }
        }
    }
}
