using UnityEngine;
using UnityEditor;
using Tiled2Unity;
using System.Collections;
using System.Collections.Generic;

[Tiled2Unity.CustomTiledImporter]
public class ImportadorDeObjetos : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(GameObject gameObject,
                                       IDictionary <string, string> customProperties)
    {
        Debug.Log("processing prefab: " + gameObject.name);

        if (customProperties.ContainsKey("AddComp"))
        {
            Debug.Log("AddComp: " + customProperties["AddComp"]);
            UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(gameObject, "Assets/Editor/ImportadorDeObjetos.cs (22,17)", customProperties["AddComp"]);
        }

        if (customProperties.ContainsKey("PrefabSwap"))
        {
            
            GameObject prefab = Resources.Load<GameObject>(gameObject.name);
            GameObject go = (GameObject)GameObject.Instantiate(prefab, gameObject.transform.position + new Vector3(0.5f, 0.5f), Quaternion.identity);
            go.transform.SetParent(gameObject.transform.parent);
            if (gameObject.name == "Placa")
            {
                go.GetComponent<Placa>().msg = customProperties["msg"];
            }


            /*
            if (gameObject.name == "Sign")
            {
                go.GetComponent<Sign>().text = customProperties["msg"];
            }
            */
            gameObject.SetActive(false);
        }


    }

    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing

    }
}
