using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
    public GameObject menuBtn;
    public GameObject yellowBtn;
    public GameObject menuCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (yellowBtn.activeSelf)
            {
                menuCanvas.SetActive(true);
            }
            else
            {
                menuCanvas.SetActive(false);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            yellowBtn.gameObject.SetActive(true);
            Debug.Log("충돌");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            yellowBtn.gameObject.SetActive(false);
            Debug.Log("충돌X");
        }
    }
}

