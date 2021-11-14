using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackDividido : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public GameObject pai;
    public Image imgAttack;
    public GameObject tardis;


    void Start()
    {
        pai = GameObject.Find("PaiParticleSystem");
        imgAttack = GetComponent<Image>();
    }

  

    private void Update()
    {
        if(tardis == null) tardis = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 posicao;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgAttack.rectTransform, eventData.position, eventData.pressEventCamera, out posicao))
        {
            /*//Rotaciona apartir do centro da tela
            var x = posicao.x / imgAttack.rectTransform.sizeDelta.x;
            var y = posicao.y / imgAttack.rectTransform.sizeDelta.y;

            //var s = Mathf.Atan2(worldposition.y - pai.transform.position.y, worldposition.x - pai.transform.position.x) * Mathf.Rad2Deg;
            var s2 = Mathf.Atan2((y), (x)) * Mathf.Rad2Deg;
            pai.transform.rotation = Quaternion.Euler(0, 0, s2);
            pai.transform.rotation.Normalize();*/

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tardis.GetComponent<Player>().StartTiro();
        RaioSonico.instancia.StartFire();
       // OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        tardis.GetComponent<Player>().StopTiro();
        RaioSonico.instancia.StopFire();
    }
}
