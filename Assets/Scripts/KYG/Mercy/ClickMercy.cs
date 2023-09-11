using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMercy : MonoBehaviour
{
    public Button MercyClickBtn;
    public GameObject MercyCanvas;

    // Start is called before the first frame update
    void Start()
    {
        MercyClickBtn = GetComponent<Button>();
    }

    // Update is called once per frame
    public void OnBtnClick()
    {
        MercyCanvas.gameObject.SetActive(false);

    }
}
