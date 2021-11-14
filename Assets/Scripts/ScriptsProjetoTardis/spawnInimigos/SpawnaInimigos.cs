using System;
using System.Collections;
using System.Data;
using UnityEngine;

public class SpawnaInimigos : MonoBehaviour
{



    /*Tipos Spawn*/
    //Em sequencia
    private bool _spawnEmSequencia = false;
    private bool _rotacionarInstancia = true;
    private bool _setPosicao = false;
    public bool _limitarInstancia = false;
    ////////////////////\\\\\\\\\\\\\\\\\\\\

    //Configurar Spawn
    private Prefabs _prefab;
    private Vector3 _positionSpawn;
    public float AtrasoDoSpawn = 1f;

    public float VelocidadeMovimentoSpawn = 2f;
    public float alcanceMaximo;
    public float alcanceMinimo;
    private Vector3 _limiteDistanciaInstancia;
    private bool _limitarX;
    private bool _limitarY;

    public float rangeEmYMin;
    public float rangeEmYMax;

    public float rangeEmXMin;
    public float rangeEmXMax;

    public int _spawnsRealizados = 0;
    private int _limiteSpawn = 0;
    public bool _primeiroSpawn = true;
    private bool _right = true;

    public float zAngle = 0f;
    public float _forca = -100;

    ////////////////\\\\\\\\\\\\\\\\\

    private bool toggleReverte = false;
    public int inimigosEmCena = 1;
    private bool trava = true;


    //Backup Rotacao
    private float rotacaoInicial;
    private bool pegouRotacao = false;

    //////////////\\\\\\\\\\\\\\\

    private void Awake()
    {
        if (pegouRotacao == false)
        {
            rotacaoInicial = transform.eulerAngles.z;
            pegouRotacao = true;
        }

    }

    void Update()
    {
        if (_spawnEmSequencia && trava)
        {
            trava = false;
            SpawnaEmSequencia();
        }
    }
    public void SetRotation(float z)
    {
        var trans = transform.eulerAngles;
        trans.z = z;
        transform.eulerAngles = trans;
    }

    public SpawnaInimigos SetPosition(Vector2 posicao, bool right = true, bool concatenar = true)
    {

        if (concatenar) _positionSpawn = new Vector3(transform.position.x + posicao.x, transform.position.y + posicao.y, 0);
        else _positionSpawn = posicao;
        _right = right;

        _setPosicao = true;

        return this;
    }

    public SpawnaInimigos SetSpawnEmSequencia(Prefabs prefab, bool rotacionarInstancia, float forca, int limiteSpawn, float intervalo = 1f)
    {
        _forca = forca;
        _prefab = prefab;
        this._spawnEmSequencia = true;
        this._rotacionarInstancia = rotacionarInstancia;
        this.AtrasoDoSpawn = intervalo;
        this._limiteSpawn = limiteSpawn;
        return this;
    }

    public SpawnaInimigos SetLimiteDistancia(Vector3 distancia, bool x, bool y)
    {
        _limitarInstancia = true;
        _limiteDistanciaInstancia = distancia;
        _limitarX = x;
        _limitarY = y;
        return this;
    }

    void SpawnaEmSequencia()
    {
        if (_primeiroSpawn)
        {
            _primeiroSpawn = false;
            spawna();
        }

        else StartCoroutine(Spawna());

        IEnumerator Spawna()
        {
            yield return new WaitForSeconds(AtrasoDoSpawn);
            spawna();
        }
    }


    public void ReiniciarRotacao()
    {
        var euler = transform.eulerAngles;
        euler.z = rotacaoInicial;
        transform.eulerAngles = euler;

    }


    void spawna()
    {
        if (_spawnsRealizados < _limiteSpawn)
        {

            _spawnsRealizados++;
            GameObject instant;
            if (_setPosicao == false)
            {
                instant = Instantiate(_prefab.Prefab, new Vector2(transform.position.x + UnityEngine.Random.Range(rangeEmXMin, rangeEmXMax), transform.position.y + UnityEngine.Random.Range(rangeEmYMin, rangeEmYMax)), (_rotacionarInstancia ? transform.rotation : Quaternion.identity));

                if (_right) Virar(1);
                else Virar(-1);
                
            }
            else
            {
                instant = Instantiate(_prefab.Prefab, new Vector2(_positionSpawn.x + UnityEngine.Random.Range(rangeEmXMin, rangeEmXMax), _positionSpawn.y + UnityEngine.Random.Range(rangeEmYMin, rangeEmYMax)), (_rotacionarInstancia ? transform.rotation : Quaternion.identity));
                if (_right) Virar(1);
                else Virar(-1);
            }

            void Virar(int scale)
            {
                var lScale = instant.transform.localScale;
                lScale.x = scale;
                instant.transform.localScale = lScale;
            }

            //ajuste da direção do impulso do prefab para a mesma direção do Y do objeto que o instanciou.
            zAngle = (transform.localEulerAngles.z - 90);
            float x = _forca * Mathf.Cos(zAngle * Mathf.Deg2Rad);
            float y = _forca * Mathf.Sin(zAngle * Mathf.Deg2Rad);

            instant.GetComponent<Empulso>().MoverEmpulsionando(new Vector2(x, y));
            if (_limitarInstancia) instant.GetComponent<Empulso>().LimitarImpulso(_limiteDistanciaInstancia, _limitarX, _limitarY);
            ////////////////////\\\\\\\\\\\\\\\\\\\\


            //Movimento eixo Z do spawn
            if (toggleReverte == false)
            {
                if (transform.eulerAngles.z <= alcanceMinimo) toggleReverte = true;
                else
                {
                    var euler = transform.eulerAngles;
                    euler.z -= VelocidadeMovimentoSpawn;
                    transform.eulerAngles = euler;
                }

            }
            else if (toggleReverte)
            {
                if (transform.eulerAngles.z >= alcanceMaximo) toggleReverte = false;
                else
                {
                    var euler = transform.eulerAngles;
                    euler.z += VelocidadeMovimentoSpawn;
                    transform.eulerAngles = euler;
                }
            }

            inimigosEmCena++;
            trava = true;
        }
    }

    public SpawnaInimigos Resetar()
    {
        rangeEmXMax = 0;
        rangeEmXMin = 0;
        rangeEmYMax = 0;
        rangeEmYMin = 0;
        _spawnsRealizados = 0;
        trava = true;
        inimigosEmCena = 0;
        _setPosicao = false;
        _primeiroSpawn = true;
        return this;
    }

}
