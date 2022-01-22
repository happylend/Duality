using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public static float LeftPer;
    public static float RightPer;
    private float sigmoid(float x)
    {
        return Mathf.Atan(x) / Mathf.PI + 1 / 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
