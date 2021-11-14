using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MenuInicial : MonoBehaviour
{
    public Button btnJogar;
    public TMP_Text txtCarregando;
    public TMP_Text txtTitulo;

    public float poy;
    public float posAlvo;

    public GameObject Config;

    private bool trava = false;

    void Start()
    {
        

        btnJogar.onClick.AddListener(() =>
        {
            StartCoroutine(carregando());
            AudioManager.instance.PlaySoundFx(TiposAudios.Alavanca);
        });


        AtivaTrava();

        IEnumerator carregando()
        {
            var ass = SceneManager.LoadSceneAsync(Fases.MenuFases);

            while (!ass.isDone)
            {
                txtCarregando.text = "Carregando...";
                yield return null;
            }
        }
    }


    private void Update()
    {

        btnJogar.transform.GetChild(0).GetComponent<TMP_Text>().color = btnJogar.gameObject.GetComponent<Image>().color;
        if(trava)
        {
            if (btnJogar.transform.localScale.x == 2) btnJogar.transform.DOScale(1.7f, 1.6f);
            else if (btnJogar.transform.localScale.x == 1.7f) btnJogar.transform.DOScale(2, 1.6f);
        }

        btnJogar.interactable = trava;
    }

    void AtivaTrava()
    {
        StartCoroutine(Espera());

        IEnumerator Espera()
        {
            yield return new WaitForSeconds(btnJogar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            trava = true;
        }


    }
}
