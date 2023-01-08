using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCtrl : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(SqlliteDbConnector.DbCreate());
    }

    private bool isInit;
    private void Update()
    {
        if (SqlliteDbConnector.isCreate && !isInit)
        {
            DataManager.Init();
            isInit = true;

            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.TITLE);
        }
    }
}
