using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFasesManager : MeuMonoBehaviour
{
    public List<btnNivelFase> cybermans;
    public List<btnNivelFase> daleks;
    public List<btnNivelFase> angels;

    public GameObject painel;
    public GameObject grid;
    public GameObject btnSubFases;

    public TMPro.TMP_Text txtCarregando;

    public Text txtMoedas;


    private List<GameObject> listaAtual = new List<GameObject>();


    private void Awake()
    {
        SceneManager.sceneLoaded += (scene, load) =>
        {
            if (grid == null) grid = GameObject.Find("Grid");
            if (painel == null) painel = GameObject.Find("pnlSubFases");
            if (painel != null) painel.SetActive(false);
        };
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) SceneManager.LoadScene(Fases.MenuInicial);
    }

    void Start()
    {
        ZPlayerPrefs.Initialize(KeyPlayerPrefs.MyPassowrAndLogin, KeyPlayerPrefs.MyPassowrAndLogin);
        txtMoedas.text = TodasMoedas.ToString();
    }

    public void AbrirPainelSubFases()
    {

        AudioManager.instance.PlaySoundFx(TiposAudios.BotaoEnter);

        if (painel != null)
        {
            painel.SetActive(true);
            painel.GetComponent<Animator>().Play("pnlSubFaseAbrindo");
        }
    }

    public void FecharPainelSubFases()
    {
        AudioManager.instance.PlaySoundFx(TiposAudios.btnBack);

        if (listaAtual.Count > 0) listaAtual.ForEach(x => { Destroy(x); });

        painel.GetComponent<Animator>().Play("pnlSubFaseFechandoanim");
        StartCoroutine(EsperaParaDesativar(0.5f));

        IEnumerator EsperaParaDesativar(float time)
        {
            yield return new WaitForSeconds(time);
            painel.SetActive(false);
        }
    }

    public void CybermanFases()
    {
        //Verifica se painel está aberto para nao ter botoes duplicados
        if (painel.activeSelf) return;

        AbrirPainelSubFases();
   
        foreach (var item in cybermans)
        {
            var cenaDesseBtn = Fases.CenaCyber + (cybermans.IndexOf(item) + 1);
            if (ZPlayerPrefs.HasKey(cenaDesseBtn) && ZPlayerPrefs.GetString(cenaDesseBtn).Equals(true.ToString()) || item.desbloqueada == true)
            {
                //Liberando caso nao esteja liberado
                if (ZPlayerPrefs.HasKey(cenaDesseBtn) == false) ZPlayerPrefs.SetString(cenaDesseBtn, true.ToString());
                item.desbloqueada = true;
                ////////\\\\\\\\
            }

            var btn = Instantiate(btnSubFases, grid.transform);
            if (btn != null)
            {
                listaAtual.Add(btn);
                btn.GetComponent<Button>().interactable = item.desbloqueada;
                btn.transform.GetChild(0).GetComponent<Text>().text = (cybermans.IndexOf(item) + 1).ToString();

                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    OndeEstou.instancia.cenaACarregarNome = cenaDesseBtn;
                    //SceneManager.LoadSceneAsync(Fases.Carregando, LoadSceneMode.Single);
                    StartCoroutine(carregando(Fases.Carregando));
                });
            }
        }
    }

    IEnumerator carregando(int fase)
    {
        var ass = SceneManager.LoadSceneAsync(fase);

        while (!ass.isDone)
        {
            if (txtCarregando != null) txtCarregando.text = "Carregando...";

            yield return null;
        }
    }
}
