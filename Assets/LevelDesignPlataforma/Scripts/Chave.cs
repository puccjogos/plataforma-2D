using System;
using UnityEngine;

public class Chave : MonoBehaviour
{
    public Porta alvo;

    void Awake()
    {
        alvo = GameObject.FindObjectOfType<Porta>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !alvo.aberta)
        {
            alvo.Unlock();
            Destroy(gameObject);
        }
    }

}

