using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletTemperaturePresenter : MonoBehaviour
{
    [SerializeField]
    private Transform contaminationPanelTransform;

    private const int MINROTATIONOFPANEL = 0;
    private const int MAXROTATIONOFPANEL = 70;
    private const float SMOOTHROTATIONTIME = 1;

    private float rotationBeforeUpdate = 0;

    private Temperature temperature;
    private StatusChangeFeedbackText statusChangeText;

    private void Awake() {
        temperature = GetComponent<Temperature>();
        statusChangeText = GetComponentInChildren<StatusChangeFeedbackText>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(temperature != null){
            temperature.temperatureIncremented += OnTemperatureIncremented;
            temperature.temperatureDecremented += OnTemperatureDecremented;
        }
        UpdateView();
    }

    private void OnDestroy() {
        if(temperature != null){
            temperature.temperatureIncremented -= OnTemperatureIncremented;
            temperature.temperatureDecremented -= OnTemperatureDecremented;
        }
    }

    private void OnTemperatureIncremented(int amount){
        UpdateView();
        statusChangeText.ShowText(amount, "temperature");
    }

    private void OnTemperatureDecremented(int amount){
        UpdateView();
        statusChangeText.ShowText(amount, "temperature");
    }

    public void UpdateView(){
        if(temperature == null) return;
        StartCoroutine(UpdateArrow());
    }

    public IEnumerator UpdateArrow(){
        float timeElapsed = 0f;
        rotationBeforeUpdate = contaminationPanelTransform.eulerAngles.z;
        while(timeElapsed < SMOOTHROTATIONTIME){
            float targetRotation = ((temperature.CurrentTemperature - temperature.MinTemperature) * (MAXROTATIONOFPANEL - MINROTATIONOFPANEL) / (temperature.MaxTemperature - temperature.MinTemperature)) + MINROTATIONOFPANEL;
            float rotation = Mathf.Lerp(rotationBeforeUpdate, targetRotation, timeElapsed/SMOOTHROTATIONTIME);
            contaminationPanelTransform.eulerAngles = new Vector3(0,0,rotation);

            yield return 0;
            timeElapsed += Time.deltaTime;
        }
    }
}
