using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private Text scoreText;

    private DropletScore dropletScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        dropletScore = GameObject.FindGameObjectWithTag("Player").GetComponent<DropletScore>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = dropletScore.score.ToString();
    }
}
