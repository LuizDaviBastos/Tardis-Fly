using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class ConfiguracaoManager : MonoBehaviour
{
    public GameObject AttackModoLivre, AttackJoystick, AttackDividido, AttackRotaciona;
    public bool TgAttackModoLivre = false, TgAttackJoystick = false, TgAttackDividido = false, TgAttackRotaciona = false;


    public static ConfiguracaoManager instancia;

    void Awake()
    {
        
        if (instancia == null)
        {
            instancia = this;
        }

        else Destroy(this.gameObject);

        SceneManager.sceneLoaded += CarregaCena;

        TgAttackModoLivre = false;
        TgAttackJoystick = true;
    }

    void CarregaCena(Scene scene, LoadSceneMode load)
    {
        if (AttackModoLivre == null) AttackModoLivre = GameObject.Find("imgControle");
        if (AttackJoystick == null) AttackJoystick = GameObject.Find("BgImg");
       // if (AttackDividido == null) AttackDividido = GameObject.Find("AttackDividido");
       // if (AttackRotaciona == null) AttackRotaciona = GameObject.Find("AttackRotaciona");

        if (AttackModoLivre != null) AttackModoLivre.SetActive(false);
        if (AttackJoystick != null) AttackJoystick.SetActive(false);

       CarregarConfig();
/*
        if (AttackModoLivre != null && AttackJoystick != null && AttackDividido != null)
        {
            AttackModoLivre.SetActive(false);
            AttackJoystick.SetActive(false);
            AttackDividido.SetActive(false);
            if(AttackRotaciona != null) AttackRotaciona.SetActive(false);

            CarregarConfig();
        }*/

    }


    public void ConfigControleModoLivre(bool toggle)
    {
        if(this != null)
        {
            AttackModoLivre.SetActive(toggle);
            TgAttackModoLivre = toggle;
        }
        

    }

    public void ConfigControleJoystick(bool toggle)
    {
        if(this != null)
        {
            AttackJoystick.SetActive(toggle);
            TgAttackJoystick = toggle;
            Player.instancia.joystickSaida = AttackJoystick.GetComponent<Joystick>();
        }
        
    }

    public void ConfigControleDividido(bool toggle)
    {
        if(this != null)
        {
            AttackDividido.SetActive(toggle);
            TgAttackDividido = toggle;
        }
        
    }

    public void ConfigControleRotaciona(bool toggle)
    {
        if (this != null)
        {
            AttackRotaciona.SetActive(toggle);
            TgAttackRotaciona = toggle;
        }
    }

    public bool[] PegarConfig()
    {
        if (PlayerPrefs.HasKey(KeyPlayerPrefs.ConfigControleAttack))
        {
            var configs = PlayerPrefs.GetString(KeyPlayerPrefs.ConfigControleAttack).Split('|');
            bool[] newList = new bool[configs.Length];

            for (int i = 0; i < configs.Length; i++) newList[i] = Convert.ToBoolean(configs[i]);
            return newList;
        }
        else return null;
    }

    public void CarregarConfig()
    {
        if (PlayerPrefs.HasKey(KeyPlayerPrefs.ConfigControleAttack))
        {
            var configs = PlayerPrefs.GetString(KeyPlayerPrefs.ConfigControleAttack);

            if(AttackModoLivre != null) AttackModoLivre.SetActive(Convert.ToBoolean(configs.Split('|')[0]));
            if (AttackJoystick != null) AttackJoystick.SetActive(Convert.ToBoolean(configs.Split('|')[1]));
           // if (AttackDividido != null) AttackDividido.SetActive(Convert.ToBoolean(configs.Split('|')[2]));
            //if(AttackRotaciona != null) AttackRotaciona.SetActive(Convert.ToBoolean(configs.Split('|')[3]));
        }
        else
        {
            TgAttackModoLivre = true;
            SalvarConfig();
        }
    }

    public void SalvarConfig()
    {
        
        string configs = $"{TgAttackModoLivre}|{TgAttackJoystick}|{TgAttackDividido}|{TgAttackRotaciona}";

        PlayerPrefs.SetString(KeyPlayerPrefs.ConfigControleAttack, configs);
        CarregarConfig();
    }


}
