using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickNoItemBtn : MonoBehaviour
{
    public Button noItemBtn;
    public GameObject itemCanvas;

    Action listener = null;

    void Start()
    {
        noItemBtn = GetComponent<Button>();
        noItemBtn.Select();
    }

    public void Init(Action action)
    {
        listener = action;
    }

    public void OnBtnClick()
    {
        //noItemBtn.gameObject.SetActive(false);
        //itemCanvas.gameObject.SetActive(false);
        //noItemBtn.gameObject.SetActive(true);

        listener?.Invoke();
    }
}
