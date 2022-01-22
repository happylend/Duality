﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_input 
{
    public float Left { get => Input.GetAxisRaw("Left"); }
    public float Right { get => Input.GetAxisRaw("Right"); }

    /// <summary>
    /// 监测是否按下
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    /// <summary>
    /// 监测是否长按
    /// </summary>
    /// <returns></returns>
    public bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }
    // Start is called before the first frame update

}
