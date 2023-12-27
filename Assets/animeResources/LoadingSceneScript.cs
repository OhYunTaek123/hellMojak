using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneScript : MonoBehaviour
{
    public static LoadingSceneScript Create()
    {
        var LoadingSceneScriptPrefab = Resources.Load<LoadingSceneScript>("LoadingSceneScript");
        return Instantiate(LoadingSceneScriptPrefab);
    }
}
