using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelScreen : MonoBehaviour
{
    public List<GameObject> gridFrame;
    public GameObject LeelHolder;
    public Level levelPrefab;
    public List<Level> listLevel;
    void OnEnable()
    {
        foreach (var item in listLevel)
        {
            item.SetStatus();
            Debug.Log(item.isUnlock);
        }
        if (listLevel.Count > 0) return;
        GenerateLevel();

    }
    public void GenerateLevel()
    {
        for (int i = 0; i < 8; i++)
        {
            Level level = Instantiate(levelPrefab, LeelHolder.transform);
            level.levelIndex = i;
            level.gridFrame = gridFrame[i];
            if (i == 0)
            {
                level.isUnlock = true;
            }
            level.SetLevelText(i + 1);
            level.SetStatus();
            listLevel.Add(level);
        }
    }
}
