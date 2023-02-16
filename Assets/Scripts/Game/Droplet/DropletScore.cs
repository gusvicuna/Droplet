using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletScore : MonoBehaviour
{
    public int score = 0;

    [Header("Flowers")]
    public int whiteFlowerScore = 10;
    [HideInInspector]
    public int whiteFlowersCount = 0;
    public int yellowFlowerScore = 15;
    [HideInInspector]
    public int yellowFlowersCount = 0;
    public int blueFlowerScore = 20;
    [HideInInspector]
    public int blueFlowersCount = 0;

    [Header("Time")]
    public int maxTimeScore = 300;
    [HideInInspector]
    public bool isOnPause = false;
    [HideInInspector]
    public int timeFromStart = 0;
    private bool canCountUp = true;

    [Header("Deaths")]
    public int deathPenalty = 30;
    [HideInInspector]
    public int deathCounts = 0;
    [Header("Mass")]
    public int finalMassScoreMultiplier = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOnPause & canCountUp) {
            StartCoroutine("CountTime");
        }
    }

    public IEnumerator CountTime(){
        canCountUp = false;
        yield return new WaitForSeconds(1);
        timeFromStart += 1;
        canCountUp = true;
    }

    public void LosePoints(int points){
        score -= points;
        if(score<=0){
            score = 0;
        }
    }

    public void WinPoints(int points){
        score += points;
    }

    public void CalculateScore(int finalMass){
        score += maxTimeScore;
        score += finalMass * finalMassScoreMultiplier;
        LosePoints(timeFromStart);
        LosePoints(deathPenalty * deathCounts);
    }

    public void LoadPlayerData(PlayerData playerData, int level){
        deathCounts = playerData.levels[level]["deathCounts"];
    }
}
