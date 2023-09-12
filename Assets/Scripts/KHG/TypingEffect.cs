using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] GameObject btn;
    public Text tx;
    string m_text = "°­Èñ°Ç\n ³Í ¾ÆÁ÷ Åð½ÇÇÏ±â¿£ ÀÌ¸£´Ù...";

    void Start()
    {
        StartCoroutine(_typing());
    }
    IEnumerator _typing() 
    {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < m_text.Length; i++) 
        {
            tx.text = m_text.Substring(0, i);

            yield return new WaitForSeconds(0.17f);
        }

        btn.SetActive(true);
    }

    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
}
