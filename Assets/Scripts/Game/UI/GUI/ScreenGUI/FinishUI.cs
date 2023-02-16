using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI finalMassText;
    public TextMeshProUGUI deathsText;
    public Text whiteFlowersText;
    public Text yellowFlowersText;
    public Text blueFlowersText;

    private DropletScore dropletScore;
    private Health dropletHealth;
    // Start is called before the first frame update
    void Start()
    {
        dropletScore = GameObject.FindGameObjectWithTag("Player").GetComponent<DropletScore>();
        dropletHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    public void ShowScore(){
        scoreText.text = dropletScore.score.ToString();
        timeText.text = dropletScore.timeFromStart.ToString();
        finalMassText.text = dropletHealth.CurrentHealth.ToString();
        deathsText.text = dropletScore.deathCounts.ToString();
        whiteFlowersText.text = $"x {dropletScore.whiteFlowersCount}";
        yellowFlowersText.text = $"x {dropletScore.yellowFlowersCount}";
        blueFlowersText.text = $"x {dropletScore.blueFlowersCount}";
    }
}
