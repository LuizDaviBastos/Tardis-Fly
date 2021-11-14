public class ScoreManager : MeuMonoBehaviour
{
    public delegate void DelegateScore();
    public static event DelegateScore ScoreModificado;

    public int moedas = 0;
    public float score = 0;

    public static ScoreManager instancia;


    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        ZPlayerPrefs.Initialize(KeyPlayerPrefs.MyPassowrAndLogin, KeyPlayerPrefs.MyPassowrAndLogin);

        if (FaseAtual.Equals(Fases.MenuFases))
        {
            //TODO - Carregar todas as moedas e Score de cada fase
        }


    }

    public void ZeraPontos()
    {
        moedas = 0;
        score = 0;
    }

    public int GetScoreFase(string faseNome)
    {
        if (ZPlayerPrefs.HasKey($"{KeyPlayerPrefs.ScoreFase}{faseNome}"))
        {
            return ZPlayerPrefs.GetInt($"{KeyPlayerPrefs.ScoreFase}{faseNome}");
        }
        else
        {
            ZPlayerPrefs.SetInt($"{KeyPlayerPrefs.ScoreFase}{faseNome}", 0);
            return GetScoreFase(faseNome);
        }
    }

    public void ColetaScore(int _score)
    {
        score += _score;
        if(ScoreModificado != null) ScoreModificado();
    }

    public void SalvaScoreFase(string faseNome)
    {
        ZPlayerPrefs.SetFloat($"{KeyPlayerPrefs.ScoreFase}{faseNome}", score);
    }


    public void ResgatarTodasMoedas(out int _moedas)
    {
        moedas = ZPlayerPrefs.GetInt(KeyPlayerPrefs.Moedas);
        _moedas = moedas;
    }

    public int PegaTodasMoedas()
    {
        if (ZPlayerPrefs.HasKey(KeyPlayerPrefs.Moedas)) return ZPlayerPrefs.GetInt(KeyPlayerPrefs.Moedas);
        else return 0;
    }

    public void ColetaMoedas(int coin)
    {
        moedas += coin;
        if (ScoreModificado != null) ScoreModificado();
    }

    public void PerdeMoedas(int coin)
    {
        moedas -= coin;
        if (ScoreModificado != null) ScoreModificado();
    }

    public void SalvaMoedas()
    {
        ZPlayerPrefs.SetInt(KeyPlayerPrefs.Moedas, moedas);
    }
}

public class ScorePorFase
{
    public int FaseIndex;
    public float Score;
}

