using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 1.可以提供给外部添加 帧更新事件的方法
/// 2.可以提供给外部添加 协程的方法
/// </summary>
public class MonoMgr : BaseManager<MonoMgr>
{
    private MonoController controller;

    public MonoMgr()
    {
        //保证MonoController对象的唯一性
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();//添加组件 只会进入一次
    }


    public void AddDontDestroyOnLoad(GameObject obj)
    {
        controller.AddDontDestroyOnLoad(obj);
    }

    /// <summary>
    /// 给外部提供的 添加帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

   
    /// <summary>
    /// 提供给外部 用于移除帧更新事件函数
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }

    /// <summary>
    /// 提供给外部 用于添加Awake事件函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddAwakeListener(UnityAction fun)
    {
        controller.AddAwakeListener(fun);
    }

    /// <summary>
    /// 提供给外部 用于移除Awake事件函数
    /// </summary>
    /// <param name="fun"></param>
    /// <returns></returns>
    public void RemoveAwakeListener(UnityAction fun)
    {
        controller.RemoveAwakeListener(fun);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }

    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }

    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }

    public void StopAllCoroutines()
    {
        controller.StopAllCoroutines();
    }
}
