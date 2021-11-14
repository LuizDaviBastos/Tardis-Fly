using UnityEngine;
using UnityEngine.UI;

public class PainelGanhouEPerdeu : MonoBehaviour
{

    public Button btnIrMenu, btnJogarNovamente, btnProximaFase;
    
    private void Awake()
    {
        this.gameObject.SetActive(true);

        if(btnIrMenu != null)
        {
            btnIrMenu.onClick.AddListener(() =>
            {
                if(GameManager.instancia.FaseConcluida) GetComponent<pnlGanhouCalculos>().AumentaDeUmaVez();
                GameManager.instancia.IrMenu();
            });
        }
        if(btnJogarNovamente != null)
        {
            btnJogarNovamente.onClick.AddListener(() =>
            {
                GameManager.instancia.JogarNovamenteGame();
            });
        }
        if(btnProximaFase != null)
        {
            btnProximaFase.onClick.AddListener(() =>
            {
                GetComponent<pnlGanhouCalculos>().AumentaDeUmaVez();
                GameManager.instancia.ProximaFaseGame();
            });
        }
        
    }

    private void Start()
    {
        this.gameObject.SetActive(true);
    }

}
