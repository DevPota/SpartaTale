using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int talkDataId = 0;

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
        if(cameraTransform.position.x >= 29.85 && isStartTalking == false)
        {
            playerCamera.GetComponent<PlayerCamera>().cameraMoveSpeed = 0f;
            isStartTalking = true;
            panel.SetActive(true);

            Talk(talkDataId);
        }
    }

    public void NextTalk()
    {
        talkIdx++;

        Talk(talkDataId);
    }

    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(0, talkIdx);

        if(talkData == null)
        {
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
