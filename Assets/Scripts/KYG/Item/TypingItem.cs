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

    // �̺�Ʈ ����
    public delegate void UsingItem();
    public static event UsingItem UseItem; 

    // Start is called before the first frame update
    void Start()
    {
        // ���
        dialogue = "* �ƽ� Ŀ�Ǹ� ���̴�.  * HP�� ���� ȸ���Ǿ���.";
        StartCoroutine(Typing(dialogue));

    }
    private void Update()
    {
        
    }

    // Ÿ���� ȿ��
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
