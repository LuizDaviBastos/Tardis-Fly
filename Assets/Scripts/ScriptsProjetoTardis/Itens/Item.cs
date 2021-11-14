using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Item : MonoBehaviour
{

    public float vidaItem;
    public List<ItemTipo> itemTipos;
    public GameObject preFabExplosaoMeteoro;

    private bool antiHorario = false;

    private void Start()
    {
        antiHorario = ((UnityEngine.Random.Range(1, 3) == 2) ? true : false);
    }

    private void FixedUpdate()
    {
        //Rotaciona Rocha
        Rotaciona();
    }

    void Rotaciona()
    {
        var rota = transform.eulerAngles;
        if (antiHorario)rota.z -= 0.5f;
        else rota.z += 0.5f;
        transform.eulerAngles = rota;
    }

    private void PerdeVida(float dano)
    {
        if (vidaItem > 0.0f)
        {
            this.vidaItem -= dano;
        }
        else
        {
            var random = UnityEngine.Random.Range(1, 5);
            if (random == 2)
            {
                Instantiate(itemTipos.Find(x => x.nomeItem.Equals(ItemDrop.Moeda)).Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }

            Instantiate(preFabExplosaoMeteoro, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

            ScoreManager.instancia.ColetaScore(Pontuacao.ScoreItem);

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("RaioSonico"))
        {
            PerdeVida(collision.gameObject.GetComponent<RaioSonico>().DanoPorSegundo - 0.2f);
        }
    }

}

[System.Serializable]
public class ItemTipo
{
    public ItemDrop nomeItem;
    public GameObject Prefab;
}

public enum ItemDrop
{
    Moeda
}


