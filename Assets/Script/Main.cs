using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Players player1;
    public Players player2;
    public Text score1;
    public Text score2;

    public int countdownTime = 3;
    public float timerSpeed = 0.4f;

    private int currentCountdownTime;
    public Text countdownDisplay;

    public bool ready;

    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public Transform spawn5;
    public int spawnSelector1;
    public int spawnSelector2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownStart());
    }



    public IEnumerator CountdownStart()
    {
        // é invertido porque o score aumenta quando morre
        score1.text = player2.score.ToString();
        score2.text = player1.score.ToString();

        player1.Respawn();
        player2.Respawn();

        ready = false;

        StartCoroutine(SpawnPessoasDivertidas());

        currentCountdownTime = countdownTime;
        countdownDisplay.text = currentCountdownTime.ToString();
        countdownDisplay.gameObject.SetActive(true);
        while (currentCountdownTime > 0)
        {
            countdownDisplay.text = currentCountdownTime.ToString();
            yield return new WaitForSeconds(timerSpeed);
            currentCountdownTime--;
        }
        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(timerSpeed);
        countdownDisplay.gameObject.SetActive(false);
        ready = true;
    }

    public IEnumerator SpawnPessoasDivertidas()
    {
        spawnSelector1 = UnityEngine.Random.Range(1, 6);
        spawnSelector2 = UnityEngine.Random.Range(1, 6);
        if (spawnSelector1 == spawnSelector2)
        {
            while (spawnSelector2 == spawnSelector1)
            {
                spawnSelector2 = UnityEngine.Random.Range(1, 6);
            }
        }
        yield return new WaitForEndOfFrame();

        //P1
        if (spawnSelector1 == 1)
        {
            player1.transform.position = spawn1.position;
        }
        else if (spawnSelector1 == 2)
        {
            player1.transform.position = spawn2.position;
        }
        else if (spawnSelector1 == 3)
        {
            player1.transform.position = spawn3.position;
        }
        else if(spawnSelector1 == 4)
        {
            player1.transform.position = spawn4.position;
        }
        else if( spawnSelector1 == 5)
        {
            player1.transform.position = spawn5.position;
        }

        //P2
        if (spawnSelector2 == 1)
        {
            player2.transform.position = spawn1.position;
        }
        else if (spawnSelector2 == 2)
        {
            player2.transform.position = spawn2.position;
        }
        else if (spawnSelector2 == 3)
        {
            player2.transform.position = spawn3.position;
        }
        else if (spawnSelector2 == 4)
        {
            player2.transform.position = spawn4.position;
        }
        else if (spawnSelector2 == 5)
        {
            player2.transform.position = spawn5.position;
        }
    }
}
