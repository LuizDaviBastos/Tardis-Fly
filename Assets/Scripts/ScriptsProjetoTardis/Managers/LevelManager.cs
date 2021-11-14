using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MeuMonoBehaviour
{


    public int LevelAtual = 1;
    public Sprite[] LevelSprites;
    public float ExpAlvo = 100f, ExpAtual = 0, MultiplicadorDeExp = 2.2f;

    public float ExpEmJogo;


    public static LevelManager instancia;

    void Awake()
    {
        ZPlayerPrefs.Initialize(KeyPlayerPrefs.MyPassowrAndLogin, KeyPlayerPrefs.MyPassowrAndLogin);
        
        if (instancia == null)
        {
            instancia = this; 
            //DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);

        SceneManager.sceneLoaded += (scene, load) => { CarregaLevelMenuFases(); };
    }

    private void Start()
    {
        ZPlayerPrefs.Initialize(KeyPlayerPrefs.MyPassowrAndLogin, KeyPlayerPrefs.MyPassowrAndLogin);
        
        PegaLevel();
        CarregaLevelMenuFases();
        
    }

    void CarregaLevelMenuFases()
    {
        ZPlayerPrefs.Initialize(KeyPlayerPrefs.MyPassowrAndLogin, KeyPlayerPrefs.MyPassowrAndLogin);
        PegaLevel();
        if (OndeEstou.instancia.faseAtual.Equals(Fases.MenuFases))
        {
            var txtLevel = GameObject.Find("txtXp");
            if (txtLevel != null) txtLevel.GetComponent<TMP_Text>().text = $"{ExpAtual} / {ExpAlvo}";

            var imgIconeLevel = GameObject.Find("imgLevel");
            if (imgIconeLevel != null) imgIconeLevel.GetComponent<Image>().sprite = LevelSprites[LevelManager.instancia.LevelAtual - 1];

            var imgProgressoXp = GameObject.Find("FilhaimgProgresoXp");
            if (imgProgressoXp != null) imgProgressoXp.GetComponent<Image>().fillAmount = (ExpAtual / ExpAlvo);
        }
    }


    public void GanhaXP(float xp)
    {
        if (ExpAtual < ExpAlvo)
        { 
            ExpEmJogo += xp;
        }

        else if (ExpAtual >= ExpAlvo) AumentaNivel();
    }

    public void AumentaNivel()
    {
        LevelAtual++;
        ExpAlvo *= MultiplicadorDeExp;
        ExpAtual = 0;
    }

    public void SalvaLevel()
    {
        ExpAtual = ExpEmJogo;
        string levelInfo = $"{LevelAtual}|{ExpAlvo}|{ExpAtual}|{MultiplicadorDeExp}";
        ZPlayerPrefs.SetString(KeyPlayerPrefs.ChaveLevelSave, levelInfo);
    }

    public void PegaLevel()
    {
        if (!ZPlayerPrefs.HasKey(KeyPlayerPrefs.ChaveLevelSave)) return;

        string[] levelInfo = ZPlayerPrefs.GetString(KeyPlayerPrefs.ChaveLevelSave).Split('|');

        LevelAtual = int.Parse(levelInfo[0]); ExpAlvo = float.Parse(levelInfo[1]);
        ExpAtual = float.Parse(levelInfo[2]); MultiplicadorDeExp = float.Parse(levelInfo[3]);
    }

    public void ApagarSaves(bool playerPrefs = true, bool zplayerprefs = true)
    {
        if(playerPrefs) PlayerPrefs.DeleteAll();

        if(zplayerprefs)
        {
            ZPlayerPrefs.Initialize(KeyPlayerPrefs.MyPassowrAndLogin, KeyPlayerPrefs.MyPassowrAndLogin);
            ZPlayerPrefs.DeleteAll();
        }
        
    }
}

