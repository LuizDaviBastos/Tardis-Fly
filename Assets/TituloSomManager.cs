using UnityEngine;

public class TituloSomManager : MonoBehaviour
{
    public AudioSource AudioSourceTransacao;
    public AudioSource AudioSourceImpact;

    public AudioClip[] clips;
    void Start()
    {
        
    }

    public void PlayTransacao()
    {
        AudioSourceTransacao.clip = clips[0];
        AudioSourceTransacao.Play();
    }

    public void PlayImpact()
    {
        AudioSourceImpact.clip = clips[1];
        AudioSourceImpact.Play();
    }
}
