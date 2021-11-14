using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void Awake()
    {
        GetComponent<Animator>().enabled = false;
    }

    private void Start()
    {
        StartCoroutine(EsperaParaAnimar());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //TODO - Som da moeda
            ScoreManager.instancia.ColetaMoedas(Pontuacao.Moeda);
            Destroy(this.gameObject);
        }
    }


    IEnumerator EsperaParaAnimar()
    {
        yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        GetComponent<Animator>().enabled = true;

    }

}
