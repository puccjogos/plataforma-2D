using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

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

    void Awake()
    {
        if (i == null)
        {
            i = this;

            DontDestroyOnLoad(gameObject);
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

    }

    void ChangeLevel(int mod)
    {
        if (changingLvls)
            return;
        currentLevel += mod;
        if (currentLevel == mapas.Count)
        {
            currentLevel = 0;
        }
        if (currentLevel < 0)
        {
            currentLevel = mapas.Count - 1;
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
        mapaAtual = Instantiate<Transform>(mapas[currentLevel], Vector3.zero, Quaternion.identity, this.transform);
        txtMapa.text = "ID: " + currentLevel.ToString() + " Nome: " + mapas[currentLevel].name + "\nZ para voltar, X para avanças, C para recarregar.";
        changingLvls = false;
    }
}
