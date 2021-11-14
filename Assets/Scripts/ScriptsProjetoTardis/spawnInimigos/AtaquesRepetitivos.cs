using System.Collections;
using UnityEngine;

public class AtaquesRepetitivos : AtaqueManager
{
    public GameObject PreFab;
    public float rateTime;
    public float time;
    public int LimiteInimigos;
    public Animator movimento;
    public bool IsFirst = true;


    void Start()
    {
        if (IsFirst) Spawn(); 
    }

    public void InvokarProximoEm()
    {
        StartCoroutine(Invokar());
        IEnumerator Invokar()
        {
            while (Time.time < (time)) yield return null;

            movimento.enabled = true;
            Spawn();
        }
    }

    void Spawn()
    {
        if (movimento != null && inimigosEmSequencia < LimiteInimigos)
        {
            var instant = Instantiate(PreFab, new Vector2(PosicaoSpawn.position.x, PosicaoSpawn.position.y), Quaternion.identity);
            instant.gameObject.name = $"objectInstant{inimigosEmSequencia}";
            instant.GetComponent<AtaquesRepetitivos>().IsFirst = false;
            instant.GetComponent<AtaquesRepetitivos>().time = Time.time + rateTime;
            instant.GetComponent<AtaquesRepetitivos>().movimento.enabled = false;
            instant.GetComponent<AtaquesRepetitivos>().InvokarProximoEm();

            inimigosEmSequencia++;
        }
        
    }
}
