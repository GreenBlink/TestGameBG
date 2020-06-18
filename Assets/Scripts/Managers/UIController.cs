using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    private int currentColor;

    public List<Color> colors = new List<Color>();
    public GameObject ui;
    public CanvasGroup info;
    public Text textChangeColorBall;
    public Text textRecord;

    public void Init()
    {
        currentColor = GameController.instance.saveManager.GetIndexColor();
        textChangeColorBall.color = colors[currentColor];
    }

    public void OpenWindow()
    {
        ui.SetActive(true);
        textRecord.text = GameController.instance.saveManager.GetRecord().ToString("0");
        info.alpha = 0;
        info.DOFade(1, 0.3f);
    }

    public void CloseWindow()
    {
        info.DOFade(0, 0.3f).OnComplete(Close);      
    }

    public void ChangeColorBall()
    {
        currentColor++;

        if (currentColor >= colors.Count)
            currentColor = 0;

        textChangeColorBall.color = colors[currentColor];
        GameController.instance.saveManager.SaveIndexColor(currentColor);
    }

    public Color GetColorBall()
    {
        return colors[currentColor];
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Close()
    {
        ui.SetActive(false);
        GameController.instance.InitNewRound();
    }
}
