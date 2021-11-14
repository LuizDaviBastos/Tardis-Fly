using System;
using System.Collections;
using UnityEngine;

public class CybermanV2 : MeuMonoBehaviour
{


    //Configuracoes do Tiro
    public GameObject Tiro;
    public float speedTiro = 5f;
    public float t;
    public float forca = -100;
    public float zAngle = 0;

    //Preferencias Tiro
    public bool tiroPersegue;
    public Transform Perseguir;
    public float tempoMinDisparo = 0f;
    public float tempoMaxDisparo = 0f;
    public GameObject Pai;

    private bool ToRight;

    //privadas
    public bool travaTiro = false;


    private void Start()
    {
        //normaliza animação deixando ela em tempos diferentes entre todos os cyber2.0 da cena
        StartCoroutine(EsperaParaAnimar(UnityEngine.Random.Range(0.2f, 0.8f)));

        //Procurando a tardis na tela para o tiro perseguir
        if (GameObject.FindWithTag("Player") != null) Perseguir = GameObject.FindWithTag("Player").transform;
        if (Pai == null ) ToRight = (transform.localScale.x > 0);

        AnimMovimento.Atirar += Atirar;
    }

    private void Update()
    {
        if (travaTiro)
        {
            travaTiro = false;
            StartCoroutine(EsperaParaAtirar(UnityEngine.Random.Range(tempoMinDisparo, tempoMaxDisparo), tiroPersegue));
        }

        if (Pai != null)
        {
            gameObject.FlipParaTardis(Pai.transform.localScale.x > 0);

            if (Pai.transform.localScale.x > 0) ToRight = (transform.localScale.x > 0);
            else ToRight = (transform.localScale.x < 0);

            if (Pai.transform.localScale.y < 0)
            {
                gameObject.ModificarEscalaY(-1, true);
            }
            else gameObject.ModificarEscalaY(1, true);
        }
    }

    IEnumerator EsperaParaAtirar(float time, bool persegue)
    {
        yield return new WaitForSeconds(time);
        if (persegue) AtirarPersegue();
        else Atirar();

        travaTiro = true;
    }

    IEnumerator EsperaParaAnimar(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<Animator>().enabled = true;
    }

    public void Atirar()
    {
        if (PlayerIsLive)
        {
            if (this != null)
            {
                var instTiro = Instantiate(Tiro, new Vector2(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y), Quaternion.Euler(Tiro.transform.eulerAngles));
                zAngle = (transform.localEulerAngles.z);

                forca = (ToRight ? Math.Abs(forca) * -1 : Math.Abs(forca));

                float x = forca * Mathf.Cos(zAngle * Mathf.Deg2Rad);
                float y = forca * Mathf.Sin(zAngle * Mathf.Deg2Rad);

                instTiro.GetComponent<Disparo>().Impulsionar(new Vector2(x, y), ToRight);
            }

            
        }
        
    }

    public void AtirarPersegue()
    {
        if (PlayerIsLive)
        {
            var instTiro = Instantiate(Tiro, new Vector2(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y), Quaternion.identity);


            //Cuidando do destino do tiro
            if (Perseguir != null) instTiro.GetComponent<Disparo>().Seguir(Perseguir.position, transform.position, speedTiro, ToRight);
        }
        

    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (collision.gameObject.GetComponent<Player>().invuneravel == false) VidaManager.instancia.PerdeVida(Dano.DanoColisaoCyberV2);

                LevelManager.instancia.GanhaXP(Pontuacao.XPCyberV2);

                var PrefabExplosao = GetComponent<VidaAlvo>().PrefabExplosao;
                Instantiate(PrefabExplosao, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Destroy(gameObject);
                break;
            case "Escudo":
                var danoEscudo = collision.gameObject.GetComponent<Escudo>().DanoPorSegundo;
                GetComponent<VidaAlvo>().RecebeDano(danoEscudo);
                break;
            case "AreaDoJogo":
                GetComponent<VidaAlvo>().Invuneravel = false;
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AreaDoJogo") == false) GetComponent<VidaAlvo>().Invuneravel = true;
    }



}


