using System.Collections.Generic;
using UnityEngine;

public class InimigoManager : Estage
{
    public bool ProximoAtaque;
    public bool AtaqueEmAndamento;
    public int Ataque;

    public int inimigosEmCena;

    public bool trava = false;

    public List<Ataques> inimigos = new List<Ataques>();

    public Estage estage;

    public int inimigosEmSequenciaManager = 0;


    public static InimigoManager instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }

        else Destroy(this.gameObject);

    }


    private void Update()
    {
        if (FaseJogavel())
        {
          /*TODO - Verifica se esta na fase 1*/   AtaquesNivel1();
        }
    }

    private void FixedUpdate()
    {
        if (FaseJogavel())
        {
            VerificaInimigosEmCena();
        }
    }

    public void VerificaInimigosEmCena()
    {
        inimigosEmCena = GameObject.FindGameObjectsWithTag("inimigo").Length;
    }

}

[System.Serializable]
public class Ataques
{
    public GameObject Prefab;
    public bool isSubAtaque;
    public float RateTime;
    public List<Ataques> SubAtaques;
}


