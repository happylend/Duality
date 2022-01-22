using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 池子中的抽屉容器
/// </summary>
public class PoolData
{
    //池子中对象挂载的父节点
    public GameObject fatherObj;
    //池子中对象挂载的容器
    public List<GameObject> poolList;

    public PoolData(GameObject obj, GameObject poolObj)
    {
        //给池子创建一个父对象，并将它作为pool()对象的子物体
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;

        poolList = new List<GameObject>() {};
        PushObj(obj);

    }

    /// <summary>
    /// 往池子的抽屉里压东西
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        //存起来
        poolList.Add(obj);
        //设置父对象
        obj.transform.parent = fatherObj.transform;
        //失活
        obj.SetActive(false);
    }

    /// <summary>
    /// 从池子的抽屉里取东西
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        GameObject obj = null;
        //取出第一个
        obj = poolList[0];
        poolList.RemoveAt(0);//移除
        //激活物体,让其显示
        obj.SetActive(true);
        //断开父子关系
        obj.transform.parent = null;

        return obj;

    }
}

/// <summary>
/// 缓存池模块
/// 1.Dictionary List
/// 2.GameObject 和 Resources 两个公共类中的API
/// </summary>
public class PoolMgr : BaseManager<PoolMgr>
{
 
    //缓存池容器
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    //根目录
    private GameObject poolObj;
    /// <summary>
    /// 异步加载物品 压入缓存池
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public void GetObj(string name, Vector3 tran, Quaternion ro, UnityAction<GameObject> callBack)
    {

        //缓存池中有物品
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count>0 )
        {
 
            callBack(poolDic[name].GetObj());
        }
        //缓存池中没物品
        else
        {
            //异步加载资源创建对象
            ResMgr.GetInstance().LoadSync<GameObject>(name, (o) =>
            {
                o.name = name;
                callBack(o);
            });

        }

        //return obj;
    }

    /// <summary>
    /// 同步加载物品 压入缓存池
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tran"></param>
    /// <param name="ro"></param>
    /// <returns></returns>
    public GameObject GetObjAsyc(string name, Vector3 tran, Quaternion ro)
    {
        GameObject obj = null;
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            obj = poolDic[name].GetObj();
        }
        //缓存池中没物品
        else
        {
        
            
            //实例化对象
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name), tran, ro);

            //对象名字和池子名字一样
            obj.name = name;
            
        }
        return obj;
    }

    /// <summary>
    /// 压入不需要的东西
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObj(string name, GameObject obj)
    {
        //当没有创建层级时
        if (poolObj == null)
            poolObj = new GameObject("Pool");


        //里面已有容器
        if(poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        //里面无抽屉
        else
        {
            poolDic.Add(name, new PoolData(obj,poolObj));
        }
    }

    /// <summary>
    /// 清除缓存池方法
    /// 用于切换场景
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
    

}
