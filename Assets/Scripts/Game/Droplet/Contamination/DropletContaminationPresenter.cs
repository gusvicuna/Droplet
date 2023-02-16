using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletContaminationPresenter : MonoBehaviour
{
    private const int MINROTATIONOFPANEL = 0;
    private const int MAXROTATIONOFPANEL = 90;
    private const float SMOOTHTIMEOFROTATION = 1;

    private Contamination contamination;
    private StatusChangeFeedbackText statusChangeText;

    private float rotationBeforeUpdate = 0;

    [SerializeField]
    private Transform contaminationPanelTransform;

    private void Awake() {
        statusChangeText = GetComponentInChildren<StatusChangeFeedbackText>();
        contamination = GetComponent<Contamination>();
    }


    void Start()
    {
        if(contamination != null){
            contamination.contaminationChanged += OnContaminationChanged;
        }
        UpdateView(0);
    }

    private void OnDestroy() {
        if(contamination != null){
            contamination.contaminationChanged -= OnContaminationChanged;
        }
    }

    public void UpdateView(int amount){
        if(contamination == null) return;
        StartCoroutine(UpdateArrow());
        statusChangeText.ShowText(amount, "contamination");
    }

    public void OnContaminationChanged(int amount){
        UpdateView(amount);
    }

    public IEnumerator UpdateArrow(){
        float timeElapsed = 0f;
        rotationBeforeUpdate = contaminationPanelTransform.eulerAngles.z;
        while(timeElapsed < SMOOTHTIMEOFROTATION){
            float targetRotation = MINROTATIONOFPANEL + (MAXROTATIONOFPANEL - MINROTATIONOFPANEL) * contamination.ContaminationPercent / 100;
            float rotation = Mathf.Lerp(rotationBeforeUpdate, targetRotation, timeElapsed/SMOOTHTIMEOFROTATION);
            contaminationPanelTransform.eulerAngles = new Vector3(0,0,rotation);

            yield return 0;
            timeElapsed += Time.deltaTime;
        }
    }
}
