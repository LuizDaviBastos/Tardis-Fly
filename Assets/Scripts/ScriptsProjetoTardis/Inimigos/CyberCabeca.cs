using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CyberCabeca : MeuMonoBehaviour
{
    public float Distancia = 5f;
    public GameObject PrefabExplosao;
    public GameObject Alvo;
    public Vector3 _origem;
    public float _speed = 5f;
    public bool rotacionando = true;
    public bool seguirAlvo = true;
    public bool invuneravel = true;

    private float distancia;
    public float tempoParaVuneravel = 0.8f;

    private Ray raio;

    private void Start()
    {
        if (GameObject.FindWithTag("Player") != null) Alvo = GameObject.FindWithTag("Player");
        _origem = transform.position;
        distancia = Vector3.Distance(Alvo.transform.position, _origem);

        StartCoroutine(DeixarVulneravel());
        IEnumerator DeixarVulneravel()
        {
            yield return new WaitForSeconds(tempoParaVuneravel);
            invuneravel = false;
        }

    }

    private void FixedUpdate()
    {
        if (PlayerIsLive)
        {
            SeguirAlvo();

            if (Vector3.Distance(Alvo.transform.position, transform.position) <= (distancia / 3))
            {
                rotacionando = false;
                seguirAlvo = false;
            }
            Rotaciona();
        }
        else
        {
            Instantiate(PrefabExplosao, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    void SeguirAlvo()
    {
        if (Alvo != null && _origem != null)
        {
            if(seguirAlvo) raio = new Ray(transform.position, Alvo.transform.position - _origem);
            transform.position = Vector3.MoveTowards(transform.position, raio.GetPoint(distancia), _speed * Time.deltaTime);
        }
       
    }

    void Rotaciona()
    {
       /* if (rotacionando)
        {
            var newZ = Mathf.Atan2(Alvo.transform.position.y, Alvo.transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, newZ); 
            
        }*/

        if (rotacionando)
        {
            var des = raio.GetPoint(distancia);
            //Rotacao da cabeca
            Quaternion newRotation;
            newRotation = Quaternion.LookRotation(des - transform.position);
            GetComponent<Rigidbody2D>().MoveRotation(newRotation);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "RaioSonico":

                if (invuneravel == false)
                {
                    Instantiate(PrefabExplosao, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    Destroy(gameObject);
                }
                
                break;
            case "Player":
                //TODO - Modificar dano causado pela CyberCabeça no player
                if (collision.gameObject.GetComponent<Player>().invuneravel == false) VidaManager.instancia.PerdeVida(Dano.DanoColisaoCyberV2);

                //TODO - Modificar pontuação da CyberCabeça
                LevelManager.instancia.GanhaXP(Pontuacao.XPCyberV2);

                Instantiate(PrefabExplosao, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Destroy(gameObject);
                break;
            case "Escudo":
                Instantiate(PrefabExplosao, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

}

