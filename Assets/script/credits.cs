using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    public Transform tranform;
    private int speed = 160;
    public int normalSpeed = 160;
    public int fastSpeed = 640;
    public int tamanho = 1600;
    public GameObject BackButton;

    void Start()
    {
        Time.timeScale = 1;
        BackButton.SetActive(false);
    }
    void FixedUpdate()
    {
        if (Input.GetButton("Submit"))
        {
            speed = fastSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        if (transform.position.y < tamanho)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
