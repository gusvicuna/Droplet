using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropletContamination : MonoBehaviour
{
    private const int MAXCONTAMINATION = 100;
    public int contaminationPercent = 0;
    public int contaminationToBeContaminated = 50;

    public UnityEvent OnFullContaminated;

    // Start is called before the first frame update
    void Start()
    {
        OnFullContaminated = new UnityEvent();

        contaminationPercent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddContamination(int contaminationAmount)
    {
        contaminationPercent += contaminationAmount;
        if (contaminationPercent < MAXCONTAMINATION) return;
        contaminationPercent = MAXCONTAMINATION;
        OnFullContaminated.Invoke();
    }

    public void LoseContamination(int contaminationLosed)
    {
        contaminationPercent -= contaminationLosed;
        if (contaminationPercent <= 0)
        {
            contaminationPercent = 0;
        }
    }
}
