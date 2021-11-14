using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CenaCarregando : MonoBehaviour
{
    public Text txtCarregando;

    void Start()
    {
        CarregaCena();
    }

    
    public void CarregaCena()
    {
        StartCoroutine(Carregando());
        IEnumerator Carregando()
        {
            AsyncOperation result;
            if (OndeEstou.instancia.cenaACarregarIndex != -1)
            {
                result = SceneManager.LoadSceneAsync(OndeEstou.instancia.cenaACarregarIndex);
                OndeEstou.instancia.cenaACarregarIndex = -1;
            }
            else
            {
                result = SceneManager.LoadSceneAsync(OndeEstou.instancia.cenaACarregarNome);
                OndeEstou.instancia.cenaACarregarNome = "";
            }

            while (!result.isDone)
            {
                if (txtCarregando != null) txtCarregando.text = "Carregando...";
                yield return null;
            }

            
            
        }
        
    }
}
