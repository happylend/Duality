using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBtn : MonoBehaviour
{
    public GameObject[] door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (var item in door)
            {
                item.SetActive(false);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (var item in door)
            {
                item.SetActive(true);
            }
        }
    }

}
