using System;
using System.Collections;
using UnityEngine;

public class MoveZigZag : MonoBehaviour
{

    public bool _ZigZag;
    public float velocity = 5f;

    public float LimiteXMin;
    public float LimiteXMax;


    void Start()
    {
        StartCoroutine(nameof(ZigZag));
    }


    IEnumerator ZigZag()
    {
        while (_ZigZag)
        {
            if (transform.position.x <= LimiteXMin) velocity = Math.Abs(velocity);
            if (transform.position.x >= LimiteXMax) velocity = Math.Abs(velocity) * -1;
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity * Time.deltaTime, 0);

            yield return null;
        }
        
    }
}
