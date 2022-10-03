using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private Text timeText;

    private DropletScore dropletScore;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<Text>();
        dropletScore = GameObject.FindGameObjectWithTag("Player").GetComponent<DropletScore>();

    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = dropletScore.timeFromStart.ToString();
    }
}
