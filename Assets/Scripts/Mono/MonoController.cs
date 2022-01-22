using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono的管理者
/// 1.生命周期函数
/// 2.事件
/// 3.协程
/// </summary>
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    private event UnityAction awakeEvent;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEvent != null)
            updateEvent();
    }

    private void Awake()
    {
        if (awakeEvent != null)
            awakeEvent();
    }

    /// <summary>
    /// 提供给外部 添加无法移除对象的函数
    /// </summary>
    /// <param name="obj">移除的物体</param>
    public void AddDontDestroyOnLoad(GameObject obj)
    {
        DontDestroyOnLoad(obj);
    }

    /// <summary>
    /// 给外部提供的 添加帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    /// <summary>
    /// 提供给外部 用于移除帧更新事件函数
    /// </summary>
    /// <param name="fun"></param>
    public void  RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }

    /// <summary>
    /// 提供给外部 用于添加Awake事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddAwakeListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    /// <summary>
    /// 提供给外部 用于移除Awake事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveAwakeListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
    
}
