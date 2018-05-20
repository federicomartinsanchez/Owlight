using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public GameObject LifePanel;
    public GameObject Score;
    public GameObject Heart_1;
    public GameObject Heart_2;
    public GameObject Heart_3;
    public GameObject Heart_4;
    public GameObject Heart_5;
    public GameObject Heart_6;
    public GameObject Mana_1;
    public GameObject Mana_2;
    public GameObject Mana_3;
    public GameObject Mana_4;
    public GameObject Mana_5;
    public GameObject Mana_6;

    private int scorePoints = 0;

    protected void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Score.GetComponent<Text>().text = "0";
    }

    //-- Actualiza la vida en la HUD --
    public void ActualiceLife(int actualLife)
    {
        switch (actualLife)
        {
            case 0: Heart_1.SetActive(false); break;
            case 1: Heart_2.SetActive(false); break;
            case 2: Heart_3.SetActive(false); break;
            case 3: Heart_4.SetActive(false); break;
            case 4: Heart_5.SetActive(false); break;
            case 5: Heart_6.SetActive(false); break;
        }
    }

    //-- Actualiza la vida en la HUD --
    public void ActualiceMana(int actualMana)
    {
        switch (actualMana)
        {
            case 0: Mana_1.SetActive(true); break;
            case 1: Mana_2.SetActive(true); break;
            case 2: Mana_3.SetActive(true); break;
            case 3: Mana_4.SetActive(true); break;
            case 4: Mana_5.SetActive(true); break;
            case 5: Mana_6.SetActive(true); break;
        }
    }

    //-- Gasta todo el mana de la HUD --
    public void UseAllMana()
    {
        Mana_1.SetActive(false);
        Mana_2.SetActive(false);
        Mana_3.SetActive(false);
        Mana_4.SetActive(false);
        Mana_5.SetActive(false);
        Mana_6.SetActive(false);
    }

    //-- Actualiza la score en la HUD --
    public void ActualiceScore()
    {
        scorePoints++;
        Score.GetComponent<Text>().text = scorePoints.ToString();
    }
}
