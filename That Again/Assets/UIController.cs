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
    public Button Reset;

    // Update is called once per frame
    void Update()
    {
        float years = GameManager.Instance.gameTime / 10f;
        yearsText.text = "Years: " + Mathf.Floor(years);
        temperature.value = GameManager.Instance.CurrentTemperature;
    }

    public void EnableGameOverMenu()
    {
        gameOver.gameObject.SetActive(true);
        GameManager.Instance.gameOver = true;
    }

    public void DisableGameOverMenu()
    {
        gameOver.gameObject.SetActive(false);
        GameManager.Instance.Reset();
    }


}
