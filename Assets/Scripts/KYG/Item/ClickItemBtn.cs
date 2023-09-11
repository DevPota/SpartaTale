using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickItemBtn : MonoBehaviour
{
    public Button itemBtn;
    public Text itemTxt;

    // Start is called before the first frame update
    void Start()
    {
        itemBtn = GetComponent<Button>();
        
    }

    // Update is called once per frame
    public void OnBtnClick()
    {
        itemBtn.gameObject.SetActive(false);
        itemTxt.gameObject.SetActive(true);
    }
}
