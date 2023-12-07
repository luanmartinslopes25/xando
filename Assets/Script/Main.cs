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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownStart());
    }



    public IEnumerator CountdownStart()
    {
        // � invertido porque o score aumenta quando morre
        score1.text = player2.score.ToString();
        score2.text = player1.score.ToString();

        player1.Respawn();
        player2.Respawn();

        ready = false;

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
}