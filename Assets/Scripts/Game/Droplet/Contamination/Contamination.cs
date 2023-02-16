using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Contamination : MonoBehaviour
{
    public delegate void ContaminationChanged(int amount);
    public ContaminationChanged contaminationChanged;
    public event Action ContaminationAtHalf;
    public event Action ContaminationLessThanHalf;
    public event Action ContaminationAtFull;

    private const int MAXCONTAMINATION = 100;
    private const int MINCONTAMINATION = 0;
    private int contaminationPercent = 0;
    private int contaminationToBeContaminated = 50;
    private bool isImmune = false;
    [SerializeField]
    private int immuneCooldown = 2;
    private bool isContaminated = false;

    public int ContaminationPercent{ get => contaminationPercent; set => contaminationPercent = value; }
    public int MaxContamination => MAXCONTAMINATION;
    public int MinContamination => MINCONTAMINATION;
    public int ContaminationToBeContaminated => contaminationToBeContaminated;

    public void AddContamination(int contaminationAmount)
    {
        if(!isImmune){
            StartCoroutine(ImmuneCooldown());

            contaminationPercent += contaminationAmount;
            contaminationPercent = Mathf.Clamp(contaminationPercent, MINCONTAMINATION, MAXCONTAMINATION);

            UpdateContamination(contaminationAmount);

            if(contaminationPercent >= contaminationToBeContaminated && !isContaminated){
                isContaminated = true;
                ContaminationAtHalf.Invoke();
            }
            if(contaminationPercent < contaminationToBeContaminated && isContaminated){
                isContaminated = false;
                ContaminationLessThanHalf.Invoke();
            }

            if (contaminationPercent < MAXCONTAMINATION) return;

            ContaminationAtFull.Invoke();
        }
    }

    public void UpdateContamination(int amount){
        contaminationChanged(amount);
    }

    private IEnumerator ImmuneCooldown(){
        isImmune = true;
        yield return new WaitForSeconds(immuneCooldown);
        isImmune = false;
    }
}
