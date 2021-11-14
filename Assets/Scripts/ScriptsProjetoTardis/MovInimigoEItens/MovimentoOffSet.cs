using UnityEngine;

public class MovimentoOffSet : MonoBehaviour
{

    public bool MovimentoPosicao;
    public bool MovimentoOffset;
    public bool Meshrender;
    public bool MovOffSetRawImage;


    public bool MovY;
    public bool MovX;


    public float vel = 1f;

    void Update()
    {
        if (MovX)
        {
            if (MovimentoPosicao) transform.position += new Vector3(vel * Time.deltaTime, 0, 0);
            if (MovimentoOffset) gameObject.GetComponent<SpriteRenderer>().material.mainTextureOffset += new Vector2(vel * Time.deltaTime, 0);
            if (Meshrender) GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(vel * Time.deltaTime, 0);
        }

        if (MovY)
        {
            if (MovimentoPosicao) transform.position += new Vector3(0, vel * Time.deltaTime, 0);
            if (MovimentoOffset) gameObject.GetComponent<SpriteRenderer>().material.mainTextureOffset += new Vector2(0, vel * Time.deltaTime);
            if (Meshrender) GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(0, vel * Time.deltaTime);

            if (MovOffSetRawImage)
            {
                var rect = GetComponent<UnityEngine.UI.RawImage>().uvRect;
                rect.y += vel * Time.deltaTime;
                GetComponent<UnityEngine.UI.RawImage>().uvRect = rect;
            }
        }

    }
}