using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float valueTime { get; private set; }

    public Text timeText;
    public float timeAddBall = 20;

    public void StartTime()
    {
        StartCoroutine(TimeProcess());
    }

    public float StopTime()
    {
        StopAllCoroutines();
        return valueTime;
    }

    private IEnumerator TimeProcess()
    {
        float tempTimeAddBall = timeAddBall;
        valueTime = 0;

        while (true)
        {
            valueTime += Time.deltaTime;
            timeText.text = valueTime.ToString("0");

            if (valueTime > tempTimeAddBall)
            {
                GameController.instance.InitNewBall();
                tempTimeAddBall += timeAddBall;
            }

            yield return null;
        }
    }
}
