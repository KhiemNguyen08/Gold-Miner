using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthDialog : MonoBehaviour
{
    public void ShowStrengthDialog(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
