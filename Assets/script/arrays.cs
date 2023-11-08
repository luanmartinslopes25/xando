using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrays : MonoBehaviour
{
    int[] numeros = { 0, 10, 20 };
    int[] numeros2 = new int[5];
    string[] nomes = { "Sesc", "Ratinho", "Burno", "Kleber"};

    void Start()
    {


        for (int i = 0; i < numeros.Length; i++)
        {
            print(nomes[i]);
        }

        foreach(string nome in nomes)
        {
            print(nome);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
