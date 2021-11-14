using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Interfaces que cuidarão dos eventos de toque na tela, arrasto etc.
public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Image BgImg;

    [SerializeField]
    private Image JoyImg;

    public Vector3 entradaJoy;

    void Start()
    {
        BgImg = GameObject.Find("BgImg").GetComponent<Image>();
        JoyImg = BgImg.transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        if(Player.instancia != null) Player.instancia.MoverComJoystickDois(MoveHorizontal(), MoveVertical());
    }

    //Ao arrastar o dedo
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 posicao;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BgImg.rectTransform, eventData.position, eventData.pressEventCamera, out posicao))
        {
            posicao.x = (posicao.x / BgImg.rectTransform.sizeDelta.x);
            posicao.y = (posicao.y / BgImg.rectTransform.sizeDelta.y);

            //armazenando valores da posicao do "dedo"
            entradaJoy = new Vector3(posicao.x *2, posicao.y * 2, 0);

            //normalizando a magnitude deixando entre -1, 0, 1
            entradaJoy = (entradaJoy.magnitude > 1 ? entradaJoy.normalized : entradaJoy);

            //movendoJoy de acordo com a posição do "dedo" a bola da frente
            JoyImg.rectTransform.anchoredPosition = new Vector2(entradaJoy.x * JoyImg.rectTransform.sizeDelta.x, entradaJoy.y * JoyImg.rectTransform.sizeDelta.y);
            
        }
    }

    //Ao tocar
    public void OnPointerDown(PointerEventData eventData)
    {
        //PegarMovimentação até mesmo quando só apertar no joystick
        OnDrag(eventData);
        
    }

    //Ao largar
    public void OnPointerUp(PointerEventData eventData)
    {
        entradaJoy = Vector3.zero;
        JoyImg.rectTransform.anchoredPosition = Vector2.zero;
    }


    public float MoveHorizontal()
    {
        if (entradaJoy.x != 0) return entradaJoy.x;
        else return Input.GetAxis("Horizontal");
    }

    public float MoveVertical()
    {
        if (entradaJoy.y != 0) return entradaJoy.y;
        else return Input.GetAxis("Vertical");
    }

}
