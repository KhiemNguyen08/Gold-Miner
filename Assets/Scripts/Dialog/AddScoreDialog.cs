using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AddScoreDialog : MonoBehaviour
{
    public Text addScoreTetx;

    public void SetScoreText(string txt)
    {
        if (addScoreTetx)
            addScoreTetx.text = txt;
    }
    public void ShowAddScoreDialog(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
