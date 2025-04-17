using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI countDown;
    [SerializeField] Button homeButton, resetButton, nextLevel, startButton;
    [SerializeField] private GameObject WinScreen, LooseScreen, HomeScreen, playSrceen, levelScreen;
    [SerializeField] private Playscreen playscreen;
    [SerializeField] private LevelScreen levelscreen;
    void OnEnable()
    {
        Instance = this;
        AddListener();
        OnkStart();
    }
    public void UpdatecountDown(float timeToDisplay)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeToDisplay);
        countDown.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds).ToString();
    }
    public void OnClickStart()
    {
        HomeScreen.SetActive(false);
        levelScreen.SetActive(true);
    }
    public void AddListener()
    {
        startButton.onClick.AddListener(OnClickStart);
    }
    public void OnClickLevel()
    {
        playSrceen.SetActive(true);
        levelScreen.SetActive(false);
    }
    public void WinScreenActice()
    {
        WinScreen.SetActive(true);
        levelscreen.listLevel[playscreen.levelIndex + 1].isUnlock = true;
    }
    public void LoseScreenActice()
    {
        LooseScreen.SetActive(true);
    }
    public void DeactiveScreenActice()
    {
        WinScreen.SetActive(false);
        LooseScreen.SetActive(false);
    }
    public void OnHomeClick()
    {
        WinScreen.SetActive(false);
        LooseScreen.SetActive(false);
        playSrceen.SetActive(false);
        levelScreen.SetActive(false);
        HomeScreen.SetActive(true);
        playscreen.OnCliCkBackHome();
    }
    public void OnkStart()
    {
        WinScreen.SetActive(false);
        LooseScreen.SetActive(false);
        playSrceen.SetActive(false);
        levelScreen.SetActive(false);
        HomeScreen.SetActive(true);
    }
    public void OpenPlayScreen(int levelIndex)
    {
        playscreen.levelIndex = levelIndex;
        playscreen.gameObject.SetActive(true);
        levelScreen.SetActive(false);
    }
    public void QuitPlayMode()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
}
