using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelSetting;

    public void Play()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void AbrirSetting()
    {
        painelMenuInicial.SetActive(false);
        painelSetting.SetActive(true);
    }

    public void FecharSetting()
    {
        painelSetting.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void ExitJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
