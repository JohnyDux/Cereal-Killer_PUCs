using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [Header("Pause")]
    public GameObject pauseMenuScreen;
    public bool GamePaused;

    [Header("Sound")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider specialFxSlider;
    public AudioMixer audioMixer;

    private float sliderValue; // Variable to hold the slider value

    private void Awake()
    {
        //Audio Sliders
        UpdateSlidersValue(masterSlider.value, "MasterVol");
        UpdateSlidersValue(musicSlider.value, "MusicVol");
        UpdateSlidersValue(specialFxSlider.value, "SpecialFxVol");

        masterSlider.onValueChanged.AddListener(delegate { UpdateSlidersValue(masterSlider.value, "MasterVol"); });
        musicSlider.onValueChanged.AddListener(delegate { UpdateSlidersValue(musicSlider.value, "MusicVol"); });
        specialFxSlider.onValueChanged.AddListener(delegate { UpdateSlidersValue(specialFxSlider.value, "SpecialFxVol"); });

        GamePaused = false;
        pauseMenuScreen.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (GamePaused)
        {
            pauseMenuScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if (!GamePaused)
        {
            pauseMenuScreen.gameObject.SetActive(false);
            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void UnPauseGame()
    {
        GamePaused = false;
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void UpdateSlidersValue(float value, string name)
    {
        // Update the variable with the slider value
        sliderValue = value;

        audioMixer.SetFloat(name, sliderValue);
    }
}
