using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public Dictionary<int, Dictionary<string,int>> levels;

    public PlayerData(string name){
        this.name = name;
        levels = new Dictionary<int, Dictionary<string, int>>();
        for (int i = 1; i <= 2; i++)
        {
            levels[i] = new Dictionary<string, int>();
            levels[i]["deathCounts"] = 0;
            levels[i]["score"] = 0;
            levels[i]["whiteFlowersCount"] = 0;
            levels[i]["yellowFlowersCount"] = 0;
            levels[i]["blueFlowersCount"] = 0;
            levels[i]["timeFromStart"] = 0;
            levels[i]["finalMass"] = 0;
        }
    }

    public override string ToString(){
        string finalString = $"{name} : ";
        for (int i = 1; i <= 2; i++)
        {
            finalString += $"{i}: deathCounts: {levels[i]["deathCounts"]}, score: {levels[i]["score"]}, whiteFlowersCount: {levels[i]["whiteFlowersCount"]}, yellowFlowersCount: {levels[i]["yellowFlowersCount"]}, blueFlowersCount: {levels[i]["blueFlowersCount"]}, timeFromStart: {levels[i]["timeFromStart"]}, finalMass: {levels[i]["finalMass"]},  ";
        }
        return finalString;
    }
}
