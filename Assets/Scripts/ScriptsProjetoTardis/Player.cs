using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    public LayerMask LayerMaskTaris;
    public float speed = 100f;
    public bool invuneravel = false;
    public AudioClip coin;


    public Joystick joystickSaida;
    private Vector3 movimentacao;
    private float magnitudeVelTardis;

    public AudioSource audioTardis;
    

    private GameObject LimiteD, LimiteE, LimiteC, LimiteB;


    public static Player instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
        }

        else Destroy(this.gameObject);
    }


    void Start()
    {
        LimiteD = GameObject.FindWithTag("LimiteD");
        LimiteE = GameObject.FindWithTag("LimiteE");
        LimiteC = GameObject.FindWithTag("LimiteC");
        LimiteB = GameObject.FindWithTag("LimiteB");

       // joystickSaida = GameObject.Find("BgImg").GetComponent<Joystick>();
        //PegarMovimentoJoystick();
    }

    void Update()
    {
        //PegarMovimentoJoystick();
        LimitarTardis();

        if (Input.GetKey(KeyCode.Space)) GetComponent<Animator>().Play("TardisMaterializa");
       // MovimentoInput();
       // if(joystickSaida != null) MoverComJoystick();
       //MoverComImagem();

    }


    void Mover(float x, float y)
    {
        var rigidBd = GetComponentInChildren<Rigidbody2D>();

        if (rigidBd.bodyType != RigidbodyType2D.Static)
        {
            transform.Translate(new Vector3(x * speed, y * speed, 0));
            
        }

        transform.parent.transform.Translate(new Vector3(x * speed, y * speed, 0));


    }

    public void MoverComJoystickDois(float x, float y)
    {
        var rigidBd = transform.parent.GetComponent<Rigidbody2D>();

        if (rigidBd.bodyType != RigidbodyType2D.Static)
        {
            rigidBd.velocity = new Vector2(x * speed,y * speed);

            //transform.Translate(new Vector2(movimentacao.x * Time.unscaledDeltaTime * speed, movimentacao.y * Time.unscaledDeltaTime * speed));
            // magnitudeVelTardis = GetComponent<Rigidbody2D>().velocity.magnitude;
        }

    }

    public void MoverComJoystick()
    {
        var rigidBd = transform.parent.GetComponent<Rigidbody2D>();

        if (rigidBd.bodyType != RigidbodyType2D.Static)
        {
            rigidBd.velocity = new Vector2(joystickSaida.MoveHorizontal() * speed, joystickSaida.MoveVertical() * speed );

            //transform.Translate(new Vector2(movimentacao.x * Time.unscaledDeltaTime * speed, movimentacao.y * Time.unscaledDeltaTime * speed));
            // magnitudeVelTardis = GetComponent<Rigidbody2D>().velocity.magnitude;
        }

    }

    private void MovimentoInput()
    {
        /*if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
        {
           
        }*/
        
        Mover(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void PegarMovimentoJoystick()
    {

        if (joystickSaida != null)
        {
            movimentacao.x = joystickSaida.MoveHorizontal();
            movimentacao.y = joystickSaida.MoveVertical();

            if (movimentacao.x > 0)
            {
                GetComponent<Animator>().SetBool("Frente", true);
                GetComponent<Animator>().SetBool("Idle", false);
            }
            if (movimentacao.x < 0)
            {
                GetComponent<Animator>().SetBool("Tras", true);
                GetComponent<Animator>().SetBool("Idle", false);
            }
            if (magnitudeVelTardis == 0)
            {
                AnimIdle();
            }
        }

    }

    public void StopTiro()
    {
        GetComponent<Animator>().SetBool("Atirando", false);
        //raioSonico.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void StartTiro()
    {

        GetComponent<Animator>().SetBool("Atirando", true);
        //raioSonico.Play();
    }

    void AnimIdle()
    {
        GetComponent<Animator>().SetBool("Frente", false);
        GetComponent<Animator>().SetBool("Tras", false);
        GetComponent<Animator>().SetBool("Idle", true);
    }

    //Chamado em evento no animator
    public void TardisDinamica(string toggle)
    {
        if (Convert.ToBoolean(toggle))
        {
            //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            var escudo = transform.GetChild(1).gameObject;
            if (escudo != null && escudo.activeSelf == false) invuneravel = false;
        }
        else gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    //Chamado em evento no animator
    public void MoverTardisParaFantasma()
    {
        if (SkillsManager.instancia.cloneTardis != null)
        {
            gameObject.transform.position = SkillsManager.instancia.cloneTardis.transform.position;

            gameObject.GetComponent<Animator>().Play("TardisMaterializa");
            Destroy(SkillsManager.instancia.cloneTardis.gameObject);
        }
    }

    void LimitarTardis()
    {
        if (transform.position.x > LimiteD.transform.position.x)
        {
            var trans = transform.parent.position;
            trans.x = LimiteD.transform.position.x;
            transform.parent.position = trans;
        }
        if (transform.position.x < LimiteE.transform.position.x)
        {
            var trans = transform.parent.position;
            trans.x = LimiteE.transform.position.x;
            transform.parent.position = trans;
        }
        if (transform.position.y > LimiteC.transform.position.y)
        {
            var trans = transform.parent.position;
            trans.y = LimiteC.transform.position.y;
            transform.parent.position = trans;
        }
        if (transform.position.y < LimiteB.transform.position.y)
        {
            var trans = transform.parent.position;
            trans.y = LimiteB.transform.position.y;
            transform.parent.position = trans;
        }
    }

    public void PlaySomMaterializacao()
    {
        audioTardis.Play();
    }

    

}



