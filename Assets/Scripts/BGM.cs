using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MusicMgr.GetInstance().PlayBkMusic("1075815-pinofas-Return to Old Town");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
