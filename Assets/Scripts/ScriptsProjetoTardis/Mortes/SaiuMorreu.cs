using System.Collections;
using UnityEngine;

public class SaiuMorreu : MonoBehaviour
{

    private void Start()
    {
        VerificaPosicao();
    }
    private void Awake()
    {
        VerificaPosicao();
    }

    void VerificaPosicao()
    {
        StartCoroutine(await());
        IEnumerator await()
        {
            while (gameObject.transform.position.x > -10f && gameObject.transform.position.x < 15f && gameObject.transform.position.y < 8f && gameObject.transform.position.y > -8f) yield return null;

            Destroy(gameObject);
        }
    }

}
