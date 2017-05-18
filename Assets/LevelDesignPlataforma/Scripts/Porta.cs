using System;
using UnityEngine;

public class Porta: MonoBehaviour
{
    public bool aberta = false;
    public Sprite portaAberta;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (aberta && other.CompareTag("Player"))
        {
            GameManager.i.SendMessage("ChangeLevel", 1);
        }
    }

    public void Unlock()
    {
        GetComponent<SpriteRenderer>().sprite = portaAberta;
        aberta = true;
    }
}

