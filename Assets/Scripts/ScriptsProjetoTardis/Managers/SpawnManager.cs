using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
        }

        else Destroy(this.gameObject);
    }

    public GameObject LimitePosicao;
    
    public List<Prefabs> prefabs = new List<Prefabs>();
    public List<Spawns> spawns = new List<Spawns>();

    //OrdemEstagios
    public bool estagio1 = false;
    public bool estagio2 = false;
    public bool estagio3 = false;
    public bool estagio4 = false;
    public bool estagio5 = false;
    public bool estagio6 = false;
    ////////////////\\\\\\\\\\\\\\\\\

    public int Onda = 1;
    public bool travaOnda = true;
    
    public void ComecarOuResetar()
    {
        estagio1 = true;
        Onda = 1;
        travaOnda = true;
}

    private void FixedUpdate()
    {
        if (estagio1)
        {
            Estagio1.PrimeiraOnda(ref Onda, spawns, prefabs, ref travaOnda);
            Estagio1.SegundaOnda(ref Onda, spawns, prefabs, ref travaOnda);
            Estagio1.TerceiraOnda(ref Onda, spawns, prefabs, ref travaOnda);
            Estagio1.QuartaOnda(ref Onda, spawns, prefabs, ref travaOnda);
            Estagio1.QuintaOnda(ref Onda, spawns, prefabs, ref travaOnda, ref estagio1, ref estagio2);
        }

        if (estagio2)
        {
            Estagio2.PrimeiraOnda(ref Onda, spawns, prefabs, ref travaOnda);
            Estagio2.SegundaOnda(ref Onda, spawns, prefabs, ref travaOnda);
            Estagio2.TerceiraOnda(ref Onda, spawns, prefabs, ref travaOnda);
        }
    }
}

[System.Serializable]
public class Prefabs
{
    public NomesInimigos Nome;
    public GameObject Prefab;
}

[System.Serializable]
public class Spawns
{
    public NomesSpawn Nome;
    public GameObject Spawn;
}

public enum NomesInimigos
{
    CybermanV1,
    CybermanV2,
    CybermanV2_1,

    CyberMarchaV1_1,
    CyberMarchaV1_2,
    CyberMarchaV1_3,
    CyberMarchaV1_4,

    CyberMarchaV2_1,
    CyberMarchaV2_2,
    CyberMarchaV2_3,

}

public enum NomesSpawn
{
    SpawnCima,
    SpawnMeio,
    SpawnBaixo,
    SpawnBaixo2,
    SpawnCima2
}

[System.Serializable]
public class PrefabsItens
{
    public NomesItens ItemNome;
    public GameObject Prefab;
}

public enum NomesItens
{
    Meteoros,

}
