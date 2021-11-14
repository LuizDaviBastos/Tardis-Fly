using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaManager : MeuMonoBehaviour
{
    public float VidaMaxima = 100;
    public float VidaAtual;

    public delegate void VidaDelegate();
    public static event VidaDelegate Morreu;
    private Image img;
    public Animator PainelDano;


    private bool travaEfeitoDano = true, travaAtivaEventoMorte = true;


    public static VidaManager instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
        }

        else Destroy(this.gameObject);

        SceneManager.sceneLoaded += QuandoCarregar;
    }

    void Start()
    {
        VidaAtual = VidaMaxima;
    }

    void Update()
    {
        if (FaseJogavel())
        {
            EfeitosDeDano();

            if (VidaAtual <= 0.0f && travaAtivaEventoMorte)
            {
                travaAtivaEventoMorte = false;
                if (Morreu != null) Morreu();
            }
        }
    }

    void QuandoCarregar(Scene scene, LoadSceneMode load)
    {
        if (FaseJogavel())
        {
            VidaAtual = VidaMaxima;

            var _img = GameObject.FindWithTag("BarraDeVida");
            if (_img != null) img = _img.GetComponent<Image>();
            var pnlD = GameObject.Find("PainelDano");
            if (pnlD != null) PainelDano = pnlD.GetComponent<Animator>();

            travaAtivaEventoMorte = true;
            travaEfeitoDano = true;
        }
    }

    public void PerdeVida(float dano)
    {
        if (VidaAtual > 0.0f)
        {
            VidaAtual -= dano;
            var _dano = (float)(dano / VidaMaxima);
            img.fillAmount -= _dano;

            //TODO - Deixar Vibraçãon do dano mais suave
            Handheld.Vibrate();
        }
    }

    public void GanahVida(float restaura)
    {
        if (VidaAtual < VidaMaxima)
        {
            VidaAtual += restaura;
            var _dano = (float)restaura / VidaMaxima;
            img.fillAmount += _dano;
        }
    }

    public void ReiniciarBarraDeVida()
    {
        img.fillAmount = 1;
        VidaAtual = VidaMaxima;
    }
    
    public void EfeitosDeDano()
    {
        if (FaseJogavel())
        {
            if (VidaAtual < (VidaMaxima / 4) && travaEfeitoDano)
            {
                travaEfeitoDano = false;
                EfeitoPainelDano(EfeitosDano.Pulsante);
                EfeitoAudioDano(EfeitosDano.Pulsante);
            }
            else if (VidaAtual > (VidaMaxima / 4))
            {
                travaEfeitoDano = true;
                EfeitoPainelDano(EfeitosDano.Default);
                EfeitoAudioDano(EfeitosDano.Default);
            }
        }
    }

    public void EfeitoAudioDano(EfeitosDano efeito)
    {
        //TODO - Implementar efeito de audio
    }
    public void EfeitoPainelDano(EfeitosDano efeito)
    {
        if (PainelDano != null)
        {
            switch (efeito)
            {
                case EfeitosDano.Pulsante:
                    PainelDano.Play("AnimTelaDanoPulsa");
                    break;
                case EfeitosDano.Rapido:
                    PainelDano.Play("AnimTelaDano");
                    break;
                case EfeitosDano.Default:
                    PainelDano.Play("AnimTelaDanoIdle");
                    break;
                default:
                    break;
            }
        }
    }


}

public enum EfeitosDano
{
    Pulsante,
    Rapido,
    Default
}
