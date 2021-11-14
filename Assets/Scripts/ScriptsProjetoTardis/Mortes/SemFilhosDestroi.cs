using System.Collections;
using UnityEngine;

public class SemFilhosDestroi : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(VerificaFilhos());
        IEnumerator VerificaFilhos()
        {
            while (transform.childCount > 0) yield return null;

            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        /*if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }

        if (gameObject.transform.position.x < -20f || gameObject.transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
        if (gameObject.transform.position.y < -10f || gameObject.transform.position.y > 10f)
        {
            Destroy(gameObject);
        }*/
    }
}

