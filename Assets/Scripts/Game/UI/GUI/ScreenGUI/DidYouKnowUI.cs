using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DidYouKnowUI : MonoBehaviour
{
    public List<string> tips;

    private TextMeshProUGUI tipText;
    // Start is called before the first frame update
    void Start()
    {
        tipText = GetComponent<TextMeshProUGUI>();
        tipText.text = tips[Random.Range(0,tips.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
