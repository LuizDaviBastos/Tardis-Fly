using System.Collections;
using System.Linq;
using UnityEngine;


public class AtivaObstaculo : MonoBehaviour
{

    [SerializeField]
    private float menorouigual, maiorouigual;

    [SerializeField]
    private GameObject ObjPai;

    private void Awake()
    {
       
       
    }

    void Start()
    {
          // ToggleFilhosState(false);
    }

    private void Update()
    {
       /* if (transform.position.x <= menorouigual && transform.position.x >= maiorouigual)
        {
            ToggleFilhosState(true);
        }*/
    }

    void ToggleFilhosState(bool toggle)
    {

        if (transform.childCount <= 0 || transform.GetChild(0).gameObject.activeSelf == toggle) return;

        else
        {
            GameObject[] chields = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                chields[i] = transform.GetChild(i).gameObject;
            }

            chields.ToList().ForEach((x) => x.SetActive(toggle));
        }
    }

    private void OnBecameVisible()
    {
        
    }

    private void OnBecameInvisible()
    {
       
    }

}
