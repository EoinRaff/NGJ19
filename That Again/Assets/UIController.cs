using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI yearsText;
    public Slider temperature;

    // Update is called once per frame
    void Update()
    {
        float years = GameManager.Instance.gameTime / 10f;
        yearsText.text = "Years: " + Mathf.Floor(years);
        temperature.value = GameManager.Instance.CurrentTemperature;
    }
}
