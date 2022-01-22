using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private AudioSource source;
    public bool start;
    public bool exit;

    Transform startCanvas;

    void Start()
    {
        startCanvas = this.transform.parent;
        source = GetComponent<AudioSource>();
    }

    //点击
    public void OnPointerClick(PointerEventData eventData)
    {
        //开始
        if (start)
        {
            source.Play();
            foreach (Transform child in startCanvas)
            {
                Animation anim = child.GetComponent<Animation>();
                anim.Play();
            }
            EventCenter.GetInstance().EventTrigger("BeginGame", 0);
        }

        //退出
        if (exit)
        {
            Application.Quit();
        }


    }

    //进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = Color.red;

    }


    //离开
    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = Color.white;

    }

    void DestoryUI()
    {
        startCanvas.gameObject.SetActive(false);
    }
}
