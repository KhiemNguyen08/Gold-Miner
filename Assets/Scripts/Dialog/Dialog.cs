using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text dialogContentText;
    public Text scoreText;
    public Text tagetText;
    
    public void ShowDialog(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
    public void SetScoreDialogText(string txt)
    {
        scoreText.text = txt;
    }public void SetTagetDialogText(string txt)
    {
        tagetText.text = txt;
    }
    public void SetdialogContent(string content)
    {
        if (dialogContentText)
        {
            dialogContentText.text = content;
        }
    }
}
