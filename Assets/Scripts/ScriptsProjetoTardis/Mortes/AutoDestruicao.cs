using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruicao : MonoBehaviour
{

    public float AutoDestruirEm;

    void Start()
    {
        StartCoroutine(AutoDestruir(AutoDestruirEm));
    }

    IEnumerator AutoDestruir(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        Destroy(gameObject);
    }
}
