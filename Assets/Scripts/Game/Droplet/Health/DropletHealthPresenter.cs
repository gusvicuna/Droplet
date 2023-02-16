using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropletHealthPresenter : MonoBehaviour
{
    [SerializeField]
    private Image healthbarImage;
    [SerializeField]
    private Image healthWaveImage;
    [SerializeField]
    private TextMeshProUGUI currentHealthText;
    private StatusChangeFeedbackText statusChangeText;
    private DropletGFX gFX;
    private Health health;

    public int waveOffset = -130;
    public int wavePositionMultiplier = 300;

    public float smoothTime = 2f;

    private float healthBarFillAmountBefore = 0;
    private float waveYPositionBefore = 0;
    private float waveScaleBefore = 0;

    private void Awake() {
        health = GetComponent<Health>();
        statusChangeText = GetComponentInChildren<StatusChangeFeedbackText>();
        gFX = GetComponent<DropletGFX>();
    }

    private void Start()
    {
        if (health != null)
        {
            health.healthDecremented += OnHealthDecremented;
            health.healthIncremented += OnHealthIncremented;
            health.DropletIsImmune += OnDropletImmune;
            health.DropletIsNotImmune += OnDropletNotImmune;
        }
        StartCoroutine(UpdateHealthUI());
    }
    
    private void OnDestroy()
    {
        if (health != null)
        {
            health.healthDecremented -= OnHealthDecremented;
            health.healthIncremented -= OnHealthIncremented;
            health.DropletIsImmune -= OnDropletImmune;
            health.DropletIsNotImmune -= OnDropletNotImmune;
        }
    }
    
    public void OnHealthDecremented(int amount)
    {
        StartCoroutine(UpdateHealthUI());
        statusChangeText.ShowText(amount, "damage");
    }

    public void OnHealthIncremented(int amount){
        StartCoroutine(UpdateHealthUI());
        statusChangeText.ShowText(amount, "heal");
    }
        public IEnumerator UpdateHealthUI(){
        float timeElapsed = 0f;
        float healthProportion = (float)health.CurrentHealth / health.MaxHealth;
        healthBarFillAmountBefore = healthbarImage.fillAmount;
        waveYPositionBefore = healthWaveImage.transform.localPosition.y;
        waveScaleBefore = healthWaveImage.transform.localScale.x;
        
        while( timeElapsed <= smoothTime){
            UpdateView(healthProportion, timeElapsed);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
    }

    public void OnDropletImmune(){
        gFX.SetImmune(true);
    }

    public void OnDropletNotImmune(){
        gFX.SetImmune(false);
    }

    public void UpdateView(float healthProportion, float timeElapsed)
    {
        if (health == null) return;

        UpdateHealthBarUI(healthProportion, timeElapsed);
        UpdateHealthTextUI();
        UpdateHealthWaveUI(healthProportion, timeElapsed);
    }

    private void UpdateHealthWaveUI(float healthProportion, float timeElapsed) {
        float yPositionTarget = (healthProportion * wavePositionMultiplier) + waveOffset;
        float yPosition = Mathf.Lerp(waveYPositionBefore, yPositionTarget, timeElapsed/smoothTime);
        healthWaveImage.transform.localPosition = new Vector2(healthWaveImage.transform.localPosition.x, yPosition);

        float targetScale = Mathf.Sqrt(1 - Mathf.Pow(healthProportion * 2 - 1, 2));
        float scale = Mathf.Lerp(waveScaleBefore, targetScale, timeElapsed/smoothTime);
        healthWaveImage.transform.localScale = new Vector2((float)scale, (float)scale);
    }

    private void UpdateHealthBarUI(float healthProportion, float timeElapsed) {
        healthbarImage.fillAmount = Mathf.Lerp(healthBarFillAmountBefore, healthProportion, timeElapsed/smoothTime);
    }

    private void UpdateHealthTextUI() {
        currentHealthText.text = string.Format("{0} ml", health.CurrentHealth.ToString());
    }

}
