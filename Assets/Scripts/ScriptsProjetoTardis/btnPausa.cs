using UnityEngine;
using UnityEngine.UI;

public class btnPausa : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            UIManager.instancia.AbrirPainelPause();
            GameManager.instancia.PausarJogo(true);

        });

    }
}
