using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject panel;

    public TalkManager talkManager;
    public int talkIdx = 0;
    bool isStartTalking = false;

    public TextMeshProUGUI talkText;

    Transform cameraTransform;

    void Start()
    {
        cameraTransform = playerCamera.transform;
    }

    void Update()
    {
        if(cameraTransform.position.x >= 29 && isStartTalking == false)
        {
            playerCamera.GetComponent<PlayerCamera>().cameraMoveSpeed = 0f;
            isStartTalking = true;
            panel.SetActive(true);

            Talk(0);

            //말풍선 옆 얼굴 띄우기 (상황에 맞는 얼굴 태그 설정 - string 설정에서 추가 예)
            //말풍선 내용 띄우기 (spacebar 클릭할 때마다 다음 대사로 넘어감)
            //
        }
    }

    public void NextTalk()
    {
        talkIdx++;

        Talk(0);
    }

    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(0, talkIdx);

        if(talkData == null)
        {
            //LoadScene으로 전투장면으로 넘어가면 됨 .
            panel.SetActive(false);
            SceneManager.LoadScene(3);
            return;
        }

        if (isStartTalking == true)
        {
            talkText.text = talkData;
        }
    }
}
