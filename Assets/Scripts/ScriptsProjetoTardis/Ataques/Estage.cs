using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Estage : AtaqueManager
{
    public void AtaquesNivel1()
    {
        AtaqueFixo();

        void AtaqueFixo()
        {
            if (trava)
            {
                trava = false;

                var inimigoAtual = Inimigos.ElementAtOrDefault(AtaqueAtual);

                //TODO - Verificando se ganhou a fase
                if (inimigoAtual == null)
                {
                    GameManager.instancia.GameWinGame();
                    return;
                }

                StartCoroutine(ChamarFuncao());

                IEnumerator ChamarFuncao()
                {
                    yield return new WaitForSeconds(inimigoAtual.RateTime);

                    var instant = Instantiate(inimigoAtual.Prefab, new Vector2(PosicaoSpawn.position.x, PosicaoSpawn.position.y), Quaternion.identity);

                    if (inimigoAtual.isSubAtaque)
                    {
                        LacosEmSubAtaque(inimigoAtual.SubAtaques);

                        void LacosEmSubAtaque(List<Ataques> ataques)
                        {
                            ataques.ForEach((x) =>
                            {
                                //faca o ataque aqui
                                StartCoroutine(spawn());
                                IEnumerator spawn()
                                {
                                    yield return new WaitForSeconds(x.RateTime);
                                    Instantiate(x.Prefab, new Vector2(PosicaoSpawn.position.x, PosicaoSpawn.position.y), Quaternion.identity);
                                }

                                if (x.isSubAtaque) LacosEmSubAtaque(x.SubAtaques);
                            });
                        }
                    }

                    if (instant.GetComponent<AtaquesRepetitivos>() != null) inimigosEmSequencia = 0;

                    AguardeMatarTodos();
                }

                
            }
        }
    }
}

