using System.Collections;
using UnityEngine;

public class ColisaoLaser : MonoBehaviour
{

    public GameObject tardis;

    public Transform LaserLimiteEsquerda;
    public Transform LaserLimiteDireita;
    public Transform LaserLimiteCima;
    public Transform LaserLimiteBaixo;


    

    public Vector3 PosicaoTardisBackup;
    public bool colidindo = false;
    private bool PassouParaLadoDireito = false;
    

    private void Start()
    {
        tardis = GameObject.FindWithTag("PlayerMov");
    }


    private void FixedUpdate()
    {
        ColisoesLaser();
    }

    private void Update()
    {
       
    }


    void ColisoesLaser()
    {
        /*var condicao = (tardis.transform.position.x >= LaserLimiteEsquerda.position.x && tardis.transform.position.y < LaserLimiteCima.position.y && tardis.transform.position.y > LaserLimiteBaixo.position.y);
        var condicao2 = (tardis.transform.position.x <= LaserLimiteDireita.position.x && tardis.transform.position.y < LaserLimiteCima.position.y && tardis.transform.position.y > LaserLimiteBaixo.position.y);
        if (condicao && PassouParaLadoDireito == false)
        {
            var pos = tardis.transform.position;
            pos.x = LaserLimiteEsquerda.position.x;
            tardis.transform.position = pos;

            imgControle.instancia.ReduzirDistancia();

            StartCoroutine(Reposicionar());
            IEnumerator Reposicionar()
            {
                while ((tardis.transform.position.x >= LaserLimiteEsquerda.position.x && tardis.transform.position.y < LaserLimiteCima.position.y && tardis.transform.position.y > LaserLimiteBaixo.position.y))
                {

                    tardis.transform.position = pos;
                    imgControle.instancia.ReduzirDistancia();
                    yield return null;
                }
            }
        }
        else if (condicao2 && PassouParaLadoDireito)
        {
            var pos = tardis.transform.position;
            pos.x = LaserLimiteDireita.position.x;
            tardis.transform.position = pos;

            imgControle.instancia.ReduzirDistancia();

            StartCoroutine(Reposicionar());
            IEnumerator Reposicionar()
            {
                while ((tardis.transform.position.x <= LaserLimiteDireita.position.x && tardis.transform.position.y < LaserLimiteCima.position.y && tardis.transform.position.y > LaserLimiteBaixo.position.y))
                {

                    tardis.transform.position = pos;
                    imgControle.instancia.ReduzirDistancia();
                    yield return null;
                }
            }
        }

        else if (tardis.transform.position.x > LaserLimiteEsquerda.position.x && (tardis.transform.position.y > LaserLimiteCima.position.y || tardis.transform.position.y < LaserLimiteBaixo.position.y))
        {
            print("Desviou do raio para frente");
            PassouParaLadoDireito = true;
        }
        else if (tardis.transform.position.x < LaserLimiteDireita.position.x && (tardis.transform.position.y > LaserLimiteCima.position.y || tardis.transform.position.y < LaserLimiteBaixo.position.y))
        {
            print("Desviou do raio para traz");
            PassouParaLadoDireito = false;
        }*/
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        print("entrou");
    }
    */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMov"))
        {
            //VidaManager.instancia.PerdeVida(5f);
        }
        
    }

    private Coroutine _coroutine = null;
    private void OnCollisionStay2D(Collision2D collision)
    {

        // imgControle.instancia.ReduzirDistancia();

        

        if (collision.collider.CompareTag("PlayerMov"))
        {
           VidaManager.instancia.PerdeVida(5f);
        }
    }

    



}
