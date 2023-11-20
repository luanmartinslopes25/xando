using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string SceneSwitch;

    public void OnClick()
    {
        SceneManager.LoadScene(SceneSwitch);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("is exit");
    }
}

