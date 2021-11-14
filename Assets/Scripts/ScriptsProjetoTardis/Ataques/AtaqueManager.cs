using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtaqueManager : MonoBehaviour
{
    public bool trava { get { return InimigoManager.instancia.trava; } set { InimigoManager.instancia.trava = value; } }
    public bool TudoLimpo { get { return (InimigoManager.instancia.inimigosEmCena < 1); } }
    public int AtaqueAtual { get { return InimigoManager.instancia.Ataque; } }
    public List<Ataques> Inimigos { get { return InimigoManager.instancia.inimigos; } }
    public Transform PosicaoSpawn { get { if (InimigoManager.instancia != null && InimigoManager.instancia.gameObject != null) return InimigoManager.instancia.gameObject.transform; else return null; } }
    public int inimigosEmSequencia { get { return InimigoManager.instancia.inimigosEmSequenciaManager; } set { InimigoManager.instancia.inimigosEmSequenciaManager = value; } }


    public void AguardeMatarTodos()
    {
        StartCoroutine(AguardaInstanciar());

        IEnumerator AguardaInstanciar()
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Espera());
        }

        IEnumerator Espera()
        {
            while (TudoLimpo == false)
            {
                yield return null;
            }

            InimigoManager.instancia.inimigosEmSequenciaManager = 0;
            InimigoManager.instancia.Ataque++;
            trava = true;
        }
    }

    public int FaseAtual
    {
        get { return OndeEstou.instancia.faseAtual; }
    }

    public int TodasMoedas
    {
        get
        {
            if (FaseAtual.Equals(Fases.MenuFases))
            {
                return ScoreManager.instancia.PegaTodasMoedas();
            }
            else return 0;
        }
    }

    public bool FaseJogavel()
    {
        if (Fases.FasesNaoJogaveis.Contains(SceneManager.GetActiveScene().buildIndex)) return false;
        else return true;
    }

}

