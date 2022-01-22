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


    List<Transform> canvasObj = new List<Transform>();
    int i = 0;

    void Start()
    {
        Time.timeScale = 0;
        i= 0;
        startCanvas = this.transform.parent;
        source = this.transform.GetComponent<AudioSource>();
    }

    //点击
    public void OnPointerClick(PointerEventData eventData)
    {
        //开始
        if (start)
        {

            if (source)
            {
                source.Play();
            }
            Time.timeScale = 1;
            foreach (Transform child in startCanvas)
            {
                Animation anim = child.GetComponent<Animation>();
                if (anim)
                {
                    anim.Play();
                }

            }
            EventCenter.GetInstance().EventTrigger("BeginGame", 0);
        }

        //退出
        if (exit)
        {
            Application.Quit();
        }


    }

    void Update()
    {
        

    }

    //进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (start || exit)
        {
            this.GetComponent<Image>().color = Color.red;
        }


    }


    //离开
    public void OnPointerExit(PointerEventData eventData)
    {
        if (start || exit)
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }

    void DestoryUI()
    {
        MoveCam.startMove = true;
        foreach (Transform child in startCanvas)
        {
            canvasObj.Add(child);
        }

        for (int i = 3; i > 0; i--)
        {
            Debug.Log(canvasObj[i - 1]);
            canvasObj[i - 1].gameObject.SetActive(false);
        }


    }
}
