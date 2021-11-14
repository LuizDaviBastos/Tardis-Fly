using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Empulso : MonoBehaviour
{
    private Vector2 _empulso;
    private bool _empulsiona = false;
    //private bool _empulsionaMeio = false;
    private bool _limitarEmpulso = false;
    private bool _limiteX = false;
    private bool _limiteY = false;
    private Vector3 _limiteEmpulso;

    public float speed = 1;
   


    private void Update()
    {
        Impulso();
    }


    public Empulso MoverEmpulsionando(Vector3 empulso)
    {
        _empulso = empulso;
        _empulsiona = true;

        return this;
    }

    public void LimitarImpulso(Vector3 limite, bool x, bool y)
    {
        _limiteX = x;
        _limiteY = y;
        _limitarEmpulso = true;
        _limiteEmpulso = limite;

    }



    void Impulso()
    {
        if (_empulso != null && _empulsiona && _limitarEmpulso == false)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_empulso.x * Time.deltaTime, _empulso.y * Time.deltaTime);
        }

        else if (_limitarEmpulso)
        {
            if (_limiteX && _limiteY == false)
            {
                if (transform.position.x > _limiteEmpulso.x)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_empulso.x * Time.deltaTime, _empulso.y * Time.deltaTime);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }

            }
            else if (_limiteY && _limiteX == false)
            {
                if (transform.position.y > _limiteEmpulso.y)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_empulso.x * Time.deltaTime, _empulso.y * Time.deltaTime);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }
            else if (_limiteX && _limiteY)
            {
                if (transform.position.y > _limiteEmpulso.y)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, _empulso.y * Time.deltaTime);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
                if (transform.position.x > _limiteEmpulso.x)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_empulso.x * Time.deltaTime, 0f);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }

        }
    }


}
