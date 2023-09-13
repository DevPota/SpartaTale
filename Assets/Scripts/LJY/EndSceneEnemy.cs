using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndSceneEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject panel;

    public TalkManager talkManager;
    public int talkIdx = 0;
    bool isStartTalking = false;

    public TextMeshProUGUI talkText;

    public Vector3 startPoint;

    [SerializeField]
    public Vector3 EndPoint;

    [SerializeField]
    public GameObject panelTriggerZone;

    Transform playerTransform;
    TileMap_PlayerMovement tmp;

    void Start()
    {
        playerTransform = player.transform;
        startPoint = transform.position;
        tmp = transform.GetComponent<TileMap_PlayerMovement>();
    }

    void Update()
    {
        if (playerTransform.position.x >= 11 && isStartTalking == false)
        {
            isStartTalking = true;
            panel.SetActive(true);

            Talk(1);
        }

        if(isStartTalking && talkIdx == 1)
        {
            panelTriggerZone.SetActive(false);
            tmp.IsActive = true;
            tmp.speed = 8.0f;
            transform.position = Vector3.MoveTowards(transform.position, EndPoint, tmp.speed * Time.deltaTime);
        }
    }

    public void NextTalk()
    {
        talkIdx++;

        Talk(1);
    }

    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(1, talkIdx);

        if (talkData == null)
        {
            panel.SetActive(false);
            //Load Credit Scene
            //SceneManager.LoadScene(3);
            return;
        }

        if (isStartTalking == true)
        {
            talkText.text = talkData;
        }
    }
}
