using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioSonico : MonoBehaviour
{

    public float Distance = 5f;
    public LayerMask LayerAlvo;
    public Transform Destino;
    public float DanoPorSegundo = 0.2f;
    public float minDeph, maxDeph;
    public ParticleSystem ParticulaRaio;
    public AudioSource audioSonico;
    public ulong delayAudio;

    private Coroutine _RefreshRayCast;

    private bool atirando = false;


    public static RaioSonico instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
        }

        else Destroy(this.gameObject);
    }


    void Update()
    {

    }

    public void StartFire()
    {
        
        audioSonico.Play();

        
        atirando = true;

        _RefreshRayCast = StartCoroutine(RefreshRayCast());


        IEnumerator RefreshRayCast()
        {
            while (atirando)
            {
                RaycastHit2D ray = Physics2D.Raycast(Destino.position, transform.right, Distance, LayerAlvo);
                
                if (ray)
                {
                    ray.collider.gameObject.GetComponent<VidaAlvo>().RecebeDano(DanoPorSegundo);
                    
                    Debug.DrawRay(Destino.position, ray.point, Color.green);
                }
                else
                {
                    Debug.DrawRay(Destino.position, transform.right * Distance, Color.green);
                }

                yield return null;
            }

        }

        if (ParticulaRaio.isPlaying == false) ParticulaRaio.Play();
    }



    public void StopFire()
    {
        audioSonico.Stop();
        if (_RefreshRayCast != null) StopCoroutine(_RefreshRayCast);
        atirando = false;
        ParticulaRaio.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

}
