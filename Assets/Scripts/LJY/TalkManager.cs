using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(0, new string[] { "꽤 바빴었지, 응?", "가장 나쁜 사람이라도 바뀔 수 있을까...?", "하 하 하 하...", "네가 한발짝이라도 더 다가온다면...", "그 뒤에 무슨 일이 일어날지 \"정말\" 궁금하지 않을걸.", "뭐.", "죄송해요, 매니저님.", "이게 바로 제가 약속을 안 하는 이유에요." });
        //talkData.Add(0, new string[] {"0text", "1text", "2text", "text3", "4text", "5text", "6text", "7text"});
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }

        else
        {
            return talkData[id][talkIndex];
        }
    }
}
