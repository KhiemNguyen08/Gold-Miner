using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timeText;
    public Text levelText;
    public Text scoreText;
    public Text tagetText;
    public Dialog dialog;
    public AddScoreDialog addScore;
    public Text addSocreText;
    public StrengthDialog strengthDialog;
    public ItemDialog itemDialog;

    
    public void SetTimeText(string txt)
    {
        if (timeText)
            timeText.text = txt;
    }public void SetLevelText(string txt)
    {
        if (levelText)
            levelText.text = txt;
    }
    public void SetScoreText(string txt)
    {
        if (scoreText)
            scoreText.text = txt;
    }
    public void SetTagetText(string txt)
    {
        if (tagetText)
            tagetText.text = txt;
    } 
   
   
    
}
