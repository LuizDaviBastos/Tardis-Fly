using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMovimento : MonoBehaviour
{
    public delegate void AtiraDelegateCyberDois();
    public static event AtiraDelegateCyberDois Atirar;



    public void DisparaEventoTiro()
    {
        try
        {
            if (this != null && GetComponent<Animator>() != null)
            {
                if(Atirar != null) Atirar();
            }
        }
        catch (System.Exception ex)
        {

            print(ex.Message);
        }
        
        
    }
}
