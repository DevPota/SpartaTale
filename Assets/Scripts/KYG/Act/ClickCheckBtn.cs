using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCheckBtn : MonoBehaviour
{
    public Button actCheckBtn;
    public Text information;


    // Start is called before the first frame update
    void Start()
    {
        actCheckBtn = GetComponent<Button>();
    }


    // Update is called once per frame
    public void OnBtnClick()
    {
        actCheckBtn.gameObject.SetActive(false);
        information.gameObject.SetActive(true);
        
    }

}
