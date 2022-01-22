using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public static bool startMove = false;
    private GameObject Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(startMove);
        if (startMove)
        {
            Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, new Vector3(0, 19, -10), 5 * Time.fixedDeltaTime);
            if (Cam.transform.position == new Vector3(0, 19, -10))
            {
                startMove = false;
            }
        }
    }
}
