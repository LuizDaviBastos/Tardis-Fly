using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Estagio1
{  
    public static void PrimeiraOnda(ref int Onda, List<Spawns> spawns,List<Prefabs> prefabs, ref bool travaOnda)
    {
        if (Onda.Equals(1))
        {
            var spawn = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnMeio));
            var spawna = spawn.Spawn.GetComponent<SpawnaInimigos>();

            if (Onda != -1 && travaOnda)
            {
                
                spawn.Spawn.SetActive(true);
                spawna.enabled = true;
                spawna.Resetar();

                spawna._limitarInstancia = false;

                spawna.alcanceMaximo = 90;
                spawna.alcanceMinimo = 90;

                spawna.rangeEmYMin = 0;
                spawna.rangeEmYMax = 0;
                spawna.SetSpawnEmSequencia(prefabs.Find(x => x.Nome.Equals(NomesInimigos.CyberMarchaV1_1)), false, -85, 1);

                //trava
                travaOnda = false;
            }
            if (GameObject.FindWithTag("inimigo") == null && spawna.inimigosEmCena >= 1 && Onda.Equals(1))
            {
                Onda = 2;
                travaOnda = true;
            }
        }

      

    }

    public static void SegundaOnda(ref int Onda, List<Spawns> spawns, List<Prefabs> prefabs, ref bool travaOnda)
    {
        if (Onda.Equals(2))
        {
            var spawn = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnMeio));
            var spawna = spawn.Spawn.GetComponent<SpawnaInimigos>();

            if (Onda != -1 && travaOnda)
            {
                //trava
                travaOnda = false;

                spawna.Resetar();

                spawn.Spawn.SetActive(true);

                spawna._limitarInstancia = false;

                spawna.enabled = true;

                spawna.alcanceMaximo = 90;
                spawna.alcanceMinimo = 90;

                spawna.rangeEmYMin = 0;
                spawna.rangeEmYMax = 0;
                spawna.SetSpawnEmSequencia(prefabs.Find(x => x.Nome.Equals(NomesInimigos.CyberMarchaV1_2)), false, -75, 2, 5f);


            }

            if (GameObject.FindWithTag("inimigo") == null && spawna.inimigosEmCena >= 2 && Onda.Equals(2))
            {
                Onda = 3;
                travaOnda = true;
            }
        }
        
    }

    public static void TerceiraOnda(ref int Onda, List<Spawns> spawns, List<Prefabs> prefabs, ref bool travaOnda)
    {
        if (Onda.Equals(3))
        {
            var spawnCima = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnCima));
            var spawnBaixo = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnBaixo));
            var spawnMeio = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnMeio));

            var spawnaCima = spawnCima.Spawn.GetComponent<SpawnaInimigos>();
            var spawnaBaixo = spawnBaixo.Spawn.GetComponent<SpawnaInimigos>();
            var spawnaMeio = spawnMeio.Spawn.GetComponent<SpawnaInimigos>();

            if (Onda != -1 && travaOnda)
            {
                //trava
                travaOnda = false;
                spawnaCima.Resetar();
                spawnaBaixo.Resetar();
                spawnaMeio.Resetar();

                //Set SpawnCima
                spawnCima.Spawn.SetActive(true);
                spawnaCima._limitarInstancia = false;
                spawnaCima.enabled = true;
                spawnaCima.SetRotation(130);
                spawnaCima.alcanceMaximo = 110;
                spawnaCima.alcanceMinimo = 110;
                spawnaCima.rangeEmYMin = 0;
                spawnaCima.rangeEmYMax = 0;
                spawnaCima.VelocidadeMovimentoSpawn = 2.5f;
                spawnaCima.SetSpawnEmSequencia(prefabs.Find(x => x.Nome.Equals(NomesInimigos.CybermanV1)), true, -90, 10, 0.5f);

                //Set SpawnBaixo
                spawnBaixo.Spawn.SetActive(true);
                spawnaBaixo._limitarInstancia = false;
                spawnaBaixo.enabled = true;
                spawnaBaixo.SetRotation(50);
                spawnaBaixo.alcanceMaximo = 70;
                spawnaBaixo.alcanceMinimo = 70;
                spawnaBaixo.rangeEmYMin = 0;
                spawnaBaixo.rangeEmYMax = 0;
                spawnaBaixo.VelocidadeMovimentoSpawn = 2.5f;
                spawnaBaixo.SetSpawnEmSequencia(prefabs.Find(x => x.Nome.Equals(NomesInimigos.CybermanV1)), true, -90, 10, 0.5f);

                //Set SpawnMeio
                
                spawnMeio.Spawn.SetActive(true);
                spawnaMeio.enabled = true;

                spawnaMeio.alcanceMaximo = 90;
                spawnaMeio.alcanceMinimo = 90;

                spawnaMeio.rangeEmYMin = 0;
                spawnaMeio.rangeEmYMax = 0;
                spawnaMeio.SetSpawnEmSequencia(prefabs.Find(x => x.Nome.Equals(NomesInimigos.CybermanV2_1)), false, -60, 1, 1f)
                    .SetLimiteDistancia(GameObject.Find("limiteCyber2.0").transform.position, x: true, y: false);

            }

            if (GameObject.FindWithTag("inimigo") == null && spawnaCima.inimigosEmCena >= 2 && Onda.Equals(3))
            {
                Onda = 4;
                travaOnda = true;
            }
        }
    }

    public static void QuartaOnda(ref int Onda, List<Spawns> spawns, List<Prefabs> prefabs, ref bool travaOnda)
    {
        var spawnMeio = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnMeio));
        var spawnaMeio = spawnMeio.Spawn.GetComponent<SpawnaInimigos>();
        
        if (Onda.Equals(4))
        {
            
            if (Onda != -1 && travaOnda)
            {
                travaOnda = false;
                spawnaMeio.Resetar();

                spawnMeio.Spawn.SetActive(true);
                spawnaMeio.enabled = true;

                spawnaMeio.alcanceMaximo = 90;
                spawnaMeio.alcanceMinimo = 90;

                spawnaMeio.SetSpawnEmSequencia(prefabs.Find(x => x.Nome.Equals(NomesInimigos.CyberMarchaV2_1)), false, -40, 1, 0f)
                            .SetLimiteDistancia(GameObject.Find("limiteCyber2.0").transform.position, x: true, y: false);

            }
            if (/*GameObject.FindWithTag("inimigo") == null &&*/ spawnaMeio.inimigosEmCena >= 1 && Onda.Equals(4))
            {
                Onda = 5;
                travaOnda = true;
            }
        }
        

    }

    public static void QuintaOnda(ref int Onda, List<Spawns> spawns, List<Prefabs> prefabs, ref bool travaOnda, ref bool estagio1, ref bool estagio2)
    {
        var spawnMeio = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnMeio));
        var spawnaMeio = spawnMeio.Spawn.GetComponent<SpawnaInimigos>();

        if (Onda.Equals(5))
        {
            if (Onda != -1 && travaOnda)
            {
                travaOnda = false;
                spawnaMeio.Resetar();
                spawnaMeio.alcanceMaximo = 90;
                spawnaMeio.alcanceMinimo = 90;

                Vector3 limite;
                limite = GameObject.Find("limiteCyber2.0").transform.position;
                limite.x += 5f;
                var pre = prefabs.Find(x => x.Nome.Equals(NomesInimigos.CyberMarchaV2_1));

                spawnaMeio._primeiroSpawn = false;
                spawnaMeio.SetSpawnEmSequencia(pre, false, -40, 1, 5f)
                            .SetLimiteDistancia(limite, x:true, y: false);
            }
        }

        if (GameObject.FindWithTag("inimigo") == null && spawnaMeio.inimigosEmCena >= 1 && Onda.Equals(5))
        {
            estagio1 = false;
            estagio2 = true;

            travaOnda = true;
            Onda = 1;
        }
    }

}

