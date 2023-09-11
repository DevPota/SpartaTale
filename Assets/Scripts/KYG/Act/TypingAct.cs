using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingAct : MonoBehaviour
{
    public Text information;
    public GameObject ActCanvas;
    public Button actBtn;
    string dialogue;

    bool isTyping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTyping)
        {
            StartTyping();
            
        }
 
    }

    void StartTyping()
    {
        isTyping = true;
        dialogue = "* 박종민 매니저님 1 공격력(Atk) 1 방어(Def)  * 가장 쉬운 적이다.  * 공격을 가할 때 그가 입는 피해가 최대 1이다.";
        StartCoroutine(Typing(dialogue));
    }

    IEnumerator Typing(string talk)
    {
        while (true)
        {
            information.text ="";

            talk = talk.TrimEnd();

            if (talk.Contains(" ")) talk = talk.Replace("  ", "\n");

            for (int i = 0; i < talk.Length; i++)
            {
                information.text += talk[i];
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(2f);

            isTyping = false;
            information.gameObject.SetActive(false);
            actBtn.gameObject.SetActive(true);
            ActCanvas.gameObject.SetActive(false);
        };
        

    }
}
