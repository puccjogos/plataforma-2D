using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    public float reloadinterval = 0.1f;
    int currentLevel;
    //public string[] lvls;
    private bool changingLvls = false;
    public List<Transform> mapas;
    public Transform mapaAtual;
    public Text txtMapa;

    public string sequencia;
    public List<Transform> seqMapas;

    void Awake()
    {
        if (i == null)
        {
            i = this;

            seqMapas = new List<Transform>();
            var array = sequencia.Split('-');
            foreach (var item in array)
            {
                seqMapas.Add(mapas[Int32.Parse(item)]);
            }
            GameManager.i.currentLevel = 0;
            Reload();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // voltar
            ChangeLevel(-1);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            // avancar
            ChangeLevel(1);
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // recarregar
            Reload();
            return;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            int atual = SceneManager.GetActiveScene().buildIndex;
            if (atual - 1 < 0)
            {
                atual = SceneManager.sceneCountInBuildSettings - 1;
            }
            else
            {
                atual--;
            }
            SceneManager.LoadScene(atual);
            return;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            int atual = SceneManager.GetActiveScene().buildIndex;
            if (atual + 1 == SceneManager.sceneCountInBuildSettings)
            {
                atual = 0;
            }
            else
            {
                atual++;
            }
            SceneManager.LoadScene(atual);
            return;
        }

    }

    void ChangeLevel(int mod)
    {
        if (changingLvls)
            return;
        currentLevel += mod;
        if (currentLevel == seqMapas.Count)
        {
            currentLevel = 0;
        }
        if (currentLevel < 0)
        {
            currentLevel = seqMapas.Count - 1;
        }
        StartCoroutine(Load());
    }

    void Reload()
    {
        if (changingLvls)
            return;
        print("RELOAD LVL");
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        changingLvls = true;
        Color cor = Camera.main.backgroundColor;
        Camera.main.backgroundColor = Color.black;
        var res = GameObject.FindWithTag("Player");
        if (res != null)
        {
            Destroy(res);
        }
        yield return new WaitForSeconds(reloadinterval);
        Camera.main.backgroundColor = cor;
        if (mapaAtual != null)
        {
            DestroyImmediate(mapaAtual.gameObject);
        }
        mapaAtual = Instantiate<Transform>(seqMapas[currentLevel], Vector3.zero, Quaternion.identity, this.transform);
        txtMapa.text = "SEQ: " + SceneManager.GetActiveScene().name + " : " + sequencia + "\nNome: " + seqMapas[currentLevel].name;
        changingLvls = false;
    }
}
