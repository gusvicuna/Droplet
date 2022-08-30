using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContaminationUI : MonoBehaviour
{
    private Text contaminationText;

    public PlayerContamination playerContamination;

    // Start is called before the first frame update
    void Start()
    {
        contaminationText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        contaminationText.text = playerContamination.contaminationPercent.ToString();
    }
}
