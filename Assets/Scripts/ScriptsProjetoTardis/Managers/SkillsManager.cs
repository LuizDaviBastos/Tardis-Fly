using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    //Controle Skills
    public bool SkillTeletransporte;
    public bool SkillEscudo;
    private GameObject TardisEmCena;

    //////////////\\\\\\\\\\\\\\

    /*Configuracoes Skills*/

    //Teletransporte
    public GameObject tardisFantasma;
    public GameObject cloneTardis;
    private float controleTempoTeleporte = 0;
    private Text tempTeleporteUI;
    public int DuracaoTeleporte = 3;
    public float tempoParaTeleporteVoltar = 20;
    public float tempoTeletransporte = 0;
    private bool TeletransporteUsado = false;

    //Escudo
    private Text tempEscudoUI;
    public float DuracaoEscudo = 8f;
    public float tempoParaEscudoVoltar = 20f;
    public float tempoEscudo = 0;
    private bool EscudoUsado = false;
    //////////////\\\\\\\\\\\\\\


    private bool Temporizar = false;

    public static SkillsManager instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
            //DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);

        SceneManager.sceneLoaded += QuandoCarregar;
    }

    void QuandoCarregar(Scene scene, LoadSceneMode load)
    {
        if (Fases.FasesNaoJogaveis.Contains(OndeEstou.instancia.faseAtual)) return;
        /*if (OndeEstou.instancia.faseAtual != Fases.MenuFases && OndeEstou.instancia.faseAtual != Fases.MenuInicial && OndeEstou.instancia.faseAtual != Fases.Carregando)
        {*/
        var txtCntg = GameObject.Find("txtContagemEscudo");
        if (txtCntg != null) tempEscudoUI = txtCntg.GetComponent<Text>();

        var txtTp = GameObject.Find("txtTpContagem");
        if (txtTp != null) tempTeleporteUI = txtTp.GetComponent<Text>();
        // }
    }

    private void Update()
    {
        TemporizadorSkills();
    }

    public void AtualizaSkills()
    {
        if (ZPlayerPrefs.HasKey(ChaveSkills.Teletransporte))
        {
            SkillTeletransporte = Convert.ToBoolean(ZPlayerPrefs.GetString(ChaveSkills.Teletransporte));
        }
        if (ZPlayerPrefs.HasKey(ChaveSkills.Escudo))
        {
            SkillEscudo = Convert.ToBoolean(ZPlayerPrefs.GetString(ChaveSkills.Escudo));
        }
    }

    public void SalvaSkill(string chave, bool value)
    {
        ZPlayerPrefs.SetString(chave, value.ToString());
        AtualizaSkills();
    }


    public void Skill_Escudo()
    {
        if (/*TODO - Criar loja para comprar skill escudo - SkillEscudo == false*/ EscudoUsado) return;
        if (TardisEmCena == null) TardisEmCena = GameObject.FindWithTag("Player");
        if (tempEscudoUI == null) tempEscudoUI = GameObject.Find("txtContagemEscudo").GetComponent<Text>();
        var escudo = TardisEmCena.transform.GetChild(3);

        escudo.gameObject.SetActive(true);
        TardisEmCena.GetComponent<Player>().invuneravel = true;
        //Começa contagem para desativar escudo
        StartCoroutine(ContagemDesativaEscudo());

        IEnumerator ContagemDesativaEscudo()
        {
            yield return new WaitForSeconds(DuracaoEscudo);
            escudo.gameObject.SetActive(false);

            TardisEmCena.GetComponent<Player>().invuneravel = false;

            EscudoUsado = true;
            tempoEscudo = tempoParaEscudoVoltar;
        }
    }

    public void SkillTeleport()
    {
        if (/*TODO - Criar loja SkillTeletransporte == false && */tempoTeletransporte > 0) return;

        //Pega Tardis e TextUI
        if (TardisEmCena == null) TardisEmCena = GameObject.FindWithTag("Player");
        if (tempTeleporteUI == null) tempTeleporteUI = GameObject.Find("txtTpContagem").GetComponent<Text>();

        //Define Local
        if (GameObject.Find("TardisFantasma(Clone)") == null)
        {
            cloneTardis = Instantiate(tardisFantasma, TardisEmCena.transform);
            cloneTardis.transform.position = TardisEmCena.transform.position;
            TardisEmCena.GetComponent<Player>().invuneravel = true;
        }

        //Contagem para teletransportar
        StartCoroutine(Teleport());

        Time.timeScale = 0;

        IEnumerator Teleport()
        {
            Temporizar = true;
            yield return new WaitForSecondsRealtime(DuracaoTeleporte);

            //Desativando movimento do fantasma tardis
            CloneTardis.instancia.podeMover = false;

            TardisEmCena.GetComponent<Animator>().Play("TardisDesmaterializa");
            Time.timeScale = 1;
            Temporizar = false;
            controleTempoTeleporte = 0;
            tempTeleporteUI.text = "";

            //Configurando quando poderá usar o teletransporte na próxima vez
            tempoTeletransporte = tempoParaTeleporteVoltar;
            TeletransporteUsado = true;
        }

    }

    void TemporizadorSkills()
    {
        //Tempo para usar teletransporte novamente
        if (TeletransporteUsado)
        {
            if (tempoTeletransporte > 0f)
            {
                tempoTeletransporte -= Time.deltaTime;
                tempTeleporteUI.text = tempoTeletransporte.ToString("0");
            }
            else if (tempoTeletransporte <= 0f)
            {
                TeletransporteUsado = false;
                tempTeleporteUI.text = "";
            }
        }

        //Tempo para definir posição do teletransporte
        if (Temporizar == true)
        {
            if (controleTempoTeleporte < DuracaoTeleporte)
            {
                controleTempoTeleporte += Time.unscaledDeltaTime;
                tempTeleporteUI.text = controleTempoTeleporte.ToString("0");
            }
        }

        //Tempo para usar escudo novamente
        if (EscudoUsado)
        {
            if (tempoEscudo > 0f)
            {
                tempoEscudo -= Time.deltaTime;
                tempEscudoUI.text = tempoEscudo.ToString("0");
            }
            else if (tempoEscudo <= 0f)
            {
                EscudoUsado = false;
                if (tempTeleporteUI != null) tempTeleporteUI.text = "";
            }
        }
    }
}
