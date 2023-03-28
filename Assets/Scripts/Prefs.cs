using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs 
{
    public static bool hasNewBest;
    public static void SetBool(bool isTrue,string key)
    {
        if (isTrue)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }
    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }
    public static int bestScore
    {
        set
        {
            if (PlayerPrefs.GetInt(PrefsConst.BEST_SCORE,0)<value || PlayerPrefs.GetInt(PrefsConst.BEST_SCORE, 0) >value)
            {
                hasNewBest = true;
                PlayerPrefs.SetInt(PrefsConst.BEST_SCORE, value);
            }
            else
            {
                hasNewBest = false;
            }
        }
        get => PlayerPrefs.GetInt(PrefsConst.BEST_SCORE, 0);
    }
    public static int amountIem
    {
        set
        {
            if (PlayerPrefs.GetInt(PrefsConst.AMOUNT_ITEM, 0) < value || PlayerPrefs.GetInt(PrefsConst.AMOUNT_ITEM, 0) > value)
            {
                //hasNewBest = true;
                PlayerPrefs.SetInt(PrefsConst.AMOUNT_ITEM, value);
            }
            else
            {
                //hasNewBest = false;
            }
        }
        get => PlayerPrefs.GetInt(PrefsConst.AMOUNT_ITEM, 0);
    }
    public static int sachda_Item
    {
        set
        {
            if (PlayerPrefs.GetInt(PrefsConst.SACHDA_ITEM, 0) < value || PlayerPrefs.GetInt(PrefsConst.SACHDA_ITEM, 0) > value)
            {
                //hasNewBest = true;
                PlayerPrefs.SetInt(PrefsConst.SACHDA_ITEM, value);
            }
            else
            {
                //hasNewBest = false;
            }
        }
        get => PlayerPrefs.GetInt(PrefsConst.SACHDA_ITEM, 0);
    }
    public static int comayman_Item
    {
        set
        {
            if (PlayerPrefs.GetInt(PrefsConst.COMAYMAN_ITEM, 0) < value || PlayerPrefs.GetInt(PrefsConst.COMAYMAN_ITEM, 0) > value)
            {
                //hasNewBest = true;
                PlayerPrefs.SetInt(PrefsConst.COMAYMAN_ITEM, value);
            }
            else
            {
                //hasNewBest = false;
            }
        }
        get => PlayerPrefs.GetInt(PrefsConst.SACHDA_ITEM, 0);
    }
    public static int strength_Item
    {
        set
        {
            if (PlayerPrefs.GetInt(PrefsConst.STRENGTH_ITEM, 0) < value || PlayerPrefs.GetInt(PrefsConst.STRENGTH_ITEM, 0) > value)
            {
                //hasNewBest = true;
                PlayerPrefs.SetInt(PrefsConst.STRENGTH_ITEM, value);
            }
            else
            {
                //hasNewBest = false;
            }
        }
        get => PlayerPrefs.GetInt(PrefsConst.STRENGTH_ITEM, 0);
    }
    public static int tagetScore
    {
        set
        {
            if (PlayerPrefs.GetInt(PrefsConst.TAGET_SCORE,0)<value)
            {
               // hasNewBest = true;
                PlayerPrefs.SetInt(PrefsConst.TAGET_SCORE, value);
            }
            else
            {
                //hasNewBest = false;
            }
        }
        get => PlayerPrefs.GetInt(PrefsConst.TAGET_SCORE, 0);
    }
    public static bool IsLevelUnlocked(int level)
    {
        return GetBool(PrefsConst.lEVEL_UNLOCKED + level);
    }
}
