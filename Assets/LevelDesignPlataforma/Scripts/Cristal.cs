using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    public float speed;
    public Bloco[] blocos;

    void Awake()
    {
        blocos = GameObject.FindObjectsOfType<Bloco>();
    }

    void Start()
    {   
        speed = (Random.Range(-0.5f, 0.5f) < 0) ? -speed : speed;
    }

    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var b in blocos)
            {
                b.ativo = true;
                b.GetComponent<SpriteRenderer>().sprite = b.blocoAtivo;
                b.GetComponent<BoxCollider2D>().isTrigger = false;
            }
            Destroy(gameObject);
        }
    }
}
