using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickAttack : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image BgJoy, JoyFront;
    public GameObject tardis;
    public GameObject sonico;

    public Vector2 Movimento;
    public Vector2 SizeDelta;
    public float magnitude;
    public Quaternion newRotation;
    public float Multiplicador = 10f;

    public static JoystickAttack instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
        }

        else Destroy(this.gameObject);
    }


    void Start()
    {
        BgJoy = GetComponent<Image>();
        JoyFront = transform.GetChild(0).GetComponent<Image>();
        tardis = GameObject.FindGameObjectWithTag("Player");
        sonico = GameObject.Find("PaiParticleSystem");
    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BgJoy.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            var x = (pos.x / BgJoy.rectTransform.sizeDelta.x) * Multiplicador;
            var y = (pos.y / BgJoy.rectTransform.sizeDelta.y) * Multiplicador;

            Movimento = new Vector2(x, y);

            var newZ = Mathf.Atan2(Movimento.y, Movimento.x) / Mathf.Deg2Rad;
            magnitude = Movimento.magnitude;

            
            if(sonico!= null) sonico.transform.rotation = Quaternion.Euler(0, 0, newZ);

            newRotation = Quaternion.Euler(0, 0, newZ);

                
            var posJoyFront = new Vector2(x/ Multiplicador, y / Multiplicador);
            posJoyFront = (posJoyFront.magnitude > 1 ? posJoyFront.normalized : posJoyFront);
            JoyFront.rectTransform.anchoredPosition = new Vector2((posJoyFront.x * JoyFront.rectTransform.sizeDelta.x) , (posJoyFront.y * JoyFront.rectTransform.sizeDelta.y));
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        
        //OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        

        
        Movimento = Vector2.zero;
        //JoyFront.rectTransform.anchoredPosition = Vector2.zero;
    }

    /*public float RotacaoRaioSonico(out float magnitude)
    {
        if (Movimento != Vector2.zero) return newRotation;
        else Input.GetAxis("");
    }*/
}