public class Estagio2
{
    public static void PrimeiraOnda(ref int Onda, List<Spawns> spawns, List<Prefabs> prefabs, ref bool travaOnda)
    {
        var spawnMeio = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnMeio));
        var spawnaMeio = spawnMeio.Spawn.GetComponent<SpawnaInimigos>();

        if (Onda.Equals(1))
        {
            if (Onda != -1 && travaOnda)
            {
                travaOnda = false;
                spawnMeio.Spawn.SetActive(true);
                spawnaMeio.enabled = true;

                spawnaMeio.Resetar();

                spawnaMeio.alcanceMaximo = 90;
                spawnaMeio.alcanceMinimo = 90;

                spawnaMeio.SetPosition(new Vector2(-20, 0),false).SetSpawnEmSequencia(prefabs.Find(x => x.Nome.Equals(NomesInimigos.CyberMarchaV2_3)), false, 0, 1, 1f);

            }
        }

        if (GameObject.FindWithTag("inimigo") == null && spawnaMeio.inimigosEmCena >= 1)
        {
            //GameManager.instancia.FaseConcluida = true;
            Onda = 2;
            travaOnda = true;
        }
    }

    public static void SegundaOnda(ref int Onda, List<Spawns> spawns, List<Prefabs> prefabs, ref bool travaOnda)
    {
        var spawnMeio = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnMeio));
        var spawnaMeio = spawnMeio.Spawn.GetComponent<SpawnaInimigos>();

        if (Onda.Equals(2))
        {
            if (Onda != -1 && travaOnda)
            {
                travaOnda = false;
                spawnMeio.Spawn.SetActive(true);
                spawnaMeio.enabled = true;

                spawnaMeio.Resetar();

                spawnaMeio.alcanceMaximo = 90;
                spawnaMeio.alcanceMinimo = 90;

                var preFabCyber = prefabs.Find(x => x.Nome.Equals(NomesInimigos.CyberMarchaV2_3));

                spawnaMeio.SetPosition(new Vector2(7.78f, 0),true,false).SetSpawnEmSequencia(preFabCyber, false, 0, 1, 1f);
            }
        }

        if (/*GameObject.FindWithTag("inimigo") == null &&*/ spawnaMeio.inimigosEmCena >= 1 && Onda.Equals(2))
        {
            //GameManager.instancia.FaseConcluida = true;
            Onda = 3;
            travaOnda = true;
        }
    }

    public static void TerceiraOnda(ref int Onda, List<Spawns> spawns, List<Prefabs> prefabs, ref bool travaOnda)
    {
        var spawnMeio = spawns.Find(x => x.Nome.Equals(NomesSpawn.SpawnMeio));
        var spawnaMeio = spawnMeio.Spawn.GetComponent<SpawnaInimigos>();

        if (Onda.Equals(3))
        {
            if (Onda != -1 && travaOnda)
            {
                travaOnda = false;
                spawnMeio.Spawn.SetActive(true);
                spawnaMeio.enabled = true;

                spawnaMeio.Resetar();

                spawnaMeio.alcanceMaximo = 90;
                spawnaMeio.alcanceMinimo = 90;

                spawnaMeio._primeiroSpawn = false;
                spawnaMeio.SetPosition(new Vector2(-20, 0), false).SetSpawnEmSequencia(prefabs.Find(x => x.Nome.Equals(NomesInimigos.CyberMarchaV1_2)), false, 140, 2, 3f);
            }
        }

        if (GameObject.FindWithTag("inimigo") == null && spawnaMeio.inimigosEmCena >= 1)
        {
            GameManager.instancia.FaseConcluida = true;
            Onda = 3;
            travaOnda = true;
        }
    }

    public static void QuartaOnda(ref int Onda, List<Spawns> spawns, List<Prefabs> prefabs, ref bool travaOnda)
    {

    }
}


