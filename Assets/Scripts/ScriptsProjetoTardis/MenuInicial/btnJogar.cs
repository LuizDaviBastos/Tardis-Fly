using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnJogar : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlaySomBtn()
    {
        AudioManager.instance.PlaySoundFx(TiposAudios.Materializa);
    }
}
