using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngineInternal;

public class CyberV1Movimento : MonoBehaviour
{
    public float VelocidadeMovimento = 1f;
    public float SmoothTimeRotate;
    public bool zigzag;
    public Vector3 pos;

    public float posYMax;
    public float posYMin;

    public float RotateParaCima;
    public float RotateParaBaixo;

    private bool trava;

    void Start()
    {
    
    }

    
    void Update()
    {
        ZigZag();
    }

    void ZigZag()
    {
        
        

        if (zigzag)
        {
            if (transform.position.y <= posYMin)
            {
                trava = true;
            }
            if(transform.position.y >= posYMax)
            {
                trava = false;
            }

            Zig(); Zag();


            void Zag()
            {
                if (trava == false) return;
                
                var rotation = transform.eulerAngles;
                rotation.z = Mathf.Lerp(transform.eulerAngles.z, RotateParaCima, SmoothTimeRotate);

                

                transform.eulerAngles = rotation;

                

                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, RotateBaixo), SmoothTimeRotate);

                transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 1), VelocidadeMovimento);
            }

            void Zig()
            {
                if (trava) return;

                var rotation = transform.eulerAngles;
                rotation.z = Mathf.Lerp(transform.eulerAngles.z, RotateParaBaixo, SmoothTimeRotate);
                transform.eulerAngles = rotation;

                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, RotateCima), SmoothTimeRotate);
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 1), VelocidadeMovimento);
            }
        }
        


    }
}
