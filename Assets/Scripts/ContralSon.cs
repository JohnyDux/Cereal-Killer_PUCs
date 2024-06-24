using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorSom : MonoBehaviour
{
    private bool estadoSom = true;
    [SerializeField] private AudioSource fundoMusical;

    [SerializeField] private Sprite somLigadoSprite;
    [SerializeField] private Sprite somDesligadoSprite;
    [SerializeField] private Image muteImage;

    // Função para ligar e desligar o som
    public void LigarDesligarSom()
    {
        estadoSom = !estadoSom;
        fundoMusical.enabled = estadoSom;
        
        if (estadoSom)
        {
            muteImage.sprite = somLigadoSprite;
        }
        else 
        {
            muteImage.sprite = somDesligadoSprite;
        }
    }

    // Função para ajustar o volume da música de fundo
    public void VolumeMusical(float value)
    {
        fundoMusical.volume = value; // Corrigido para 'volume'
    }
}
