using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_input 
{
    public float Left { get => Input.GetAxisRaw("Left"); }
    public float Right { get => Input.GetAxisRaw("Right"); }


    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }
    // Start is called before the first frame update

}
