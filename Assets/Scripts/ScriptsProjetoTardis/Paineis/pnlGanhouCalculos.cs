using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pnlGanhouCalculos : MonoBehaviour, IPointerDownHandler
{
    public delegate void AumentaDelegate(float ExpEmJogo, float ExpAlvo);
    public AumentaDelegate aumenta;

    
    public TextMeshProUGUI txtXP, txtScore, txtMoeda;
    public Image ImageXp, imgIconeLevel;

    public Coroutine coroutineXp, coroutineScore, coroutineMoeda;

    private bool travaAumentaDeUmaVez = true;
    private bool travaAumentaGradualmente = true;

    void Start()
    {
        GameManager.Ganhou += AumentaGradualmente;
    }

    void AumentaGradualmente()
    {
        
        if (travaAumentaGradualmente)
        {
            AudioManager.instance.PlaySoundFx(TiposAudios.XPInicio, true);

            travaAumentaGradualmente = false;

            LevelManager.instancia.PegaLevel();
            aumenta = AumentaEPassaNivel;
            if (imgIconeLevel != null) imgIconeLevel.sprite = LevelManager.instancia.LevelSprites[LevelManager.instancia.LevelAtual - 1];
            if(this != null) AumentaEPassaNivel(LevelManager.instancia.ExpEmJogo + LevelManager.instancia.ExpAtual, LevelManager.instancia.ExpAlvo);

            //Score
            if (this != null) coroutineScore = StartCoroutine(txtScore.AumentaGradualmente(':', 0, ScoreManager.instancia.score));
            if (this != null) coroutineMoeda = StartCoroutine(txtMoeda.AumentaGradualmente(0, ScoreManager.instancia.moedas));
        }
    }

    void AumentaEPassaNivel(float ExpEmJogo, float ExpAlvo)
    {
        if (ExpEmJogo >= ExpAlvo)
        {
            coroutineXp = StartCoroutine(txtXP.AumentaXpGraduamente(aumenta, LevelManager.instancia.ExpAtual, LevelManager.instancia.ExpAlvo, imgIconeLevel, ImageXp, LevelManager.instancia.ExpAlvo));
        }
        else
        {
            coroutineXp = StartCoroutine(txtXP.AumentaXpGraduamente(aumenta, LevelManager.instancia.ExpAtual, LevelManager.instancia.ExpEmJogo + LevelManager.instancia.ExpAtual, imgIconeLevel, ImageXp, LevelManager.instancia.ExpAlvo));
        }
    }

    public void AumentaDeUmaVez()
    {
        CancelarCorrotina();

        if (travaAumentaDeUmaVez)
        {
            travaAumentaDeUmaVez = false;
            //Resolvendo XP
            LevelManager.instancia.ExpEmJogo += LevelManager.instancia.ExpAtual;
            while (LevelManager.instancia.ExpEmJogo >= LevelManager.instancia.ExpAlvo)
            {
                LevelManager.instancia.ExpEmJogo = LevelManager.instancia.ExpEmJogo - LevelManager.instancia.ExpAlvo;
                LevelManager.instancia.AumentaNivel();
            }

            if (imgIconeLevel != null) imgIconeLevel.sprite = LevelManager.instancia.LevelSprites[LevelManager.instancia.LevelAtual - 1];
            if (ImageXp != null) ImageXp.fillAmount = LevelManager.instancia.ExpEmJogo / LevelManager.instancia.ExpAlvo;
            if (txtXP != null) txtXP.text = $"{LevelManager.instancia.ExpEmJogo.ToString("0")} / {LevelManager.instancia.ExpAlvo.ToString("0")}";

            //Resolvendo Score
            if (txtScore != null) txtScore.text = $"{txtScore.text.Split(':')[0]}: {ScoreManager.instancia.score}";

            //resolve Moeda
            if(txtMoeda != null) txtMoeda.text = ScoreManager.instancia.moedas.ToString();
            
        }

    }

    private void CancelarCorrotina()
    {
        if (coroutineXp != null && this != null) StopCoroutine(coroutineXp);
        if (coroutineScore != null && this != null) StopCoroutine(coroutineScore);
        if (coroutineMoeda != null && this != null) StopCoroutine(coroutineMoeda);
    }

    private int _countTouch = 1;
    public void OnPointerDown(PointerEventData eventData)
    {
        _countTouch++;
        if (_countTouch >= 2) AumentaDeUmaVez();

    }
}
