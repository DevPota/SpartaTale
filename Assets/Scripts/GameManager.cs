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

    [SerializeField] private Vector3   battlePivot;
    [SerializeField] private Vector3[] spawnPosition;

    [SerializeField] private GameObject scriptText;
    private Text script;

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
        UpdateTurn();
        //Mask.transform.DOShakePosition(0.8f, 0.5f, 10);
        //Invoke("Battle", 0.8f);
    }

    void Update()
    {
        if(Player.hp == 0)
        {
            //StopAllCoroutines();
            /* HP 0 �Ǹ� �÷��̾� ��ǲ ���ް� �ϰ� �ִϸ��̼� ��� �� ���� ���������� �̵� */
        }
        //if (Input.GetKey(KeyCode.Z) && pattern!= Pattern.Fourth && pattern != Pattern.Eighth && pattern != Pattern.Tenth)
        //{
        //    //StopAllCoroutines();
        //}
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
        float offsetY = 0.757f;
        int[] index = { 0, 3, 1, 4, 2, 5 };
        int i = 0;

        while (true)
        {
            Instantiate(StraightPrefab, spawnPosition[index[i++]] - new Vector3(offsetX, offsetY, 0), Quaternion.identity);
            if (i % 2 == 0) offsetX -= boneCount * 0.3f * 0.5f;
            offsetY *= -1;
            if (i == 2) offsetY = -0.757f;
            if (i == 4) offsetY = 0.757f;
            if (i == 6) break;

            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(1.0f);

        UpdateTurn();
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
        script.DOText("�� �׸� �����ϼ���?", 2.0f);
        yield return new WaitForSeconds(2.0f);
        script.text = null;

        Mask.transform.DOShakePosition(1.0f, 0.5f, 10);
        Mask.transform.DOScale(new Vector3(maskHalfWidth / 3.0f, maskHalfHeight * 0.5f, 1.0f), 1.0f);
        prevMaskPosition = Mask.transform.position;
        Player.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        maskInitialize();
        Player.GetComponent<Character>().moveWithMask = true;

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

        list.Add(new List<Vector2>() { new Vector2(-2.8f, -0.41f), new Vector2(-0.93f, -3.32f) , new Vector2(3.62f, -1.37f) });
        list.Add(new List<Vector2>() { new Vector2(-3.21f, -1.45f), new Vector2(-1.04f, 0.14f), new Vector2(-0.55f, -2.06f), new Vector2(1.86f, -3.4f), new Vector2(3.1f, 0.03f), new Vector2(3.92f, -1.86f) });
        list.Add(new List<Vector2>() { new Vector2(-4.3f, -0.9f), new Vector2(-4.07f, -3.75f), new Vector2(-2.13f, -0.37f), new Vector2(-0.49f, 0.33f), new Vector2(-1.64f, -2.22f), new Vector2(-1.09f, -3.51f), new Vector2(1.43f, -2.74f), new Vector2(2.6f, -3.6f), new Vector2(3.02f, 0.34f), new Vector2(4.42f, -0.09f), new Vector2(4.82f, -1.63f), new Vector2(4.66f, -2.8f) });
        list.Add(new List<Vector2>() { new Vector2(-3.95f, -0.24f), new Vector2(-3.75f, -2.07f), new Vector2(-4.62f, -3.28f), new Vector2(-3.72f, -3.94f), new Vector2(-2.56f, 0.02f), new Vector2(-1.94f, -0.49f), new Vector2(- 0.02f, 0.49f), new Vector2(0.29f, -0.37f), new Vector2(-1.44f, -1.95f), new Vector2(-0.85f, -2.07f), new Vector2(-1.32f, -3.43f), new Vector2(-0.07f, -3.9f), new Vector2(1.55f, -2.23f), new Vector2(2.02f, -2.43f), new Vector2(2.8f, -3.4f), new Vector2(3.7f, -3.87f), new Vector2(2.63f, -1.03f), new Vector2(3.33f, -0.83f), new Vector2(3.95f, 0.34f), new Vector2(4.85f, 0.57f), new Vector2(4.98f, -0.81f), new Vector2(5.41f, -1.59f), new Vector2(4.93f, -2.64f), new Vector2(3.92f, -2.17f) });


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
        Player.GetComponent<Character>().blueHeart = true;

        Instantiate(TwelfthShortPrefab, maskPosition + new Vector3(2.0f, -2.0f, 0), Quaternion.identity);
        Instantiate(TwelfthLongPrefab, maskPosition + new Vector3(0, 0.5f, 0), Quaternion.identity);
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

        Player.transform.position = spawnPosition[4] + new Vector3(0, Player.transform.localScale.y * 0.5f, 0);
        Player.GetComponent<Character>().blueHeart = true;

        Instantiate(TwelfthShortPrefab, maskPosition + new Vector3(0, -2.0f, 0), Quaternion.identity);
        Instantiate(TwelfthLongPrefab, maskPosition + new Vector3(0, 0.5f, 0), Quaternion.identity);
        //������ ���� �� ������ �ȿ� ����ִ� ������Ʈ���� position�� �״�� ������ ä �����ȴ�. 
        //������ �ȿ� ����ִ� boneShort�� position�� -10�̾��ٸ�, ����ũ �߾ӿ� �������� �� �߾ӿ��� -10�� ��ġ�� boneShort�� ��Ÿ����.

        yield return new WaitForSeconds(5.0f);

        //Square.SetActive(true);
    }

    private void maskInitialize()
    {
        Player.GetComponent<Character>().moveWithMask = false;
        //��Ȱ��ȭ�Ǿ��־ GetComponent����
        GetMask();
        Player.transform.position = maskPosition;
        Player.gameObject.SetActive(true);
        Player.GetComponent<Character>().GetMask();
        SetSpawnPosition();
    }

}
