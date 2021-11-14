using System;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject ExplosaoCyberTiro;
    public float dano;
    public bool ToRight;

    private Vector3 _destino, _impulso, _origem;

    [SerializeField]
    private float _speed = 5f;
    private bool segue = false;
    private bool empulso = false;



    void Update()
    {
        Segue();
        Impulso();
    }


    public void Impulsionar(Vector2 direcao, float speed)
    {
        _speed = speed;
        _impulso = direcao;
        empulso = true;
    }

    public void Impulsionar(Vector2 direcao, bool toright)
    {
        _impulso = direcao;
        empulso = true;
        ToRight = toright;
     
    }

    public void Seguir(Vector3 destino, Vector3 origem, float speed, bool toright)
    {
        _speed = speed;
        _destino = destino;
        _origem = origem;
        //_destino = Vector3.MoveTowards(transform.position, destino, _speed * Time.deltaTime);
        segue = true;
        ToRight = toright;
    }


    void Impulso()
    {

        var scale = transform.localScale;
        scale.x = (ToRight ? Math.Abs(scale.x) : Math.Abs(scale.x) * -1);
        transform.localScale = scale;

        if (_impulso != null && empulso == true)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_impulso.x * _speed, _impulso.y * _speed);
        }
    }

    void Segue()
    {
        if (_destino != null && segue == true)
        {

            //Cuidando da rotação do tiro
            Quaternion newRotation;
            newRotation = Quaternion.LookRotation(_destino - _origem);
            GetComponent<Rigidbody2D>().MoveRotation(newRotation);

            var scale = transform.localScale;
            scale.x = (ToRight ? Math.Abs(scale.x) : Math.Abs(scale.x) * -1);
            transform.localScale = scale;


            //Segue na direção do player e passa (como um tiro)
            Ray ray = new Ray(transform.position, _destino - _origem);
            transform.position = Vector3.MoveTowards(transform.position, ray.GetPoint(_destino.magnitude), _speed * Time.deltaTime);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().invuneravel == false) VidaManager.instancia.PerdeVida(dano);

            Instantiate(ExplosaoCyberTiro, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Escudo"))
        {
            Instantiate(ExplosaoCyberTiro, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
