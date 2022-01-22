using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI层级
/// </summary>
public enum E_UI_layer
{
    Bot,
    Mid,
    Top,
    System
}
public class UIManager : BaseManager<UIManager>
{

    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();


    private Transform bot;//底层
    private Transform mid;//中层
    private Transform top;//上层
    private Transform system;//系统层
    //记录UI的Canvas父对象 方便外部可以使用
    public RectTransform canvas;
    public UIManager()
    {
        //找到Canvas
        GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        //面板不被移除
        GameObject.DontDestroyOnLoad(obj);

        //找到各层
        bot = canvas.Find("Bot");
        bot = canvas.Find("Mid");
        bot = canvas.Find("Top");
        bot = canvas.Find("System");


        //创建EventSystem 让其过场景的时候 不被移除
        obj = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }

    /// <summary>
    ///显示面板 
    /// </summary>
    /// <typeparam name="T">面板脚本类型</typeparam>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在那一层</param>
    /// <param name="callback">当面板预设体创建成功后，你想做的事</param>
    public void ShowPanel<T>(string panelName, E_UI_layer layer =  E_UI_layer.Mid, UnityAction<T> callback = null) where T: BasePanel
    {
        //异步加载
        ResMgr.GetInstance().LoadSync<GameObject>("UI/" + panelName, (obj) =>
        {
            //如果已经存在了
            if(panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].ShowMe();
                //处理面板创建完成后的逻辑
                if (callback != null)
                    callback(panelDic[panelName] as T);

                //避免重复加载
                return;
            }
            //作为Canvas的子对象
            //设置相对位置
            Transform father = bot;
            switch(layer)
            {
                case E_UI_layer.Mid:
                    father = mid;
                    break;

                case E_UI_layer.Top:
                    father = top;
                    break;

                case E_UI_layer.System:
                    father = system;
                    break;
            }

            //设置父对象
            obj.transform.SetParent(father);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            //面板显示时可以操作
            //得到预设体身上的面板脚本
            T panel = obj.GetComponent<T>();

            //处理面板创建完成后的逻辑
            if (callback != null)
                callback(panel);

            //虚函数 可重写
            panel.ShowMe();

            //把面板存起来
            panelDic.Add(panelName, panel);



        });
    }

    /// <summary>
    /// 通过层级枚举 得到对应层级的父对象
    /// </summary>
    /// <param name="layer">层级</param>
    /// <returns></returns>
    public Transform GetLeveFather(E_UI_layer layer)
    {
        switch(layer)
        {
            case E_UI_layer.Bot:
                return this.bot;
            case E_UI_layer.Mid:
                return this.mid;
            case E_UI_layer.Top:
                return this.top;
            case E_UI_layer.System:
                return this.system;
        }
        return null;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="panelName"></param>
    public void HidePanel(string panelName)
    {
        if(panelDic.ContainsKey(panelName))
        {
            //虚函数 可重写
            panelDic[panelName].HideMe();

            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 得到显示的已经显示面板
    /// 方便外部使用
    /// </summary>
    public T GetPanel<T>(string name) where T:BasePanel
    {
        //存在
        if (panelDic.ContainsKey(name))
            return panelDic[name] as T;

        return null;
    }

}
