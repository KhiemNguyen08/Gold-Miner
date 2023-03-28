using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;
using System.Xml;

public class GameController : Singleton<GameController>
{

    public float time;
    float m_curTime;
    //Transform enemy;
    UIManager m_ui;
    //SHopManager m_sm;
    //GameObject itemObj;
    public Animator animator;
    //public int tagetScore;
    int m_level;
    MineralManager m_LevelObj;
    public MineralManager LevelObj { get => m_LevelObj; }
    public int Level { get => m_level; }

    private string fileName = "_level_";

    private string path;
    public override void Awake()
    {
        MakeSingleton(false);
        // Prefs.tagetScore = tagetScore;
        Prefs.tagetScore += 400;

    }
    public override void Start()
    {
        fileName = "_level_";
        Time.timeScale = 1f;
        m_curTime = time;
        Debug.Log(Prefs.tagetScore);
        m_ui = FindObjectOfType<UIManager>();
        //m_ui.itemDialog.ShowDialog(true);
        if (Prefs.amountIem > 0)
        {
            Debug.Log("shopitem[3,1]" + Prefs.amountIem);
            m_ui.itemDialog.ShowDialog(true);
        }
        m_ui.SetScoreText(Prefs.bestScore.ToString());
        //SaveJson();
        //tạo level cho game
        //MineralManager[] levelPrefabs = LevelManager.Ins.levelPrefabs;
        //if (levelPrefabs != null && levelPrefabs.Length > 0)
        //{
        //    MineralManager levelPrefab = levelPrefabs[LevelManager.Ins.CurLevel];
        //    if (levelPrefab != null)
        //    {
        //        m_level = LevelManager.Ins.CurLevel;
        //        m_LevelObj = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
        //        m_LevelObj = levelPrefab;
        //        m_LevelObj = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
        //    }
        //}
        // SaveJson();
        // LoadJson();
        //Save();
        // Load();
        m_ui.SetLevelText("0" + (m_level + 1).ToString());
        m_ui.SetTagetText("" + Prefs.tagetScore);
        StartCoroutine(TimeCountingDown());
    }
    public void ScoreIncrement(int x)
    {
        Prefs.bestScore += x;
        m_ui.SetScoreText(Prefs.bestScore.ToString());
    }
    public void Next()
    {
        // Save();
        SceneManager.LoadScene("Shop");
    }
    //đếm ngược thời gian
    IEnumerator TimeCountingDown()
    {

        while (m_curTime > 0)
        {
            yield return new WaitForSeconds(1f);
            m_curTime--;
            m_ui.SetTimeText(m_curTime.ToString());
            if (m_curTime <= 0)
            {
                if (Prefs.bestScore >= Prefs.tagetScore)
                {
                    animator.SetBool("win", true);
                    Time.timeScale = 0f;
                    m_ui.dialog.SetdialogContent("Hoàn thành cấp độ !");
                    m_ui.dialog.SetScoreDialogText("Điểm số : " + Prefs.bestScore.ToString());
                    m_ui.dialog.SetTagetDialogText("Mục tiêu : " + Prefs.tagetScore.ToString());
                    m_ui.dialog.ShowDialog(true);
                }
                else
                {
                    PlayerPrefs.DeleteAll();
                    Debug.Log("Backmenu");
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }
    private Save SaveGameObj()
    {
        Save save = new Save();
        for (int i = 0; i < m_LevelObj.minerals.Count; i++)
        {
            save.mineral_score.Add(m_LevelObj.minerals[i].score);
            save.mineral_slowdown.Add(m_LevelObj.minerals[i].slowDown);
            save.mineral_sprite.Add(m_LevelObj.minerals[i].GetComponent<SpriteRenderer>().sprite);
            save.mineral_changsprite.Add(m_LevelObj.minerals[i].enemychange);
            save.mineral_positionX.Add(m_LevelObj.minerals[i].transform.position.x);
            save.mineral_positionY.Add(m_LevelObj.minerals[i].transform.position.y);
        }
        return save;
    }
    public void SaveJson()
    {
        Save savejson = new Save();
        
        for (int i = 0; i < m_LevelObj.minerals.Count; i++)
        {
            savejson.mineral_score.Add(m_LevelObj.minerals[i].score);
            savejson.mineral_slowdown.Add(m_LevelObj.minerals[i].slowDown);
            savejson.mineral_sprite.Add(m_LevelObj.minerals[i].GetComponent<SpriteRenderer>().sprite);
            savejson.mineral_changsprite.Add(m_LevelObj.minerals[i].enemychange);
            savejson.mineral_positionX.Add(m_LevelObj.minerals[i].transform.position.x);
            savejson.mineral_positionY.Add(m_LevelObj.minerals[i].transform.position.y);
        }
        
        string json = JsonUtility.ToJson(savejson);
        File.WriteAllText(Application.dataPath + "/level.json", json);
        Debug.Log("saveJson");
    }
    public void LoadJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/level.json");
        Save loadjson = JsonUtility.FromJson<Save>(json);
        for (int i = 0; i < m_LevelObj.minerals.Count; i++)
        {
            loadjson.mineral_score.Add( m_LevelObj.minerals[i].score);
        }
       
        Debug.Log("loadJson");
    }
    public void Save()
    {
        Save save = SaveGameObj();
        //Đường dẫn lưu file xml
        path = getPath() + ".xml";
        //Tạo file XmlDocument
        XmlDocument xmlDoc = new XmlDocument();
        //Tạo 1 element
        XmlElement elmRoot = xmlDoc.CreateElement("level");
        //Thêm element vào document
        xmlDoc.AppendChild(elmRoot);
        #region
        //for (int i = 0; i < m_LevelObj.minerals.Count; i++)
        //{
        //    XmlElement elmChild_ID = xmlDoc.CreateElement("ID");
        //    XmlElement elmChild_Score = xmlDoc.CreateElement("score");
        //    XmlElement elmChild_Slowdown = xmlDoc.CreateElement("slowdown");
        //    XmlElement elmChild_Sprite = xmlDoc.CreateElement("sprite");
        //    XmlElement elmChild_changenemy = xmlDoc.CreateElement("enemychange");
        //    XmlElement elmChild_positionX = xmlDoc.CreateElement("positionX");
        //    XmlElement elmChild_positionY = xmlDoc.CreateElement("positionY");

        //    elmChild_ID.InnerText = "" + i;
        //    //elmChild_Score.InnerText = "" + m_LevelObj.minerals[i].score;
        //    elmChild_Score.InnerText = save.score.ToString();
        //    Debug.Log("save_diem" + m_LevelObj.minerals[i].score);
        //    Debug.Log("save_diem" + save.score);
        //   // elmChild_Slowdown.InnerText = "" + m_LevelObj.minerals[i].slowDown;
        //    elmChild_Slowdown.InnerText = save.slowdown.ToString();
        //   // elmChild_Sprite.InnerText = "" + m_LevelObj.minerals[i].GetComponent<SpriteRenderer>().sprite;
        //    //elmChild_Sprite.InnerText = save.enemy.ToString();
        //   // elmChild_changenemy.InnerText = "" + m_LevelObj.minerals[i].enemychange.ToString();
        //    //elmChild_changenemy.InnerText = save.changenemy.ToString();
        //   // elmChild_positionX.InnerText = "" + m_LevelObj.minerals[i].transform.position.x;
        //    elmChild_positionX.InnerText = save.positionX.ToString();
        //    Debug.Log("save_vitri_X" + save.positionX);

        //    // elmChild_positionY.InnerText = "" + m_LevelObj.minerals[i].transform.position.y;
        //    elmChild_positionY.InnerText = save.positionY.ToString();
        //    Debug.Log("save_vitri_Y" + m_LevelObj.minerals[i].transform.position.y);
        //    //Thêm element vào document
        //    elmRoot.AppendChild(elmChild_ID);
        //    elmRoot.AppendChild(elmChild_Score);
        //    elmRoot.AppendChild(elmChild_Slowdown);
        //    elmRoot.AppendChild(elmChild_Sprite);
        //    elmRoot.AppendChild(elmChild_changenemy);
        //    elmRoot.AppendChild(elmChild_positionX);
        //    elmRoot.AppendChild(elmChild_positionY);
        //}
        #endregion
        XmlElement mineral, score, slowdown, sprite, changesprite, positionX, positionY;
        //Debug.Log("m" + save.mineral_positionX.Count);
        for (int i = 0; i < save.mineral_positionX.Count; i++)
        {
            mineral = xmlDoc.CreateElement("mineral");
            score = xmlDoc.CreateElement("score");
            slowdown = xmlDoc.CreateElement("slowdown");
            sprite = xmlDoc.CreateElement("sprite");
            changesprite = xmlDoc.CreateElement("changesprite");
            positionX = xmlDoc.CreateElement("positionX");
            positionY = xmlDoc.CreateElement("positionY");

            score.InnerText = save.mineral_score[i].ToString();
            slowdown.InnerText = save.mineral_slowdown[i].ToString();
            sprite.InnerText = save.mineral_sprite[i].ToString();
            changesprite.InnerText = save.mineral_changsprite[i].ToString();
            positionX.InnerText = save.mineral_positionX[i].ToString();
            positionY.InnerText = save.mineral_positionY[i].ToString();

            //xmlDoc.AppendChild(mineral);
            mineral.AppendChild(score);
            mineral.AppendChild(slowdown);
            mineral.AppendChild(sprite);
            mineral.AppendChild(changesprite);
            mineral.AppendChild(positionX);
            mineral.AppendChild(positionY);
            elmRoot.AppendChild(mineral);
        }

        //Sau khi thiết lập thông tin thì chúng ta lưu file và đóng file
        StreamWriter outStream = File.CreateText(path);
        xmlDoc.Save(outStream);
        outStream.Close();
        Debug.Log("Save game information successful");
    }
    public void Load()
    {
        //Đường dẫn file
        path = getPath() + ".xml";
        //Tải file lên
        Save save = new Save();
        XmlReader reader = XmlReader.Create(path);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(reader);
        //Lấy NodeList của node cha
        XmlNodeList ObjectData = xmlDoc.GetElementsByTagName("level");
        XmlNodeList mineral = xmlDoc.GetElementsByTagName("mineral");
        // duyệt danh sách của node cha -> “level”
        Debug.Log("objdata" + m_LevelObj.minerals.Count);
        for (int i = 0; i < mineral.Count; i++)
        {
            XmlNode dataChild = mineral.Item(i);
            XmlNodeList allChildNode = dataChild.ChildNodes;
            for (int j = 0; j < allChildNode.Count; j++)
            {
                XmlNode ID = allChildNode.Item(j);
                //Lấy dữ liệu trong node
                Debug.Log("data _: " + ID.InnerText);
            }
            #region
            //    Duyệt danh sách của node con -> “ID”
            //    for (int j = 0; j < allChildNode.Count; j++)
            //    {
            //XmlNode dataChild = ObjectData.Item(i);
            //    //XmlNodeList allChildNode = dataChild.ChildNodes;
            //        XmlNode ID = allChildNode.Item(j);
            //        /// XmlNode Score = allChildNode.Item(j);
            //        // XmlNode Score = allChildNode.Item(m_LevelObj.minerals[i].score);
            //        // XmlNode Slowdown = allChildNode.Item(int.Parse(m_LevelObj.minerals[j].slowDown));
            //        //Lấy dữ liệu trong node
            //        Debug.Log("data " + ID.InnerText);
            //    }
            //}
            //Instantiate(m_LevelObj, new Vector3(0, 0, 0), Quaternion.identity);
            //Đóng file

            #endregion
            reader.Close();
        }
    }
    private string getPath()
    {

#if UNITY_EDITOR

        return Application.dataPath + "/Resources" + fileName + (m_level + 1);

#elif UNITY_ANDROID

                    return Application.persistentDataPath+fileName;

#elif UNITY_IPHONE

                    return Application.persistentDataPath+”/”+fileName;

#else

                    return Application.dataPath +”/”+ fileName;

#endif

    }
}

