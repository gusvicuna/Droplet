using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureUI : MonoBehaviour
{
    private Text temperatureText;

    private Temperature dropletTemperature;

    // Start is called before the first frame update
    void Start()
    {
        temperatureText = GetComponent<Text>();
        dropletTemperature = GameObject.FindGameObjectWithTag("Player").GetComponent<Temperature>();

    }

    // Update is called once per frame
    void Update()
    {
        temperatureText.text = $"{dropletTemperature.CurrentTemperature} °C";
    }
}
