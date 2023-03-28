using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SHopManager : Singleton<SHopManager>
{
    public int[,] shopItems = new int[5, 5];
    int coins;
    public Text CoinsText;
    UIManager m_ui;
    //public Text CoinsText;
    public override void Awake()
    {
        MakeSingleton(false);
        shopItems[3, 1]= Prefs.amountIem ;
        shopItems[3, 2] = Prefs.sachda_Item ;
        shopItems[3, 3] = Prefs.strength_Item ;
        shopItems[3, 4] = Prefs.comayman_Item;
    }
    public override void Start()
    {
        coins = PlayerPrefs.GetInt(PrefsConst.BEST_SCORE);
        CoinsText.text = "Coins: " + coins.ToString();
        //ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        //Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;
        //Quatity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }
    //click mua sp
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        int buttonref = ButtonRef.GetComponent<ButonInfo>().ItemID;
        if (coins >= shopItems[2, buttonref])
        {
            coins -= shopItems[2, buttonref];
            shopItems[3, buttonref]++;
            CoinsText.text = "Coins: " + coins.ToString();
            ButtonRef.GetComponent<ButonInfo>().QuatityTxt.text = shopItems[3, buttonref].ToString();
        }
        Prefs.bestScore = coins;
        Prefs.amountIem = shopItems[3, 1];
        Prefs.sachda_Item = shopItems[3, 2];
        Prefs.strength_Item = shopItems[3, 3];
        Prefs.comayman_Item = shopItems[3, 4];
    }
    public void nextLevel()
    {
        LevelManager.Ins.CurLevel++;
        SceneManager.LoadScene("SampleScene");
    }
}
