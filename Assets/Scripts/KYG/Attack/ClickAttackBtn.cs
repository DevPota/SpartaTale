using UnityEngine;
using UnityEngine.UI;

public class ClickAttackBtn : MonoBehaviour
{
    public Button attackBtn;
    public GameObject attackCanvas;
    
    void Start()
    {
        attackBtn = GetComponent<Button>();
    }

    public void OnBtnClick()
    {
        attackCanvas.gameObject.SetActive(false);
    }
}
