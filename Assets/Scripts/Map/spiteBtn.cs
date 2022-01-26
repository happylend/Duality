using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiteBtn : MonoBehaviour
{
    public GameObject[] spites;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (var item in spites)
            {
                item.SetActive(true);
                if (item.AddComponent<BoxCollider2D>().attachedRigidbody.gameObject.tag == "Player")
                {
                    EventCenter.GetInstance().EventTrigger("Restart", 1);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (var item in spites)
            {
                item.SetActive(false);
            }

        }
    }

}
