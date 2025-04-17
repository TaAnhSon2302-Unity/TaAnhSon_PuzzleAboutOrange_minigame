using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public bool isUnlock;
    public TextMeshProUGUI text;
    public List<GameObject> active;
    public GameObject gridFrame;
    public Button button;
    public int levelIndex;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickPlay);
    }
    public void SetLevelText(int index)
    {
        text.text = "Level: " + index.ToString();
    }
    public void SetStatus()
    {
        if (isUnlock)
        {
            foreach (var item in active)
            {
                item.SetActive(false);
            }
            button.interactable = true;
        }
        else
        {
            foreach (var item in active)
            {
                item.SetActive(true);
            }
            button.interactable = false;
        }
    }
    public void OnClickPlay()
    {
        UIManager.Instance.OpenPlayScreen(levelIndex);
    }
}
