using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    public float reloadinterval = 0.1f;
    int currentLevel;
    //public string[] lvls;
    private bool changingLvls = false;

    void Awake()
    {
        if (i == null)
        {
            i = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        GameManager.i.currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    void NextLevel()
    {
        if (changingLvls)
            return;
        currentLevel++;
        if (currentLevel >= SceneManager.sceneCountInBuildSettings)
        {
            currentLevel = 0;
        }
        StartCoroutine(Load());
    }

    void Reload()
    {
        if (changingLvls)
            return;
        print("RELOAD LVL");
        currentLevel = SceneManager.GetActiveScene().buildIndex; 
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        changingLvls = true;
        Color cor = Camera.main.backgroundColor;
        Camera.main.backgroundColor = Color.black;
        yield return new WaitForSeconds(reloadinterval);
        Camera.main.backgroundColor = cor;
        SceneManager.LoadScene(currentLevel);
        changingLvls = false;
    }
}
