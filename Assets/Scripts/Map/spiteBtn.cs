using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiteBtn : MonoBehaviour
{
    public GameObject[] spites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            foreach (var item in spites)
            {
                item.SetActive(true);
                if(item.AddComponent<BoxCollider2D>().attachedRigidbody.gameObject.tag == "Player")
                {
                    EventCenter.GetInstance().EventTrigger("Restart", 1);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            foreach (var item in spites)
            {
                item.SetActive(false);
            }
        }
    }
}
