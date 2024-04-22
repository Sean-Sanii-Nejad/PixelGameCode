using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameObject debuffPanel;
    private GameObject debuffSymbol;

    void Start()
    {
        debuffPanel = GameObject.Find("Canvas/DebuffPanel");
        debuffSymbol = GameObject.Find("Canvas/DebuffPanel/DebuffSymbol");
        debuffSymbol.SetActive(false);
    }

    public void SetDebuffSymbol(bool setValue)
    {
        if(setValue)
        {
            debuffSymbol.SetActive(true);
        }
        else
        {
            debuffSymbol.SetActive(false);
        }
    }
   
}
