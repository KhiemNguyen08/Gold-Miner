using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMove : MonoBehaviour
{
    public float min_Z = -75f, max_Z = 75f;
    public float rotate_Speed = 0.5f;
    float rotate_Angle;
    bool rotate_Right;
    public bool canRotate;
    public float move_Speed = 5f;
    float initial_MoveSpeed;
    public float min_Y = -5f;
    public float left_X;
    public float right_X;
    float initial_Y;
    bool moveDown;
    RopeReneder ropeRenederer;
    GameController m_gc;
    public AudioSource aus;
    public AudioClip item;
    public AudioClip throw_hook;
    public AudioClip add_score;
    float _slowDown;
    int _Score;
    Transform enemy;
    UIManager m_ui;
    bool flag;
    public Animator animator;
    public GameObject explosive;
    private void Awake()
    {
        ropeRenederer = GetComponent<RopeReneder>();
        m_gc = FindObjectOfType<GameController>();
        m_ui = FindObjectOfType<UIManager>();
    }
    void Start()
    {
        initial_Y = transform.position.y;
        Check_Strength();
        initial_MoveSpeed = move_Speed;
        Debug.Log("Check_strength: " + move_Speed);
        Debug.Log("phao:" + Prefs.amountIem);
        Debug.Log("sachda:" + Prefs.sachda_Item);
        Debug.Log("strength:" + Prefs.strength_Item);
        Debug.Log("comayman: " + Prefs.comayman_Item);
        canRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        GetInput();
        MoveRope();
        //move_Speed = update_moveSpeed;
     
    }
    //quay móc
    void Rotate()
    {
        if (!canRotate)
            return;
        if (rotate_Right)
        {
            rotate_Angle += rotate_Speed + Time.deltaTime;
        }
        else
        {
            rotate_Angle -= rotate_Speed + Time.deltaTime;
        }
        transform.rotation = Quaternion.AngleAxis(rotate_Angle, Vector3.forward);
        if (rotate_Angle >= max_Z)
        {
            rotate_Right = false;
        }
        else if (rotate_Angle <= min_Z)
        {
            rotate_Right = true;
        }
    }
    //nhấp chuột phải
    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canRotate)
            {
                canRotate = false;
                if (aus && throw_hook)
                {
                    aus.PlayOneShot(throw_hook);
                }
                moveDown = true;
                //  animator.SetBool("pull", true);
            }
        }
    }
    // thu dãn sợi dây
    public void MoveRope()
    {
        if (canRotate)
            return;
        if (!canRotate)
        {
            animator.SetBool("rotate", true);
            Vector3 temp = transform.position;

            if (moveDown)
            {
                temp -= transform.up * move_Speed * Time.deltaTime;

            }
            else
            {

                temp += transform.up * (move_Speed - _slowDown) * Time.deltaTime;

            }

            transform.position = temp;
            if (temp.y <= min_Y || temp.x <= -9 || temp.x >= 10)
            {
                flag = true;
                moveDown = false;
            }
            if (temp.y >= initial_Y)
            {
                canRotate = true;
                move_Speed = initial_MoveSpeed;
                //move_Speed = update_moveSpeed;
                _slowDown = 0;
                animator.SetBool("explosive", false);
                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
                // Destroy(enemy.gameObject);
                flag = false;
                m_ui.addScore.ShowAddScoreDialog(false);
                animator.SetBool("rotate", false);
                m_gc.ScoreIncrement(_Score);
                if (aus && throw_hook)
                {
                    aus.PlayOneShot(add_score);
                }
                m_ui.strengthDialog.ShowStrengthDialog(false);
                ropeRenederer.ReanderLine(temp, false);

            }
            ropeRenederer.ReanderLine(temp, true);
        }

    }
    //xử lý va chạm vs vật thể
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (flag)
            return;
        flag = true;
        if (aus && item)
        {
            aus.PlayOneShot(item);
        }
        if (col.gameObject.CompareTag("?"))
        {
            RandomItem();
            col.GetComponent<SpriteRenderer>().sprite = col.GetComponent<Enemy>().enemychange;

            col.transform.rotation = transform.rotation;
            col.transform.position = transform.position;
            col.transform.SetParent(transform);
            col.transform.localPosition = new Vector3(0, -5, 0);
            enemy = col.transform;
            _slowDown = enemy.GetComponent<Enemy>().slowDown;

            move_Speed =- _slowDown;

        }
        else if (col.gameObject.CompareTag("Exposive"))
        {
            moveDown = false;
            Destroy(col.gameObject);
            if (explosive)
            {
            Instantiate(explosive, transform.position, Quaternion.identity);

            }
            Destroyer.Ins.DestroyThem();
            
            //Destroy(enemy.gameObject);
        }
        else if (col.gameObject.CompareTag("stone"))
        {
            col.GetComponent<SpriteRenderer>().sprite = col.GetComponent<Enemy>().enemychange;
            col.transform.rotation = transform.rotation;
             col.transform.SetParent(transform);
             col.transform.position = transform.position;
            col.transform.localPosition = new Vector3(0, -18, 0);
            //col.transform.localPosition = new Vector3(0,0,0);
            enemy = col.transform;
            Check_SachDa();
            _Score = enemy.GetComponent<Enemy>().score;
            m_ui.addScore.SetScoreText("+" + _Score + "$");
            m_ui.addScore.ShowAddScoreDialog(true);
        }
        else
        {
            col.GetComponent<SpriteRenderer>().sprite = col.GetComponent<Enemy>().enemychange;

            col.transform.rotation = transform.rotation;
            col.transform.position = transform.position;
            col.transform.SetParent(transform);
            if (col.gameObject.CompareTag("300gold"))
            {
                col.transform.localPosition = new Vector3(0, -16, 0);
            }
            else if(col.gameObject.CompareTag("biggold"))
            {
                col.transform.localPosition = new Vector3(0, -27, 0);
            }
            else
            {
                col.transform.localPosition = new Vector3(0, -7, 0);
            }
           
            enemy = col.transform;
            //col.transform.position.y = transform.position.y;
            _slowDown = enemy.GetComponent<Enemy>().slowDown;
            move_Speed =- _slowDown;
            _Score = enemy.GetComponent<Enemy>().score;
            // m_gc.ScoreIncrement(_Score);
            m_ui.addScore.SetScoreText("+" + _Score + "$");
            m_ui.addScore.ShowAddScoreDialog(true);
        }
    }
    public void DestroyObj()
    {

        if (enemy != null)
        {
            animator.SetBool("explosive", true);
            Prefs.amountIem--;
            Destroy(enemy.gameObject);
            m_ui.addScore.ShowAddScoreDialog(false);
            //_slowDown = enemy.GetComponent<Enemy>().slowDown;
            move_Speed += 10;
            moveDown = false;
            m_ui.itemDialog.ShowDialog(false);

        }
    }
    public void Check_SachDa()
    {
        if (Prefs.sachda_Item > 0)
        {
            _slowDown = 3.5f;
            move_Speed =- _slowDown;
            Prefs.sachda_Item--;
        }
        else
        {
            _slowDown = enemy.GetComponent<Enemy>().slowDown;
            move_Speed =- _slowDown;
        }
    }
    public void Check_Strength()
    {
        if (Prefs.strength_Item > 0)
        {
            move_Speed = 7;
            Prefs.strength_Item--;
        }
    }
    public void RandomItem()
    {
        int randomIdx = Random.Range(1, 4);
        switch (randomIdx)
        {
            case 1:
                if (Prefs.comayman_Item > 0)
                {
                    Check_comayman();
                }
                else
                {
                    m_gc.ScoreIncrement(100);
                    //initial_MoveSpeed += 0;
                    Debug.Log("movespeed" + move_Speed);
                    m_ui.addScore.SetScoreText("+" + 100 + "$");
                    m_ui.addScore.ShowAddScoreDialog(true);
                    Debug.Log("1");
                }
                break;
            case 2:
                if (Prefs.comayman_Item > 0)
                {
                    Check_comayman();
                }
                else
                {
                    initial_MoveSpeed += 2;
                    Debug.Log("movespeed" + move_Speed);
                    m_ui.strengthDialog.ShowStrengthDialog(true);
                }


                break;
            case 3:
                if (Prefs.comayman_Item > 0)
                {
                    Check_comayman();
                }
                else
                {
                    int randomscore = Random.Range(100, 300);
                    m_gc.ScoreIncrement(randomscore);
                    initial_MoveSpeed += 1;
                    Debug.Log("movespeed" + move_Speed);
                    m_ui.addScore.SetScoreText("+" + randomscore + "$");
                    m_ui.addScore.ShowAddScoreDialog(true);
                    Debug.Log("3");
                }

                break;
        }
    }
    public void Check_comayman()
    {

        int randomscore = Random.Range(200, 400);
        m_gc.ScoreIncrement(randomscore);
        initial_MoveSpeed += 3;
        Debug.Log("movespeed" + move_Speed);
        m_ui.addScore.SetScoreText("+" + randomscore + "$");
        m_ui.addScore.ShowAddScoreDialog(true);
        Debug.Log("3");
        Prefs.comayman_Item--;


    }
}
