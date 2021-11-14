using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Image img;
    public float VidaMaxima = 100;
    public float VidaAtual;

    void Start()
    {
        VidaAtual = VidaMaxima;
    }

    public void PerdeVida(float dano, out bool morte)
    {
        if (VidaAtual > 0.0f)
        {
            VidaAtual -= dano;
            var result = (float)dano / VidaMaxima;
            img.fillAmount -= result;
            morte = false;
            Handheld.Vibrate();
            
        }
        else morte = true;
    }

    public void ReiniciarBarraDeVida()
    {
        img.fillAmount = 1;
        VidaAtual = VidaMaxima;
    }
}

