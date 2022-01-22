using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private GameObject map;
    public static bool CanMove = false;
    public static bool Win = false;
    public static bool Dead = false;
    // Start is called before the first frame update
    void Awake()
    {
        EventCenter.GetInstance().AddEventListener("BeginGame", BeginGame);
    }
    
    /// <summary>
    /// 进入游戏
    /// </summary>
    /// <param name="key"></param>
    public void InitWord(object key)
    {
        //加载地图
        map = PoolMgr.GetInstance().GetObjAsyc("Prefabs/Level_0_to_Level_1", new Vector3(0f, 0f, 0f), Quaternion.identity);
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
        CanMove = true;

    }

    /// <summary>
    /// 重载游戏
    /// </summary>
    /// <param name="key"></param>
    public void Restart(int key)
    {
        CanMove = false;
        //清理缓存池
        PoolMgr.GetInstance().Clear();
        //清理地图
        Destroy(map);
        //重置胜利
        Win = false;
        //重新加载地图
        map = PoolMgr.GetInstance().GetObjAsyc("", new Vector3(0f, 0f, 0f), Quaternion.identity);
        //摄像机移动

        //人物可以移动
        CanMove = true;
    }

    public void DestoryListener()
    {
        EventCenter.GetInstance().RomoveEventListener("BeginGame", BeginGame);
    }
}
