using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CloneTardis : MonoBehaviour
{

    public static CloneTardis instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
        }

        else Destroy(this.gameObject);
    }


    public Joystick joystick;
    public float speed = 5f;
    public GameObject Tardis;
    public bool podeMover = true;


    void Start()
    {
        joystick = GameObject.Find("BgImg").GetComponent<Joystick>();
        Tardis = GameObject.Find("Tardis");

    }
    
    void Update()
    {
        Mover(); 
    }

    void Mover()
    {
        if (podeMover == false) return;

        var x = joystick.MoveHorizontal();
        var y = joystick.MoveVertical();
        if(x != 0f || y != 0f)
        {
            transform.Translate(new Vector3(x * speed * Time.unscaledDeltaTime, y * speed * Time.unscaledDeltaTime, 0));
        }
    }
}
