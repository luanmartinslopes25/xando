using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public int countdownTime;
    public Text countdownDisplay;
    public float timerSpeed = 0.4f;

    public bool ready;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownStart());
    }



    IEnumerator CountdownStart()
    {
        ready = false;
        countdownTime = 3;
        countdownDisplay.text = countdownTime.ToString();
        countdownDisplay.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(timerSpeed);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(timerSpeed);
        countdownDisplay.gameObject.SetActive(false);
        ready = true;
    }
}
