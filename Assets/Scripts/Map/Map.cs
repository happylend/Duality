using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private GameObject map;
    public static bool CanMove = true;
    public static bool Win = false;
    public static bool Dead = false;
    public GameObject[] canvas;
    // Start is called before the first frame update
    void Awake()
    {
        //EventCenter.GetInstance().AddEventListener("BeginGame", BeginGame);
        EventCenter.GetInstance().AddEventListener("Restart", Restart);
        InitWord(1);
    }
    
    /// <summary>
    /// 进入游戏
    /// </summary>
    /// <param name="key"></param>
    public void InitWord(object key)
    {
        //加载地图
        map = PoolMgr.GetInstance().GetObjAsyc("Prefabs/Level_1", new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    /// <param name="key"></param>
    public void BeginGame(object key)
    {
        //摄像机开始往上移动

        //缓存几秒
        //人物可以移动
        Map.CanMove = true;

    }

    /// <summary>
    /// 重载游戏
    /// </summary>
    /// <param name="key"></param>
    public void Restart(object key)
    {
        MusicMgr.GetInstance().StopBKMusic();
        CanMove = false;
        //清理缓存池
        PoolMgr.GetInstance().Clear();
        //清理地图
        Destroy(map);
        //重置胜利
        Win = false;
        //重新加载地图
        map = PoolMgr.GetInstance().GetObjAsyc("Prefabs/Level_1", new Vector3(0f, 0f, 0f), Quaternion.identity);
   
       
        foreach (var item in canvas)
        {
            item.SetActive(true);
        }

        MusicMgr.GetInstance().PlayBkMusic("1075815-pinofas-Return to Old Town");
        //MoveCam.startMove = true;
    }

    public void DestoryListener()
    {
        EventCenter.GetInstance().RomoveEventListener("BeginGame", BeginGame);
    }
}
