using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCtrl : MonoBehaviour
{
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
