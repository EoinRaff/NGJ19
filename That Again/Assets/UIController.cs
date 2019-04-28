using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : Singleton<UIController>
{
    public TextMeshProUGUI yearsText;
    public Slider temperature;
    public Transform gameOver;
    public Transform[] inGameUI;
    public Button Reset;
    public TextMeshProUGUI endGameText;

    public Sprite ThermometerFill;
    float startY;
    float currentY;
    float endY;

    // Update is called once per frame
    void Update()
    {
        yearsText.text = "Years: " + Mathf.Floor(GameManager.Instance.yearsPassed);
        temperature.value = GameManager.Instance.CurrentTemperature;

        float tempPercentage = temperature.value / GameManager.Instance.MaxTemperature;
        currentY = Mathf.Lerp(startY, endY, tempPercentage);
        //Move thermometer fill down with the rate of the temperature
    }

    public void EnableGameOverMenu()
    {
        foreach (var UIelement in inGameUI)
        {
            UIelement.gameObject.SetActive(false);
        }
        endGameText.text = GameManager.Instance.EndGameMessage;
        gameOver.gameObject.SetActive(true);
        GameManager.Instance.gameOver = true;
    }

    public void DisableGameOverMenu()
    {
        foreach (var UIelement in inGameUI)
        {
            UIelement.gameObject.SetActive(true);
        }
        gameOver.gameObject.SetActive(false);
        GameManager.Instance.Reset();
    }


}
