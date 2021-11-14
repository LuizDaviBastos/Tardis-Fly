using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MeuMonoBehaviour
{
    public GameObject barraVida;
    public Text txtCoin, txtScore;
    public GameObject pnlPerdeu, pnlGanhou, pnlPausa, pnlConfig, pnlStartGame;
    

    public static UIManager instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
            //DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);

        SceneManager.sceneLoaded += QuandoCarregarCena;
    }

    void QuandoCarregarCena(Scene scene, LoadSceneMode load)
    {
        StartUI();
        
    }

    void Start()
    {
        //Não colocar o método "QuandoCarregarCena()" em "Start()" pois com isso occorre bugs de referência tendo mais de uma instância do mesmo tipo em jogo
    }

    //sub rotinas
    void EsperaEDesativaPainel(GameObject panel)
    {
        StartCoroutine(desativarEm(0.5f));
        IEnumerator desativarEm(float time)
        {
            yield return new WaitForSeconds(time);
            if (panel != null) panel.SetActive(false);
        }
    }
    
    //////////////////////\\\\\\\\\\\\\\\\\\\\
    


    public void StartUI()
    {
        if (FaseJogavel())
        {
            ScoreManager.ScoreModificado += UpdateUI;
            PegarDados();
            MostrarPainelStartGame();

           
        }
    }


    public void UpdateUI()
    {
        if (this != null && txtScore != null) StartCoroutine(txtScore.AumentaGradualmente(float.Parse(txtScore.text), ScoreManager.instancia.score, 0, 0.08f, 0.5f));
        if (this != null && txtCoin != null) StartCoroutine(txtCoin.AumentaGradualmente(float.Parse(txtCoin.text), ScoreManager.instancia.moedas, 0, 0.08f, 0.5f));

    }

    public void PegarDados()
    {

        if (FaseJogavel())
        {
            if (pnlGanhou == null) pnlGanhou = GameObject.Find("pnlGanhou");
            if (pnlPerdeu == null) pnlPerdeu = GameObject.Find("pnlPerdeu");
            if (pnlPausa == null) pnlPausa = GameObject.Find("pnlPausa");
            if (pnlConfig == null) pnlConfig = GameObject.Find("pnlConfig");
            if (pnlStartGame == null) pnlStartGame = GameObject.Find("pnlStartGame");

            if (pnlGanhou != null && pnlPerdeu != null && pnlPausa != null && pnlConfig != null && pnlStartGame != null)
            {
                if (this != null) StartCoroutine(EsperaEDesativaPaineis());
                IEnumerator EsperaEDesativaPaineis()
                {
                    yield return new WaitForSeconds(0.01f);
                    pnlGanhou.SetActive(false);
                    pnlPerdeu.SetActive(false);
                    pnlPausa.SetActive(false);
                    pnlConfig.SetActive(false);
                }
            }

            if (txtCoin == null && GameObject.FindWithTag("txtCoin") != null) txtCoin = GameObject.FindWithTag("txtCoin").GetComponent<Text>();
            if (txtScore == null && GameObject.FindWithTag("txtScore") != null) txtScore = GameObject.FindWithTag("txtScore").GetComponent<Text>();
            if (GameObject.Find("BarraVida") != null) barraVida = GameObject.Find("BarraVida");
        }

    }
    
    
    public void GameOverUI()
    {
        if (FaseJogavel())
        {
            pnlPerdeu.SetActive(true);
            pnlPerdeu.GetComponent<Animator>().Play("MostraPainel");
        }
        
    }

    
    public void GameWinUI()
    {
        if (FaseJogavel())
        {
            pnlGanhou.SetActive(true);
            pnlGanhou.GetComponent<Animator>().Play("MostraPainel");
        }
    }


    public void JogarNovamenteUI()
    {

        PegarDados();
        if (pnlGanhou.activeSelf == true) pnlGanhou.GetComponent<Animator>().Play("EscondePainel");

        var imgManager = barraVida.GetComponent<BarraDeVida>();
        imgManager.ReiniciarBarraDeVida();
        txtCoin.text = "0";
        txtScore.text = "0";
    }

    public void ProximaFaseUI()
    {

        //TODO - Ver oq é necessário para fazer no UIManager ao apertar botao de proxima fase
    }

    public void AbrirPainelPause()
    {
        pnlPausa.SetActive(true);
        pnlPausa.GetComponent<Animator>().Play("AbrirPainel");
    }

    public void FecharPainelPause()
    {
        pnlPausa.GetComponent<Animator>().Play("FecharPainel");

        EsperaEDesativaPainel(pnlPausa);
    }

    public void AbrirPainelConfigracao()
    {
        pnlConfig.SetActive(true);
        pnlConfig.GetComponent<Animator>().Play("pnlConfigAbrindo");
    }

    public void FecharPainelConfigracao()
    {
        pnlConfig.GetComponent<Animator>().Play("pnlConfigFechando");

        EsperaEDesativaPainel(pnlConfig);
    }

    public void MostrarPainelStartGame()
    {
        switch (GameManager.instancia.nomeFaseAtual.Split('-')[0] + "-")
        {
            case Fases.CenaCyber:
                if (pnlStartGame != null)
                {
                    pnlStartGame.SetActive(true);
                    var text = pnlStartGame.GetComponentInChildren<TMP_Text>();
                    text.text = $"cybermans - {GameManager.instancia.nomeFaseAtual.Split('-')[1]}";
                }
                break;
            default:
                break;
        }

        if (pnlStartGame != null)
        {
            pnlStartGame.GetComponent<Animator>().Play("AnimPnlStartGame");

            var duracao = Time.time + pnlStartGame.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            if(this != null) StartCoroutine(EsperaAnimacaoTerminar());
            IEnumerator EsperaAnimacaoTerminar()
            {
                while (Time.time < duracao) yield return null;

                DesativarPainelStartGame();
            }
        }
    }


    public void DesativarPainelStartGame()
    {
        InimigoManager.instancia.trava = true;
        if(pnlStartGame != null)
        {
            pnlStartGame.SetActive(false);
        }
    }

}
