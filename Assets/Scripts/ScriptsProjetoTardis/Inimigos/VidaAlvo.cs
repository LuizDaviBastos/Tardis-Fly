using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaAlvo : MonoBehaviour
{

    public float vidaMaxima = 10;
    public float vidaAtual = 0;
    public GameObject PrefabExplosao;
    public bool Invuneravel = false;
    public TipoPontuacao tipoPontuacao;
    public bool morreu = false;


    public Sprite[] spritesDano;

    private void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void RecebeDano(float dano)
    {
        if (vidaAtual > 0.0f && Invuneravel == false)
        {
            vidaAtual -= dano;

            if (vidaAtual > (vidaMaxima / 1.4f) && vidaAtual < vidaMaxima)
            {
                if(spritesDano.Length > 0) GetComponent<SpriteRenderer>().sprite = spritesDano[0];
            }
            else if (vidaAtual > (vidaMaxima / 2f) && vidaAtual < vidaMaxima)
            {
                if (spritesDano.Length >= 1) GetComponent<SpriteRenderer>().sprite = spritesDano[1];
            }
            else if (vidaAtual > (vidaMaxima / 5) && vidaAtual < vidaMaxima)
            {
                if (spritesDano.Length >= 2) GetComponent<SpriteRenderer>().sprite = spritesDano[2];
            }
        }
        else if (Invuneravel == false)
        {

            Instantiate(PrefabExplosao, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

            switch (tipoPontuacao)
            {
                case TipoPontuacao.CyberV1:
                    ScoreManager.instancia.ColetaScore(Pontuacao.ScoreInimigo);
                    LevelManager.instancia.GanhaXP(Pontuacao.XPCyberV1);
                    break;
                case TipoPontuacao.CyberV2:
                    ScoreManager.instancia.ColetaScore(Pontuacao.ScoreInimigo);
                    LevelManager.instancia.GanhaXP(Pontuacao.XPCyberV2);
                    break;
                case TipoPontuacao.CyberV3:
                    ScoreManager.instancia.ColetaScore(Pontuacao.ScoreInimigo);
                    LevelManager.instancia.GanhaXP(Pontuacao.XPCyberV3);
                    break;
                case TipoPontuacao.Item:
                    ScoreManager.instancia.ColetaScore(Pontuacao.ScoreItem);
                    break;
                default:
                    break;
            }
            morreu = true;
            Destroy(this.gameObject);
        }
    }

   public enum TipoPontuacao
    { 
        CyberV1,
        CyberV2,
        CyberV3,
        Item
    }

}
