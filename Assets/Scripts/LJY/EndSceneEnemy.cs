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
    }

    void Update()
    {
        if (playerTransform.position.x >= 11 && isStartTalking == false)
        {
            isStartTalking = true;
            panel.SetActive(true);
            Talk(talkDataId);
            Debug.Log(talkIdx + "firstupdate");
        }

        else if(talkIdx == 1)
        {
            panel.SetActive(false);
            tmp.IsActive = true;
            tmp.speed = 60.0f;
            transform.position = Vector3.MoveTowards(transform.position, EndPoint, tmp.speed * Time.deltaTime);
            Debug.Log("talkIdx in isStartTalking" + talkIdx + " " + talkText.text);
        }
        
        if(talkIdx == 1 && transform.position == EndPoint)
        {
            panel.SetActive(true);
            tmp.IsActive = false;
            Debug.Log("talkIdx in Endpoint" + talkIdx + " " + talkText.text);
            Talk(talkDataId);
        }

        else if(talkIdx >= 2)
        {
            Talk(talkDataId);
            Debug.Log("talkIdx Idx>=2 else" + talkIdx + " " + talkText.text);
        }
    }

    public void NextTalk()
    {
        talkIdx++;

        Talk(talkDataId);
        Debug.Log("talkIdx in Endpoint" + talkIdx + " " + talkText.text);
    }

    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(talkDataId, talkIdx);

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
