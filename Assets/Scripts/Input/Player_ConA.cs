using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ConA : MonoBehaviour
{
    public Player_input input { get; set; }

    public float speed = 5f;

    public float jumpforce = 60f;
    private Rigidbody2D m_Rigidbody;
    //是否在地面
    private bool IsOnGround = true;

    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        input = new Player_input();
    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }

    /// <summary>
    /// 人物移动函数
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    private void Move()
    {
        float h = input.Left;
        //如果移动
        if(h!=0)
        {

            transform.Translate(transform.right*Time.deltaTime*h*speed);//Time.deltaTime 表示上一帧的时间
            
            //设置翻转
            
            /*
            if (h > 0) GetComponent<SpriteRenderer>().flipX = false;
            else GetComponent<SpriteRenderer>().flipX = true;
            */

            //播放跑步动画

        }
        //如果不移动
        else
        {
            //退出跑步
        }

        //跳跃
        if(IsOnGround&&input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
            //播放跳跃动画
            //播放音效
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //如果在地面上
        if(collision.gameObject.tag == "Ground")
        {
            //关闭跳跃动画

            //设置在地面上
            IsOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //离开地面
        if (collision.gameObject.tag == "Ground")
        {
            //开启跳跃动画

            //设置在地面上
            IsOnGround = false;
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
        //重新加载当前场景
    }
}
