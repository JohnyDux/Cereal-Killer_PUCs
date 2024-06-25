using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject loadingScreen;

    private void Awake()
    {
        mainMenuScreen.gameObject.SetActive(true);
        loadingScreen.gameObject.SetActive(false);
    }

    public void LoadNewScene(string sceneName)
    {
        loadingScreen.gameObject.SetActive(true);
        mainMenuScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
}
