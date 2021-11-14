using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class pnlConfiguracoes : MonoBehaviour
{
    public Button btnConfigAbrir, btnConfigFechar;
    public Toggle TgAttackModoLivre, TgAttackJoystick, TgAttackDividido, TgRotacionar;


    private void Start()
    {
        if(btnConfigFechar != null)
        {
            btnConfigFechar.onClick.AddListener(() =>
            {
                ConfiguracaoManager.instancia.SalvarConfig();
                UIManager.instancia.FecharPainelConfigracao();
            });
        }
        TgAttackModoLivre.onValueChanged.AddListener((toggle) =>
        {
            ConfiguracaoManager.instancia.ConfigControleModoLivre(toggle);
        });
        TgAttackJoystick.onValueChanged.AddListener((toggle) =>
        {
            ConfiguracaoManager.instancia.ConfigControleJoystick(toggle);
        });
        TgAttackDividido.onValueChanged.AddListener((toggle) =>
        {
            ConfiguracaoManager.instancia.ConfigControleDividido(toggle);
        });

        TgRotacionar.onValueChanged.AddListener((toggle) =>
        {
            ConfiguracaoManager.instancia.ConfigControleRotaciona(toggle);
        });

        CarregaConfigs();
    }

    void CarregaConfigs()
    {
        var listConfig = ConfiguracaoManager.instancia.PegarConfig();

        if (listConfig != null)
        {
            TgAttackModoLivre.isOn = listConfig[0];
            TgAttackJoystick.isOn = listConfig[1];
        }
    }
}

