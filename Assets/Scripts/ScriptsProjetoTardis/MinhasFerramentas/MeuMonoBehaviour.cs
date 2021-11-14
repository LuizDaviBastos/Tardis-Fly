using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeuMonoBehaviour : MonoBehaviour
{
    public int FaseAtual { get { return OndeEstou.instancia.faseAtual; } }

    public int TodasMoedas { get { if (FaseAtual.Equals(Fases.MenuFases)) return ScoreManager.instancia.PegaTodasMoedas(); else return 0; } }

    public bool PlayerIsLive { get { return GameManager.instancia.VerificaTardisEmCena(); } }



    public bool FaseJogavel()
    {
        if (Fases.FasesNaoJogaveis.Contains(SceneManager.GetActiveScene().buildIndex)) return false;
        else return true;
    }

}



