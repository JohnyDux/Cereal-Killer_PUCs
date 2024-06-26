using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    public bool GamePaused;
    private void Awake()
    {
        GamePaused = false;
        pauseMenuScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GamePaused)
        {
            pauseMenuScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!GamePaused)
        {
            pauseMenuScreen.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void UnPauseGame()
    {
        GamePaused = false;
    }
}
