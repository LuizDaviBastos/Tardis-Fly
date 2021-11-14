using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackRotaciona : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public Image img;
    public GameObject sonico, tardis;
    
    void Start()
    {
        sonico = GameObject.Find("PaiParticleSystem");
        tardis = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<Image>().rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            var x = (pos.x / img.rectTransform.sizeDelta.x);
            var y = (pos.y / img.rectTransform.sizeDelta.y);

            //var x = pos.x;
            //var y = pos.y;
            print(new Vector2(x, y).magnitude);
            if (new Vector2(x, y).magnitude < 0.35f) return;

            var newZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            img.rectTransform.rotation = Quaternion.Euler(0, 0, newZ - 90);
            sonico.transform.rotation = Quaternion.Euler(0, 0, newZ);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tardis.GetComponent<Player>().StartTiro();
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

}
