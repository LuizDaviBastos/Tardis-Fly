using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class imgControle : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image imgJoy;
    public Vector2 Movimento;
    public float Magnitude;
    public Vector3 posicaoDedo;
    public Vector3 posicaoTardis;
    public Vector3 posicaoFinal;
    public GameObject tardis;
    public Ray raio;


    public Vector3 Diferenca;

    public Vector2 posBackup;


    public bool ToggleControleCentro = false;

    public bool trava = false;


    public float vel = 0.25f;
    public Vector3 refer;

    public static imgControle instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
        }

        else Destroy(this.gameObject);
    }


    private void Start()
    {
        imgJoy = gameObject.GetComponent<Image>();
        tardis = GameObject.FindWithTag("PlayerMov");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;


        //verfica se esta tocando em cima da imagem e tem uma said da posicao de onde foi tocado
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgJoy.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x /= imgJoy.rectTransform.sizeDelta.x;
            pos.y /= imgJoy.rectTransform.sizeDelta.y;
        }

        if (posicaoDedo != null)
        {

            posicaoDedo = eventData.pointerCurrentRaycast.worldPosition;
            posicaoDedo.z = 0;
            // var x = eventData.pointerCurrentRaycast.worldPosition.x /*- posicaoDedo.x*/;
            //var y = eventData.pointerCurrentRaycast.worldPosition.y /*- posicaoDedo.y*/;

            var novaPosicao = posicaoDedo + Diferenca;



            //tardis.transform.position = novaPosicao;

            //tardis.transform.position = Vector3.SmoothDamp(tardis.transform.position, novaPosicao, ref refer, vel);

            if(tardis == null) tardis = GameObject.FindWithTag("PlayerMov");
            else tardis.transform.position = Vector3.Lerp(tardis.transform.position, novaPosicao, vel);

            

            //tardis.transform.position = Vector3.Slerp(tardis.transform.position, novaPosicao, vel);

            // tardis.transform.GetComponent<Rigidbody2D>().MovePosition(novaPosicao);



            //tardis.transform.Translate(new Vector3((posicaoDedo.x + Diferenca.x) * Time.deltaTime , (posicaoDedo.y + Diferenca.y) * Time.deltaTime , 0));
            //tardis.transform.SetPositionAndRotation(posicaoDedo + Diferenca, Quaternion.identity);



        }

    }

    public void ReduzirDistancia()
    {
        posicaoTardis = tardis.transform.position;
        Diferenca = (posicaoTardis - posicaoDedo);

        //tardis.transform.position = posicaoTardis;

    }

  



    public void OnPointerDown(PointerEventData eventData)
    {
        posicaoDedo = eventData.pointerCurrentRaycast.worldPosition;
        posicaoTardis = tardis.transform.position;
        posicaoDedo.z = tardis.transform.position.z;
        Diferenca = posicaoTardis - posicaoDedo;

        //OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Movimento = Vector2.zero;

    }

}
