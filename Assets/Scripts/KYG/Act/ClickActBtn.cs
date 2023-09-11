using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickActBtn : MonoBehaviour
{
    public Button actBtn;
    public Button actCheckBtn;

    // Start is called before the first frame update
    void Start()
    {
        actBtn = GetComponent<Button>();
    }

    // Update is called once per frame
    public void OnBtnClick()
    {
        actBtn.gameObject.SetActive(false);
        actCheckBtn.gameObject.SetActive(true);
    }
}
