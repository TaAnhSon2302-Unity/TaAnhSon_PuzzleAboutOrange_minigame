using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playscreen : MonoBehaviour
{
    public int levelIndex;
    public GameObject GridFrameHolder;
    public GameObject GridFrame;
    public GameObject currentGridFrame;
    public LevelScreen levelScreen;
    void OnEnable()
    {
        if (currentGridFrame != null)
            Destroy(currentGridFrame);
        GridFrame = levelScreen.gridFrame[levelIndex];
        currentGridFrame = Instantiate(GridFrame, GridFrameHolder.transform);

    }
    public void OnClickNext()
    {
        levelIndex++;
        Destroy(currentGridFrame);
        GridFrame = levelScreen.gridFrame[levelIndex];
        currentGridFrame = Instantiate(GridFrame, GridFrameHolder.transform);
        levelScreen.listLevel[levelIndex].isUnlock = true;
        UIManager.Instance.DeactiveScreenActice();

    }
    public void OnClickRetry()
    {
        Destroy(currentGridFrame);
        currentGridFrame = Instantiate(GridFrame, GridFrameHolder.transform);
        UIManager.Instance.DeactiveScreenActice();
    }
    public void OnCliCkBackHome()
    {
        Destroy(currentGridFrame);
    }
}
