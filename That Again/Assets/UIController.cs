using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : Singleton<UIController>
{
    public TextMeshProUGUI yearsText;
    public Slider temperature;
    public Button Reset;

    // Update is called once per frame
    void Update()
    {
        float years = GameManager.Instance.gameTime / 10f;
        yearsText.text = "Years: " + Mathf.Floor(years);
        temperature.value = GameManager.Instance.CurrentTemperature;
    }
}
