using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Temperature : MonoBehaviour
{
    public delegate void TemperatureChanged(int amount);
    public TemperatureChanged temperatureIncremented;
    public TemperatureChanged temperatureDecremented;

    [SerializeField]
    private int maxTemperature = 140;
    [SerializeField]
    private int minTemperature = -40;
    [SerializeField]
    private int maxSolidTemperature = 10;
    [SerializeField]
    private int minGasTemperature = 90;
    private int currentTemperature = 20;

    private bool canModifyTemperature = true;
    [SerializeField]
    private int canModifyTemperatureCooldown = 2;


    public int CurrentTemperature{ get => currentTemperature; set => currentTemperature = value; }
    public int MaxTemperature => maxTemperature;
    public int MinTemperature => minTemperature;
    public int TemperatureToSolidify => maxSolidTemperature;
    public int TemperatureToVaporize => minGasTemperature;

    public void Increase(int amount)
    {
        if(!canModifyTemperature) return;
        UpdateTemperature(amount);

        temperatureIncremented.Invoke(amount);

    }

    public void Decrease(int amount)
    {
        if(!canModifyTemperature) return;
        UpdateTemperature(amount);

        temperatureDecremented.Invoke(amount);

    }

    public void UpdateTemperature(int amount){
        StartCoroutine(ModifyTemperatureCooldown());

        currentTemperature += amount;
        currentTemperature = Mathf.Clamp(currentTemperature, minTemperature, maxTemperature);
    }

    private IEnumerator ModifyTemperatureCooldown(){
        canModifyTemperature = false;
        yield return new WaitForSeconds(canModifyTemperatureCooldown);
        canModifyTemperature = true;
    }
}
