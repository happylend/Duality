﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ConB : MonoBehaviour
{
    // Start is called before the first frame update
    public Player_input input { get; set; }
    public static bool CamMove = false;
    public static float height,Preheight;
    public static Vector3 surV = Vector3.zero;

    [Header("people State")]
    public float speed = 5f;
    public float Dragspeed = 1.5f;
    public float PushSpeed = 2.5f;
    public float Jumpforce = 60f;
    public float Dam = 0f;

    [Header("the Other")]
    public GameObject PlayerA;

    private Rigidbody2D m_Rigidbody;
    private float Health = 120f;
    private float pushTranstion = 0;
    private float pullTranstion = 0;
    //是否在地面
    private bool IsOnGround = true;
    //是否在触碰距离
    private bool IsTouch = false;
    private GameObject Pillar;

    //动画控制器
    private Animator animator;

    //private float Preheight;
   
    /// <summary>
    /// 是否能拉
    /// </summary>
    private bool IsPull
    {
        get
        {
            bool temp = input.GetRightPullKey() && IsOnGround;
            return temp;
        }
    }
    /// <summary>
    /// 是否能推
    /// </summary>
    private bool IsPush
    {
        get
        {
            bool temp = input.GetRightPushKey() && IsOnGround;
            return temp;

        }
    }



    // Start is called before the first frame update
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    
    }
    void Start()
    {
        input = new Player_input();
        Preheight = 16.15f;
        surV = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
       
       height = transform.position.y;
        
       if(height-Preheight>3&& !CamMove)
       {
           CamMove = true;         
           Preheight += 3f;          
       }

       
        if (Health >= 0)
            Health -= Dam * Time.deltaTime;
        else
        {
            Debug.Log(this.name + "寄！");
            //死亡
            Map.CanMove = false;
        }
        if (Map.CanMove)
            Move();
    }

    /// <summary>
    /// 人物移动函数
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    private void Move()
    {
        float h = input.Right;
        //如果移动
        if (h != 0 && !IsTouch)
        {

            transform.Translate(transform.right * Time.deltaTime * h * speed);//Time.deltaTime 表示上一帧的时间

            //设置翻转
            //Debug.Log("h是" + h);
            //播放跑步动画
            animator.SetFloat("FloorB", h);
        }
        //如果不移动
        else
        {
            //退出跑步
            animator.SetFloat("FloorB", 0);
        }

        //跳跃
        if (IsOnGround && input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * Jumpforce);
            //播放跳跃动画
        }
        //掉落动画
        else if (!IsOnGround && h == 0)
        {
            animator.SetBool("Fall", true);
        }
        //推拉柱子
        else if (IsTouch)
        {
            //Debug.Log(h);
            //拉
            if (h > 0)
            {
                if (IsPull)
                {
                    Pillar.transform.Translate(transform.right * Time.deltaTime * h * Dragspeed);
                    transform.Translate(transform.right * Time.deltaTime * h * Dragspeed);
                    pullTranstion = 1;
                    animator.SetFloat("FloorB", h + pullTranstion);

                }
                else if (!IsPull)
                {
                    transform.Translate(transform.right * Time.deltaTime * h * speed);
                    animator.SetFloat("FloorB", h);
                    pushTranstion = 0;
                }
            }
            if (h < 0)
            {
                if (IsPush)
                {
                    pushTranstion = -1f;
                    Pillar.transform.Translate(transform.right * Time.deltaTime * h * PushSpeed);
                    transform.Translate(transform.right * Time.deltaTime * h * PushSpeed);
                    animator.SetFloat("FloorB", h + pushTranstion);

                }
                else if (!IsPush)
                {
                    pushTranstion = 0;
                    transform.Translate(transform.right * Time.deltaTime * h * speed);
                    animator.SetFloat("FloorB", h);
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //如果在地面上
        if (collision.gameObject.tag == "Ground")
        {
            //设置在地面上
            IsOnGround = true;
            //关闭跳跃动画
            animator.SetBool("Fall", false);
        }
        if (collision.gameObject.tag == "Dead")
        {
            //结束游戏
            GameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pillar" && IsOnGround)
        {
            //设置为可以推拉
            IsTouch = true;
            Pillar = collision.transform.parent.gameObject;
        }
        //如果是复活点
        if (collision.gameObject.tag == "Resurgence")
        {
            Debug.Log("复活点");
            Transform Rs = collision.transform;
            if (Rs.GetComponent<ResurrectionPoints>().id == ResurrectionPoints.Id.Black && surV.y < Rs.position.y)
            {
                surV = Rs.position;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //离开地面
        if (collision.gameObject.tag == "Ground")
        {

            //设置在地面上
            IsOnGround = false;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pillar")
        {
            //设置为不可以推拉
            IsTouch = false;
        }
    }
    /// <summary>
    /// 人物死亡
    /// </summary>
    private void Die()
    {
        //播放死亡动画
        //播放死亡音效
        //设置几秒后游戏结束
        Invoke("GameOver", 3.5f);
    }

    private void GameOver()
    {
        Debug.Log("重启");
        //双方碰过复活点
        if (Player_ConA.surV != Vector3.zero && Player_ConB.surV != Vector3.zero)
        {

            var Cam = GameObject.FindWithTag("MainCamera");
            Vector3 CamTran = Cam.transform.position;
            //如果两个复活点相同
            if (Player_ConA.surV.y == Player_ConB.surV.y)
            {
                PlayerA.transform.position = Player_ConA.surV;
                this.transform.position = Player_ConB.surV;

                Cam.transform.position = new Vector3(CamTran.x, Player_ConA.surV.y - 1f, CamTran.z);
            }
            else
            {
                var surY = transform.position.y < PlayerA.transform.position.y ? transform.position.y : PlayerA.transform.position.y;
                transform.position = new Vector3(Player_ConA.surV.x, surY, Player_ConA.surV.z);
                PlayerA.transform.position = new Vector3(Player_ConB.surV.x, surY, Player_ConB.surV.z);

                Cam.transform.position = new Vector3(CamTran.x, surY - 1f, CamTran.z);

            }
        }
        else
        {
            EventCenter.GetInstance().EventTrigger("Restart", 1);

        }//重新加载当前场景
    }
}
