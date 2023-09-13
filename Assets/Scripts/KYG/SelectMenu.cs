using UnityEngine;

public class SelectMenu : MonoBehaviour
{
    public GameObject menuBtn;
    public GameObject yellowBtn;
    public GameObject menuCanvas;

    private void Update()
    {
        if(BattleUIManager.I.isClicked == true && GameManager.I.Player.MoveWithMask == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.I.PlaySFX("UISelect");
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
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            yellowBtn.gameObject.SetActive(false);
        }
    }
    
}

