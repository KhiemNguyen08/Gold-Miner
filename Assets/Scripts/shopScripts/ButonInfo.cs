using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceTxt;
    public Text QuatityTxt;
    public GameObject Shopmanager;

    void Update()
    {
        PriceTxt.text = "$" + Shopmanager.GetComponent<SHopManager>().shopItems[2, ItemID].ToString();
        Shopmanager.GetComponent<SHopManager>().shopItems[3, 1] = Prefs.amountIem;
        Shopmanager.GetComponent<SHopManager>().shopItems[3, 2] = Prefs.sachda_Item;
        Shopmanager.GetComponent<SHopManager>().shopItems[3, 3] = Prefs.strength_Item;
        Shopmanager.GetComponent<SHopManager>().shopItems[3, 4] = Prefs.comayman_Item;
        QuatityTxt.text = Shopmanager.GetComponent<SHopManager>().shopItems[3, ItemID].ToString();

    }
}
