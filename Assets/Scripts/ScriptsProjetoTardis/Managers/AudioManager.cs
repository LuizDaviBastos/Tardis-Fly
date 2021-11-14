using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;
    public List<Audio> audios;

    public static AudioManager instance = null;

    void Awake()
    {
        if (instance == null) instance = this;

        //else if (instance != this) Destroy(instance.gameObject);
    }


    public void PlaySoundFx(TiposAudios tipo, bool loop = false)
    {
        audioSource.loop = loop;
        var audio = audios.Find(x => x.Tipo.Equals(tipo));
        audioSource.clip = audio.clip;
        audioSource.Play();
    }

}

[System.Serializable]
public enum TiposAudios
{
    Alavanca,
    BackGround,
    Materializa,
    Transacao,
    BotaoEnter,
    btnBack,
    XPInicio,
    XPMeio,
    XPFinal
}

[System.Serializable]
public struct Audio
{
    public AudioClip clip;
    public TiposAudios Tipo;
}
