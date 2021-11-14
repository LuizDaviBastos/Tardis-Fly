using System.Collections;
using UnityEngine;

public class CybermanV1 : MeuMonoBehaviour
{

    //Especificações Cyberman2.0
    public GameObject PrefabExplosao;
    public float vidaMaxima = 10;
    public float vidaAtual = 0;

    public GameObject Pai;


    public Sprite[] spritesDano;

    void Start()
    {
        vidaAtual = vidaMaxima;

        //normaliza animação deixando ela em tempos diferentes entre todos os cyber1.0 da cena
        StartCoroutine(EsperaParaAnimar(Random.Range(0.2f, 0.8f)));

        if (Pai != null)
        {
            if (Pai.transform.localScale.y < 0)
            {
                gameObject.ModificarEscalaX(-1, true);
            }
            else gameObject.ModificarEscalaX(1, true);
        }
    }



    IEnumerator EsperaParaAnimar(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<Animator>().enabled = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            
            case "Player":
                if (collision.gameObject.GetComponent<Player>().invuneravel == false) VidaManager.instancia.PerdeVida(Dano.DanoColisaoCyberV1);

                LevelManager.instancia.GanhaXP(Pontuacao.XPCyberV1);

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
