using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingItem : MonoBehaviour
{
    public Text itemTxt;
    public Image itemImage;
    public GameObject ItemCanvas;
    string dialogue;

    // 이벤트 선언
    public delegate void UsingItem();
    public static event UsingItem UseItem; 

    // Start is called before the first frame update
    void Start()
    {
        // 대사
        dialogue = "* 맥심 커피를 마셨다.  * HP가 전부 회복되었다.";
        StartCoroutine(Typing(dialogue));

    }
    private void Update()
    {
        
    }

    // 타이핑 효과
    IEnumerator Typing(string talk)
    {
        itemTxt.text = null;

        if (talk.Contains(" ")) talk = talk.Replace("  ", "\n");

        for (int i = 0; i < talk.Length; i++)
        {
            itemTxt.text += talk[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(6f);
        ItemCanvas.gameObject.SetActive(false);
        itemTxt.gameObject.SetActive(false);

        itemImage.gameObject.SetActive(true);

        if (UseItem != null)
        {
            UseItem();
        }
    }

  
}
