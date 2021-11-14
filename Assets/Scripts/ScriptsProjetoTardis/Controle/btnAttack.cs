using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class btnAttack : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public GameObject pai;
    public Image imgAttack;
    public GameObject tardis;
    private Vector3 auxCentro;

    public float sensibilidadeX = 0;
    public float sensibilidadeY = 0;

    public float pointerPosicaoX;
    public bool TipoControleApartirDoDedo = false;
    public bool TipoControleApartirDaTardis = false;

    public Vector2 MovimentoAttack;
    public float MagnitudeDedo;

    public static btnAttack instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
        }
    }


    private void Start()
    {
        pai = GameObject.Find("PaiParticleSystem");
        imgAttack = GetComponent<Image>();
        tardis = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 posicao;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgAttack.rectTransform, eventData.position, eventData.pressEventCamera, out posicao))
        {

            
            if (TipoControleApartirDaTardis)
            {

                var posTardis = pai.transform.position;

                var DedoPos = eventData.pointerCurrentRaycast.worldPosition;
                MovimentoAttack = new Vector2(DedoPos.x - posTardis.x, DedoPos.y - posTardis.y);


                var s = Mathf.Atan2((DedoPos.y - posTardis.y), (DedoPos.x - posTardis.x)) * Mathf.Rad2Deg;
                //pai.transform.rotation = Quaternion.Lerp(pai.transform.rotation,  Quaternion.Euler(0, 0, s), 10f);

                pai.transform.rotation = Quaternion.Euler(0, 0, s);
                // pai.transform.rotation.Normalize();
            }
            else if (TipoControleApartirDoDedo)
            {
                //Rotacionando RaioSonico na direcao do dedo



                var DedoPos = eventData.pointerCurrentRaycast.worldPosition;
                MovimentoAttack = new Vector2(DedoPos.x - auxCentro.x, DedoPos.y - auxCentro.y);

                if (MovimentoAttack.x > 0) sensibilidadeX = 3;
                else sensibilidadeX = -3;


                var s = Mathf.Atan2((DedoPos.y - auxCentro.y), (DedoPos.x - auxCentro.x)) * Mathf.Rad2Deg;
                //pai.transform.rotation = Quaternion.Lerp(pai.transform.rotation,  Quaternion.Euler(0, 0, s), 10f);

                pai.transform.rotation = Quaternion.RotateTowards(pai.transform.rotation, Quaternion.Euler(0, 0, s), 4f);
                // pai.transform.rotation.Normalize();

            }
            else
            {
                //Rotaciona apartir do centro da tela
                var x = posicao.x / imgAttack.rectTransform.sizeDelta.x;
                var y = posicao.y / imgAttack.rectTransform.sizeDelta.y;

                var posDedo = new Vector2(x, y);
                if (posDedo.x > 0) sensibilidadeX = 3;
                else sensibilidadeX = -3;


                //var s = Mathf.Atan2(worldposition.y - pai.transform.position.y, worldposition.x - pai.transform.position.x) * Mathf.Rad2Deg;
                var s2 = Mathf.Atan2((y), (x)) * Mathf.Rad2Deg;
                pai.transform.rotation = Quaternion.Euler(0, 0, s2);
                pai.transform.rotation.Normalize();
            }



        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        auxCentro = eventData.pointerCurrentRaycast.worldPosition;
        tardis.GetComponent<Player>().StartTiro();
        OnDrag(eventData);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        tardis.GetComponent<Player>().StopTiro();
        OnDrag(eventData);
    }

    public void ConfiguraSensibilidade()
    {
        sensibilidadeX = GameObject.Find("SliderX").GetComponent<Slider>().value;
        sensibilidadeY = GameObject.Find("SliderY").GetComponent<Slider>().value;
    }
}
