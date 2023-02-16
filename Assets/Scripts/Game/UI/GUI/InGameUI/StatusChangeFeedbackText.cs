using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusChangeFeedbackText : MonoBehaviour
{
    private TextMeshProUGUI statusChangeText;
    private Animator animator;

    [SerializeField]
    public Color damageColor;
    [SerializeField]
    public Color healColor;
    [SerializeField]
    public Color contaminationColor;
    [SerializeField]
    public Color heatColor;
    [SerializeField]
    public Color coldColor;
    [SerializeField]
    public Color scoreColor;

    void Awake()
    {
        animator = GetComponent<Animator>();
        statusChangeText = GetComponent<TextMeshProUGUI>();
    }

    public void ShowText(int amount, string type){
        switch(type){
            case "damage":
                statusChangeText.color = damageColor;
                statusChangeText.text = $"-{amount}";
                break;
            case "heal":
                statusChangeText.color = healColor;
                statusChangeText.text = $"+{amount}";
                break;
            case "contamination":
                statusChangeText.color = contaminationColor;
                if(amount>0){
                    statusChangeText.text = $"+{amount}";
                }
                else{
                    statusChangeText.text = $"{amount}";
                }
                break;
            case "temperature":
                if(amount>0){
                    statusChangeText.color = heatColor;
                    statusChangeText.text = $"+{amount}";
                }
                else{
                    statusChangeText.color = coldColor;
                    statusChangeText.text = $"{amount}";
                }
                break;
            case "score":
                statusChangeText.color = scoreColor;
                if(amount>0){
                    statusChangeText.text = $"+{amount}";
                }
                else{
                    statusChangeText.text = $"-{amount}";
                }
                break;
        }
        animator.SetTrigger("Appear");
    }
}
