using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{
    public int faseAtual = -1;
    public string nomeFaseAtual = "";
    public int cenaACarregarIndex = -1;
    public string cenaACarregarNome = "";


    //Ajustes da camera
    [SerializeField]
    private float aspect = 1.75f;
    //orthoSize nada mais é que o tamanho da area que a camera pegará.
    [SerializeField]
    private float orthoSize = 5f;
    ///////////\\\\\\\\\\\\
    

    public static OndeEstou instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);

        SceneManager.sceneLoaded += QuandoCarregar;
        faseAtual = SceneManager.GetActiveScene().buildIndex;
    }

    void QuandoCarregar(Scene scene, LoadSceneMode load)
    {
        faseAtual = SceneManager.GetActiveScene().buildIndex;
        nomeFaseAtual = SceneManager.GetActiveScene().name;


        //Ajuste da camera
        if (Fases.FasesNaoJogaveis.Contains(faseAtual) == false)
        {
            float resultadoOrthoSize = (orthoSize * aspect);
            Camera.main.projectionMatrix = Matrix4x4.Ortho(-resultadoOrthoSize, resultadoOrthoSize, -orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
        }
        
    }

}
