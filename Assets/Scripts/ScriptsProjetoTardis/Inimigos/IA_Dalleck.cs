using System.Collections;
using UnityEngine;

public class IA_Dalleck : MonoBehaviour
{
    public GameObject laser;
    public GameObject spawnAttack;
    private Rigidbody2D rgb2d;

    public float speed_laser = -100f;
    public float life_Laser = 2f;
    public float cadenciaLaser = 2.5f;
    private float TempoLimiteLaser = 0f;
    

    public float CadenciaTempoVoo1 = 1.5f;
    public float CadenciaTempoVoo2 = 2.5f;
    private float TempoLimiteVooTotal = 0f;
    public float speedx = -2f;
    public float speedy = -3f;
    
    
    

    


    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Voar();
        Exterminar();
    }


    void Voar()
    {
        if (TempoLimiteVooTotal < Time.time)
        {
            speedy *= -1f;
            rgb2d.velocity = new Vector2(speedx, speedy);
            TempoLimiteVooTotal = UnityEngine.Random.Range(CadenciaTempoVoo1, CadenciaTempoVoo2) + Time.time;
        }
    }

    


    void Exterminar()
    {
        if (TempoLimiteLaser < Time.time)
        {
            StartCoroutine(AtirarLaser());

            TempoLimiteLaser = UnityEngine.Random.Range(0.5f, cadenciaLaser) + Time.time;
        }   
    }

    IEnumerator AtirarLaser()
    {
        var instant = Instantiate(laser, new Vector3(spawnAttack.transform.position.x,
            spawnAttack.transform.position.y, laser.transform.position.z), laser.transform.rotation);

        instant.GetComponent<Rigidbody2D>().AddForce(new Vector3(-speed_laser, 0, 0));

        yield return new WaitForSeconds(life_Laser);
        Destroy(instant);


    }


    

}
