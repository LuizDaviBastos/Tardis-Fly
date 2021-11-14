using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pnlPausa : MonoBehaviour
{
    public Button btnRetomar, btnConfiguracao, btnIrMenu;

    void Start()
    {
        btnRetomar.onClick.AddListener(() =>
        {
            UIManager.instancia.FecharPainelPause();
            GameManager.instancia.PausarJogo(false);
        });
        btnConfiguracao.onClick.AddListener(() =>
        {
            UIManager.instancia.AbrirPainelConfigracao();
        });
        btnIrMenu.onClick.AddListener(() =>
        {
            GameManager.instancia.IrMenu();
        });

    }

}
