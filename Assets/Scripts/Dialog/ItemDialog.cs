using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDialog : MonoBehaviour
{
    public void ShowDialog(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
