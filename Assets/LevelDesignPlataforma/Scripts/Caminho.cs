using UnityEngine;
using System.Collections;

public class Caminho : MonoBehaviour
{
    void Awake()
    {
        var prefab = Resources.Load<PlataformaMovel>("PlataformaMovel");
        var plataforma = GameObject.Instantiate<PlataformaMovel>(prefab, this.transform.parent);
        plataforma.basePath = GetComponent<EdgeCollider2D>();
    }
}

