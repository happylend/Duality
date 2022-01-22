using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BasePanel : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// 面板基类
    /// 帮助我们通过代码快速的找到所有的子控件
    /// 方便我们在子类处理逻辑
    /// 节省工作量
    /// 提供显示 或者隐藏的行为
    /// </summary>

    //通过里式转换原则 来存储所有控件
    private Dictionary<string, List<UIBehaviour> > controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        Button[] btns = this.GetComponentsInChildren<Button>();
        FindChildrenControl<Text>();
        FindChildrenControl<Image>();
        FindChildrenControl<Button>();
        //单选框
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        //输入框
        FindChildrenControl<InputField>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 显示自己
    /// </summary>
    public virtual void ShowMe()
    {

    }
    
    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void HideMe()
    {

    }

    /// <summary>
    /// 按钮响应
    /// 虚函数 用于子类继承重写
    /// </summary>
    /// <param name="BtnName"></param>
    protected virtual void OnClick(string BtnName)
    {

    }

    /// <summary>
    /// 单选框
    /// 虚函数
    /// 用于子类继承重写
    /// </summary>
    /// <param name="BtnName"></param>
    protected virtual void OnValueChange(string toggleName, bool value)
    {

    }
    /// <summary>
    /// 得到对应名字的 对应控件脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controName"></param>
    /// <returns></returns>
    protected T GetControl<T>(string controName) where T : UIBehaviour
    {
        //是否有这个名字的控件
        if(controlDic.ContainsKey(controName))
        {
            for (int i = 0; i < controlDic[controName].Count; i++)
            {
                //是对应的类型
                if (controlDic[controName][i] is T)
                    return controlDic[controName][i] as T;
            }
        }
        return null;

    }
    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>() where T: UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        
        for (int i = 0; i < controls.Length; i++)
        {
            string objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
                controlDic[objName].Add(controls[i]);
            else
                controlDic.Add(objName ,new List<UIBehaviour>() {controls[i]});
            //如果是控件按钮 
            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(()=>
                {
                    //Lambda 无参数
                    OnClick(objName);
                });
            }
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    //Lambda 无参数
                    OnValueChange(objName, value);
                });
            }
        }


    }
}
