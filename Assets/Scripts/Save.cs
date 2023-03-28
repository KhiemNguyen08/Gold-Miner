using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public  class Save 
{
    //public int score;
    //public float slowdown;
    //public Sprite enemy;
    //public Sprite changenemy;
    //public float positionX, positionY;
    public List<int> mineral_score = new List<int>();
    public List<float> mineral_slowdown= new List<float>();
    public List<Sprite> mineral_sprite = new List<Sprite>();
    public List<Sprite> mineral_changsprite = new List<Sprite>();
    public List<float> mineral_positionX = new List<float>();
    public List<float> mineral_positionY = new List<float>();
}
