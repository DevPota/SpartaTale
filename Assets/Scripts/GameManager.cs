using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GridBrushBase;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public GameObject Mask;
    Vector3 prevMaskPosition;

    public Character Player;

    public Vector3 maskPosition;
    public float maskHalfWidth;
    public float maskHalfHeight;

    [SerializeField] private GameObject StraightPrefab;
    [SerializeField] private GameObject SecondPrefab;
    [SerializeField] private GameObject SecondRotatePrefab;
    [SerializeField] private GameObject ThirdPrefab;
    [SerializeField] private GameObject FifthPrefab;
    [SerializeField] private GameObject SixthPrefab;
    [SerializeField] private GameObject EighthPrefab;
    [SerializeField] private GameObject EleventhPrefab;
    [SerializeField] private GameObject TwelfthShortPrefab;
    [SerializeField] private GameObject TwelfthLongPrefab;
    [SerializeField] private GameObject ThirteenthPrefab;

    [SerializeField] private Vector3   battlePivot;
    [SerializeField] private Vector3[] spawnPosition;

    [SerializeField] private GameObject scriptText;
    private Text script;

    bool end = false;

    public bool IsPlayerTurn { get; private set; } = false;

    public enum Pattern
    {
        First = 1, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth
    }

    Pattern pattern = Pattern.First;

    void Awake()
    {
        I = this;
        Player = GameObject.Find("Player").GetComponent<Character>();
        script = scriptText.GetComponent<Text>();
        GetMask();
        Player.transform.position = maskPosition;
        SetSpawnPosition();
    }

    void Start()
    {
        BattleUIManager.I.Init();
        Player.Init(BattleUIManager.I.SetPlayerKrText, BattleUIManager.I.SetPlayerHpSlider);

        UpdateTurn();
        //Mask.transform.DOShakePosition(0.8f, 0.5f, 10);
        //Invoke("Battle", 0.8f);
    }

    void Update()
    {
        if(Player.Hp == 0 && !end)
        {
            end = true;
            StopAllCoroutines();
            /* HP 0 �Ǹ� �÷��̾� ��ǲ ���ް� �ϰ� �ִϸ��̼� ��� �� ���� ���������� �̵� */
            Mask.SetActive(false);
            Player.transform.DOMove(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0)), 2.0f);
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        Mask.SetActive(false);
        Player.transform.DOMove(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0)), 2.0f);

        yield return new WaitForSeconds(2.2f);
        //SceneManager.LoadScene("GameOverSceneTest");
    }

    [ContextMenu("UpdateTurn")]
    public void UpdateTurn()
    {
        if(IsPlayerTurn == true)
        {
            IsPlayerTurn = false;
            Player.transform.position = battlePivot;
            Battle();
        }
        else
        {
            IsPlayerTurn = true;
            Player.transform.position = Player.ButtonPositions[0];
            pattern += 1;
        }
    }

    public void GetMask()
    {
        maskPosition = Mask.transform.position;
        maskHalfWidth = Mask.transform.localScale.x * 0.5f;
        maskHalfHeight = Mask.transform.localScale.y * 0.5f;
    }

    void SetSpawnPosition()
    {
        spawnPosition[0] = new Vector3(maskPosition.x - maskHalfWidth, maskPosition.y + maskHalfHeight, 0);
        spawnPosition[1] = new Vector3(maskPosition.x , maskPosition.y + maskHalfHeight, 0);
        spawnPosition[2] = new Vector3(maskPosition.x + maskHalfWidth, maskPosition.y + maskHalfHeight, 0);
        spawnPosition[3] = new Vector3(maskPosition.x - maskHalfWidth, maskPosition.y - maskHalfHeight, 0);
        spawnPosition[4] = new Vector3(maskPosition.x, maskPosition.y - maskHalfHeight, 0);
        spawnPosition[5] = new Vector3(maskPosition.x + maskHalfWidth, maskPosition.y - maskHalfHeight, 0);
    }
    void Battle()
    {
        StartCoroutine(pattern.ToString());
    }

    void ChangePattern(Pattern newPattern)
    {
        StopCoroutine(pattern.ToString());
        pattern = newPattern;
        StartCoroutine(pattern.ToString());
    }

    private IEnumerator First()
    {
        yield return new WaitForSeconds(1.0f);

        int boneCount = StraightPrefab.GetComponent<StraightBone>().BoneCount;
        float offsetX = boneCount * 0.3f * 0.5f;
        float offsetY = 0.3f;
        int[] index = { 0, 3, 1, 4, 2, 5 };
        int i = 0;

        while (true)
        {
            Instantiate(StraightPrefab, spawnPosition[index[i++]] - new Vector3(offsetX, offsetY, 0), Quaternion.identity);
            if (i % 2 == 0) offsetX -= boneCount * 0.3f * 0.5f;
            offsetY *= -1;
            if (i == 2) offsetY = -0.8f;
            if (i == 4) offsetY = 0.3f;
            if (i == 6) break;

            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(1.0f);

        ChangePattern(Pattern.Second);
    }

    private IEnumerator Second()
    {

        for(int i = 0; i < 6; i++)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    Instantiate(SecondPrefab, spawnPosition[5] + new Vector3(0, maskHalfHeight, 0), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(SecondRotatePrefab, spawnPosition[5] + new Vector3(0, maskHalfHeight, 0), Quaternion.identity);
                    break;
            }

            yield return new WaitForSeconds(1.3f); 
        }

        ChangePattern(Pattern.Third);
    }
    private IEnumerator Third()
    {
        yield return new WaitForSeconds(1.0f);
        Instantiate(ThirdPrefab, spawnPosition[5] + new Vector3(0, maskHalfHeight, 0), Quaternion.identity);

        yield return new WaitForSeconds(4.5f);

        ChangePattern(Pattern.Fourth);
    }
    private IEnumerator Fourth() //����ũ ũ�� ��ȭ
    {
        Player.gameObject.SetActive(false);
        Mask.transform.DOShakePosition(1.0f, 0.5f, 10);
        Mask.transform.DOScale(new Vector3(maskHalfWidth * 3f, maskHalfHeight * 2.2f, 1.0f), 1.0f);

        yield return new WaitForSeconds(1.0f); 
        
        maskInitialize();//�ٷ� �ʱ�ȭ�ϸ� �� Ŀ���⵵ ���� Mask�޾ƿ� . �� 1�� �Ŀ� �޾ƿ;� �Ѵ�
        script.text = null;
        script.DOText("�� ������ ����Ŭ�� ���۵˴ϴ�", 2.0f);

        yield return new WaitForSeconds(2.0f);

        ChangePattern(Pattern.Fifth);
    }
    private IEnumerator Fifth() //ó���� ���ӿ�����Ʈ����
    {
        script.text = null;
        for (int i = 0; i < 10; i++)
        {
            Instantiate(FifthPrefab, spawnPosition[1] + new Vector3(Random.Range(-maskHalfWidth, maskHalfWidth), 0, 0), Quaternion.identity);
        }

        yield return new WaitForSeconds(5.0f);

        ChangePattern(Pattern.Sixth);
    }
    private IEnumerator Sixth() //ķ���ּ���  
    {
        int[] index = { 0, 2, 3, 5 };

        foreach (int idx in index)
        {
            Mask.transform.DOShakePosition(0.4f, 0.5f, 10);
            if (idx == 0) Instantiate(SixthPrefab, spawnPosition[idx], Quaternion.Euler(0, 0, Random.Range(-90.0f, 0.0f)));
            else if (idx == 2) Instantiate(SixthPrefab, spawnPosition[idx], Quaternion.Euler(0, 0, Random.Range(-180.0f, -90.0f)));
            else if (idx == 3) Instantiate(SixthPrefab, spawnPosition[idx], Quaternion.Euler(0, 0, Random.Range(0.0f, 90.0f)));
            else Instantiate(SixthPrefab, spawnPosition[idx], Quaternion.Euler(0, 0, Random.Range(90.0f, 180.0f)));

            yield return new WaitForSeconds(0.5f);
        }

        ChangePattern(Pattern.Seventh);
    }
    private IEnumerator Seventh() //��𰡼��� 
    {
        int[] index = { 0, 5, 2, 3, 1, 4 };
        float rotate = 0.0f;

        for(int i = 0; i<index.Length; i++)
        {
            Vector2 vector = spawnPosition[index[i + 1]] - spawnPosition[index[i]];
            rotate  = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            Instantiate(SixthPrefab, spawnPosition[index[i]], Quaternion.Euler(0,0,rotate));

            i++;

            vector = spawnPosition[index[i - 1]] - spawnPosition[index[i]];
            rotate = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            Instantiate(SixthPrefab, spawnPosition[index[i]], Quaternion.Euler(0, 0, rotate));

            yield return new WaitForSeconds(0.6f);
        }
        ChangePattern(Pattern.Eighth);
    }
    private IEnumerator Eighth() //�׸������ϼ���? - ����ũ ũ�� ��ȭ
    {
        Player.Speed = 2.0f;
        script.DOText("�� �׸� �����ϼ���?", 2.0f);
        yield return new WaitForSeconds(2.0f);
        script.text = null;

        Mask.transform.DOShakePosition(1.0f, 0.5f, 10);
        Mask.transform.DOScale(new Vector3(maskHalfWidth / 3.0f, maskHalfHeight * 0.5f, 1.0f), 1.0f);
        prevMaskPosition = Mask.transform.position;
        Player.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        maskInitialize();
        Player.GetComponent<Character>().MoveWithMask = true;

        ChangePattern(Pattern.Ninth);
    }

    private IEnumerator Ninth() //�׸� ����
    {
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)); //��ũ����ǥ���� ���ʾƷ� 0,0���κ��� ����
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector2[] positions = { new Vector2(-5.08f, 1.65f), new Vector2(-3.71f, -2.76f), new Vector2(1.29f, 2.11f), new Vector2(6.35f, 1.65f), new Vector2(1f, -3.25f), new Vector2(6.27f, -3.03f),  new Vector2(1.62f, -2.11f),  new Vector2(0.05f, 0.31f) , new Vector2(-3.2f, -1.28f), new Vector2(-4.79f, -0.02f) };

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 10; i++)
        {
            Instantiate(EighthPrefab, positions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(2.0f);

        ChangePattern(Pattern.Tenth);
    }

    private IEnumerator Tenth() //�� TIL �����ϼ��� - ����ũ ũ�� ��ȭ
    {
        Player.Speed = 5.0f;

        Player.gameObject.SetActive(false);
        Mask.transform.DOShakePosition(1.0f, 0.5f, 10);
        Mask.transform.DOMove(prevMaskPosition, 1.0f);
        Mask.transform.DOScale(new Vector3(maskHalfWidth * 3.0f * 4.0f, maskHalfHeight * 2.0f * 4.0f, 1.0f), 1.0f);
        yield return new WaitForSeconds(1.0f);

        maskInitialize();

        script.DOText("�� �� TIL �����ϼ���", 2.0f);
        yield return new WaitForSeconds(2.0f);

        ChangePattern(Pattern.Eleventh);
    }
    private IEnumerator Eleventh() //TIL ����
    {
        script.text = null;
        List<List<Vector2>> list = new List<List<Vector2>>();
        GameObject obj = null;

        list.Add(new List<Vector2>() { new Vector2(-2.02f, 0.07f), new Vector2(-0.19f, -2.02f), new Vector2(2.05f, -0.67f) });
        list.Add(new List<Vector2>() { new Vector2(-2.26f, -0.91f), new Vector2(-1.04f, 0.14f), new Vector2(-0.55f, -2.06f), new Vector2(1.16f, -1.13f), new Vector2(2.56f, 0.38f), new Vector2(2.83f, -2.11f) });
        list.Add(new List<Vector2>() { new Vector2(-2.63f, -1.25f), new Vector2(-2.21f, -2.2f), new Vector2(-2.01f, -0.23f), new Vector2(-0.68f, 0.37f), new Vector2(-0.46f, -1.02f), new Vector2(-0.59f, -1.92f), new Vector2(1.04f, -1.96f), new Vector2(1.44f, -1.04f), new Vector2(1.08f, -0.09f), new Vector2(2.42f, 0.61f), new Vector2(2.8f, -0.51f), new Vector2(2.78f, -1.9f) });
        list.Add(new List<Vector2>() { new Vector2(-2.3f, 0.57f), new Vector2(-2.68f, -1f), new Vector2(-2.45f, -1.72f), new Vector2(-2.28f, -2.31f), new Vector2(-2.56f, 0.02f), new Vector2(-1.94f, -0.49f), new Vector2(-1.11f, 0.25f), new Vector2(0.12f, -0.15f), new Vector2(-0.57f, -0.75f), new Vector2(-1.15f, -1.46f), new Vector2(-0.52f, -2.15f), new Vector2(0.49f, -1.42f), new Vector2(0.94f, -2.1f), new Vector2(2.06f, -2.15f), new Vector2(1.47f, -0.96f), new Vector2(3f, -1.78f), new Vector2(2.63f, -1.03f), new Vector2(2.37f, -0.27f), new Vector2(1.21f, 0.04f), new Vector2(0.3f, 0.72f), new Vector2(2.02f, 0.73f), new Vector2(3.15f, 0.67f), new Vector2(3.21f, 0.1f), new Vector2(3.25f, -2.43f) });

        for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < list[i].Count; j++)
            {
                if (i == 0) obj = Instantiate(EleventhPrefab, list[i][j], Quaternion.identity);
                else
                {
                    obj = Instantiate(obj, list[i - 1][j / 2], Quaternion.identity);
                    obj.transform.DOMove(list[i][j], 0.4f);
                }
            }
            yield return new WaitForSeconds (1.4f);
            obj.transform.localScale = obj.transform.localScale * 0.7f;
        }
        ChangePattern(Pattern.Twelfth);
    }
    private IEnumerator Twelfth() //�Ķ���Ʈ�� ����!
    {
        Player.transform.position = spawnPosition[4] + new Vector3(0, Player.transform.localScale.y * 0.5f, 0);
        Player.GetComponent<Character>().BlueHeart = true;

        Instantiate(TwelfthShortPrefab, maskPosition + new Vector3(2.0f, -2.0f, 0), Quaternion.identity);
        Instantiate(TwelfthLongPrefab, maskPosition + new Vector3(0, 1.0f, 0), Quaternion.identity);
        //������ ���� �� ������ �ȿ� ����ִ� ������Ʈ���� position�� �״�� ������ ä �����ȴ�. 
        //������ �ȿ� ����ִ� boneShort�� position�� -10�̾��ٸ�, ����ũ �߾ӿ� �������� �� �߾ӿ��� -10�� ��ġ�� boneShort�� ��Ÿ����.

        yield return new WaitForSeconds(5.0f);

        //Square.SetActive(true);

        ChangePattern(Pattern.Thirteenth);
    }

    private IEnumerator Thirteenth() 
    {
        yield return null;
        //Square.SetActive(false);
        Mask.transform.localScale = new Vector3(10, 3, 0);
        maskInitialize();
        Player.transform.position = spawnPosition[4] + new Vector3(0, Player.transform.localScale.y * 0.5f, 0);

        Instantiate(ThirteenthPrefab, maskPosition + new Vector3(-maskHalfWidth, 0, 0), Quaternion.identity);
        Instantiate(ThirteenthPrefab, maskPosition + new Vector3(maskHalfWidth, 0, 0), Quaternion.identity);

        yield return new WaitForSeconds(1.5f);

        //Square.SetActive(true);
        ChangePattern(Pattern.Fourteenth);
    }
    private IEnumerator Fourteenth() 
    {
        yield return null;
        //Square.SetActive(false);
        Mask.transform.localScale = new Vector3(9, 3.85f, 0);
        maskInitialize();
        Player.GetComponent<Character>().BlueHeart = false;

        int[] index = { 0, 5, 2, 3 };
        float rotate = 0.0f;

        for (int i = 0; i < index.Length; i++)
        {
            yield return new WaitForSeconds(0.6f);
            Vector2 vector = spawnPosition[index[i + 1]] - spawnPosition[index[i]];
            rotate = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            Instantiate(SixthPrefab, spawnPosition[index[i]], Quaternion.Euler(0, 0, rotate));

            i++;

            vector = spawnPosition[index[i - 1]] - spawnPosition[index[i]];
            rotate = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            Instantiate(SixthPrefab, spawnPosition[index[i]], Quaternion.Euler(0, 0, rotate));

        }

        yield return new WaitForSeconds(0.6f);

        //Square.SetActive(true);
        ChangePattern(Pattern.Fifteenth);
    }
    private IEnumerator Fifteenth() 
    {
        yield return null;
        Mask.transform.localScale = new Vector3(6, 3.5f, 0);
        maskInitialize();
        Player.GetComponent<Character>().MoveWithMask = true;
        Player.GetComponent<Character>().Ending = true;
    }


    private void maskInitialize()
    {
        Player.GetComponent<Character>().MoveWithMask = false;
        //��Ȱ��ȭ�Ǿ��־ GetComponent����
        GetMask();
        Player.transform.position = maskPosition;
        Player.gameObject.SetActive(true);
        Player.GetComponent<Character>().GetMask();
        SetSpawnPosition();
    }

}
