using UnityEngine;

public class KitManager : MonoBehaviour
{

    public static KitManager instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);
    }

}
