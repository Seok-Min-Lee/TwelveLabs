using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCtrl : MonoBehaviour
{
    private bool isQuitable = false;
    private float timer = 0f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isQuitable)
            {
                Application.Quit();
            }
            else
            {
                isQuitable = true;
            }
        }
        
        if (timer > 1)
        {
            timer = 0f;
            isQuitable = false;
        }

        if (isQuitable)
        {
            timer += Time.deltaTime;
        }
    }

    public void OnClickButton(int num)
    {
        string sceneName = GetNextSceneName(num);

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName: sceneName);
    }

    private string GetNextSceneName(int num)
    {
        string name = "";

        switch (num)
        {
            case 0:
                name = "Lotto";
                break;
        }

        return name;
    }
}
