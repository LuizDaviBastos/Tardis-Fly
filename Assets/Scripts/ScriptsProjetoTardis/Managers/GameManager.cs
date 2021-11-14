using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MeuMonoBehaviour
{
    public delegate void DelegateGame();
    public static event DelegateGame Ganhou;

    public GameObject PreFabTardis;
    public GameObject TardisEmCena;
    public GameObject SpawnManager;

    public bool FaseConcluida = false;
    public bool morte = false;
    public bool testes;



    public string nomeFaseAtual = "";


    public static GameManager instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            //DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);

        SceneManager.sceneLoaded += (scene, load) => { StartGame(); };
        
    }


    private void Start()
    {
        StartGame();
    }

    public void MorteTardis()
    {
        //TODO - Play em animação de morte da tardis
       if(TardisEmCena != null) Destroy(TardisEmCena.gameObject);
    }
    public void NasceTardis()
    {

        var spawnTardis = GameObject.FindWithTag("Respawn");
        if (spawnTardis != null)
        {

            if(this != null) StartCoroutine(EsperaENasce());
            
        }

        IEnumerator EsperaENasce()
        {
            yield return new WaitForSeconds(2f);
            if (GameObject.FindWithTag("Player") == null) TardisEmCena = Instantiate(PreFabTardis, new Vector2(spawnTardis.transform.position.x, spawnTardis.transform.position.y), Quaternion.identity);
        }
    }

    public void StartGame()
    {
        if (FaseJogavel())
        {
            FaseConcluida = false;
            morte = false;
            NasceTardis();
            nomeFaseAtual = SceneManager.GetActiveScene().name;
            VidaManager.Morreu += GameOverGame;
        }
    }

    public void GameOverGame()
    {
        if (FaseConcluida == false)
        {
            morte = true;
            MorteTardis();
            UIManager.instancia.GameOverUI();
        }
    }

    public void GameWinGame()
    {
        if (morte == false)
        {
            FaseConcluida = true;
            UIManager.instancia.GameWinUI();
            Ganhou();

        }
    }

    public void JogarNovamenteGame()
    {
        //TODO - fazer a velha verificação do score pra ver se ganhou ou não antes de jogar novamente
        if (FaseConcluida)
        {
            ScoreManager.instancia.SalvaMoedas();
            ScoreManager.instancia.SalvaScoreFase(OndeEstou.instancia.nomeFaseAtual);
            LevelManager.instancia.SalvaLevel();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        VidaManager.instancia.ReiniciarBarraDeVida();

        FaseConcluida = false;
        morte = false;
    }

    public void ProximaFaseGame()
    {
        //TODO - fazer a velha verificação do score pra ver se ganhou ou não antes de jogar novamente
        if (FaseConcluida)
        {
            ScoreManager.instancia.SalvaMoedas();
            ScoreManager.instancia.SalvaScoreFase(OndeEstou.instancia.nomeFaseAtual);
            LevelManager.instancia.SalvaLevel();

        }

    }

    public void RestartSpawnInimigo()
    {
        SpawnManager.GetComponent<SpawnManager>().Onda = 1;
        SpawnManager.GetComponent<SpawnManager>().estagio1 = true;
        SpawnManager.GetComponent<SpawnManager>().estagio2 = false;
    }

    public void PausarJogo(bool toggle)
    {
        if (toggle) Time.timeScale = 0;
        else Time.timeScale = 1;
    }


    public void IrMenu()
    {
        if (FaseConcluida)
        {
            LevelManager.instancia.SalvaLevel();
            ScoreManager.instancia.SalvaMoedas();
        }

        PausarJogo(false);
        morte = false;
        FaseConcluida = false;
        OndeEstou.instancia.cenaACarregarIndex = Fases.MenuFases;

        
        SceneManager.LoadScene(Fases.Carregando);
    }

    public bool VerificaTardisEmCena()
    {
        return (GameObject.FindWithTag("Player") != null);
    }
}
