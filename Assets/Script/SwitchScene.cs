using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string Texto;
    public void OnClick()
    {
        SceneManager.LoadScene(Texto);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("is exit");
    }
}

