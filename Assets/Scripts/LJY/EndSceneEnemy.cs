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
    bool isComingManager = false;
    
    public TextMeshProUGUI talkText;

    public Vector3 startPoint;

    Transform playerTransform;
    TileMap_PlayerMovement tmp;
    int talkDataId = 1;

    [SerializeField]
    public Vector3 EndPoint;

    
    Animator anim;

    void Start()
    {
        playerTransform = player.transform;
        startPoint = transform.position;
        tmp = transform.GetComponent<TileMap_PlayerMovement>();
        anim = transform.GetComponent<Animator>();
        anim.SetFloat("Horizontal", 1.0f);
    }

    void Update()
    {
        if (playerTransform.position.x >= 11 && isStartTalking == false)
        {
            isStartTalking = true;
            panel.SetActive(true);
            Talk(talkDataId);
        }

        else if(talkIdx == 1)
        {
            panel.SetActive(false);
            tmp.IsActive = true;
            tmp.speed = 20.0f;
            transform.position = Vector3.MoveTowards(transform.position, EndPoint, tmp.speed * Time.deltaTime);

            if(transform.position == EndPoint)
            {
                isComingManager = true;
            }

            if (isComingManager)
            {
                panel.SetActive(true);
                anim.SetBool("IsMove", false);
                tmp.IsActive = false;
                Talk(talkDataId);
            }
        }
        
    }

    public void NextTalk()
    {
        talkIdx++;

        Talk(talkDataId);
    }

    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(talkDataId, talkIdx);

        if (talkData == null)
        {
            panel.SetActive(false);
            SceneManager.LoadScene(6);
            return;
        }

        if (isStartTalking == true)
        {
            talkText.text = talkData;
        }
    }
}
