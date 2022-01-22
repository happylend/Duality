using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换模块
/// 知识点
/// 1.场景异步加载
/// 2.协程
/// 3.委托
/// </summary>
public class SceneMgr : BaseManager<SceneMgr>
{
    /// <summary>
    /// 切换场景 同步
    /// </summary>
    /// <param name="name"></param>
    public  void LoadScene(string name, UnityAction fun)
    {
        //场景同步加载
        SceneManager.LoadScene(name);

        //加载完过后 才会执行fun
        fun();
    }

    /// <summary>
    /// 提供给外部 异步加载
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    public void LoadSceneAsyn(string name, UnityAction fun, int number)
    {
        MonoMgr.GetInstance().StartCoroutine(ReallyloadSceneAsyn(name, fun,  number));
    }

    /// <summary>
    ///协程异步加载场景 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    /// <param name="number">场景序列</param>
    /// <returns></returns>
    private IEnumerator ReallyloadSceneAsyn(string name, UnityAction fun, int number)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        //得到场景加载的进度 ao.prograss
        /*
         while(!ao.isDone)
         {
            //更新进度条
            yield return ao.progress
          }
         
         */
         //事件中心 向外分发 进度情况 外面想用就用
        EventCenter.GetInstance().EventTrigger("场景加载", number);
        yield return ao;

        fun();
    }
}
