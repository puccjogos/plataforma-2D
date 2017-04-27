using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placa : MonoBehaviour
{
    public Transform uiMsg;
    public float tempoTransicao = 0.5f;
    private Text txtMsg;
    public string msg;

    void Start()
    {
        txtMsg = uiMsg.GetComponentInChildren<Text>();
        txtMsg.text = msg;
        uiMsg.localScale = Vector3.one * 0.0001f;
        uiMsg.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            // mostrar mensagem
            uiMsg.gameObject.SetActive(true);
            Go.to(uiMsg, tempoTransicao, new GoTweenConfig()
                .scale(1f).setEaseType(GoEaseType.BounceInOut));
        }
    }

    void OnTriggerExit2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            // esconder mensagem
            Go.to(uiMsg, tempoTransicao, 
                new GoTweenConfig()
                .scale(0.0001f).setEaseType(GoEaseType.BounceInOut).onComplete(c =>
                    {
                        uiMsg.gameObject.SetActive(false);    
                    }));
        }
    }
}
