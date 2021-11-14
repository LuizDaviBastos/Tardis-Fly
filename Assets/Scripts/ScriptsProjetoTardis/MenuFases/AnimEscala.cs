using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class AnimEscala : MonoBehaviour
{
    public TMP_Text txt;
    public float duration;
    public float EscalaMaxima, EscalaMinimia;


    public bool UmaVez, AumentaEDiminui;
    public float AtrasoDe;


    private bool trava = true;


    [SerializeField] private float RefEscalaAtual;

    private void Start()
    {
        StartCoroutine(EsperaParaAnimar());
        AtivaTrava();
    }

    IEnumerator EsperaParaAnimar()
    {
        yield return new WaitForSeconds(AtrasoDe);
        AnimUmaVez(txt, EscalaMaxima);
    }

    private void Update()
    {
        if(AumentaEDiminui)
        {
            if (trava)
            {
                if (RefEscalaAtual == EscalaMaxima) AnimUmaVez(txt, EscalaMinimia);
                else if (RefEscalaAtual == EscalaMinimia) AnimUmaVez(txt,EscalaMaxima);
            }
        }
    }

    void AtivaTrava()
    {
        StartCoroutine(Espera());

        IEnumerator Espera()
        {
            yield return new WaitForSeconds(AtrasoDe);
            trava = true;
        }
    }

    void AnimUmaVez(TMP_Text txt, float maxScale)
    {
        if (UmaVez == false) return;
         
        if(txt.rectTransform.localScale.x < maxScale) txt.rectTransform.DOScale(maxScale, duration);
        RefEscalaAtual = txt.rectTransform.localScale.x;
    }

}



