﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public static bool startMove = false;
    public static bool CamMove = false;
    private float speed = 2.9f;
    public GameObject Cam;
    public Transform ParentTran;
    public Transform[] players;
    // Start is called before the first frame update
    void Start()
    {
        Cam = this.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(startMove);
        if (startMove)
        {
            Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, new Vector3(0, 19, -10), 5 * Time.fixedDeltaTime);
            if (Cam.transform.position == new Vector3(0, 19, -10))
            {
                startMove = false;
                CamMove = true;
                //人物可以移动
                Map.CanMove = true;
            }
        }
        if(CamMove)
        {   
            if(Player_ConA.CamMove && Player_ConB.CamMove)
            {
                Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, new Vector3(0, 19 + speed * Player_ConA.CamCount, -10), 6 * Time.fixedDeltaTime);
                if(Cam.transform.position == new Vector3(0, 19+speed*Player_ConA.CamCount, -10))
                {            
                    Player_ConA.CamMove = Player_ConB.CamMove = false;
                }
                /*
                else if(Cam.transform.position == new Vector3(0, 75.2f, -10)|| Cam.transform.position == new Vector3(0, 83f, -10)||
                    Cam.transform.position == new Vector3(0, 91.5f, -10)) Player_ConA.CamMove = Player_ConB.CamMove = false;
                */
                
            }
            
            
            //人物移动



        }

    }
}
