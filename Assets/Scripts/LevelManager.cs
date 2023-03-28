using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : Singleton<LevelManager>
{
    //khai báo 
    public  MineralManager[] levelPrefabs;
    int m_curLevel;
    //public xml

    public int CurLevel { get => m_curLevel; set => m_curLevel = value; }
}
