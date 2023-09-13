using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    #region Singleton
    public static BattleUIManager I;

    public void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    List<SelectMenu> selectMenuList = new List<SelectMenu>();
    public Button[] menuButtons;

    public GameObject[] menuCanvas;

    public Text InformationTxt;
    public Text itemTxt;

    private bool isTxtActive = false;

    [SerializeField] public GameObject Dialog;

    public bool isClicked = false;

    public List<SelectMenu> SelectMenuList { get; private set; } = new List<SelectMenu>();

    public TMP_Text PlayerLevelText { get; private set; } = null;
    public TMP_Text PlayerKrText { get; private set; } = null;
    public Slider PlayerHpSlider { get; private set; } = null;

    private void Start()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            int buttonIndex = i; 
            menuButtons[i].onClick.AddListener(() => OnMenuButtonClick(buttonIndex));
        }

    }
    public void Init()
    {
        PlayerHpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        PlayerKrText = GameObject.Find("PlayerKRText").GetComponent<TMP_Text>();
        PlayerLevelText = GameObject.Find("PlayerLevelText").GetComponent<TMP_Text>();

        for (int i = 0; i < 4; i++)
        {
            selectMenuList.Add(GameObject.Find("BattleBtn" + i).GetComponent<SelectMenu>());
        }
    }

    public void SetPlayerKrText(int hp, int maxHp)
    {
        PlayerKrText.text = "" + hp + " / " + maxHp;
    }

    public void SetPlayerHpSlider(float value)
    {
        PlayerHpSlider.value = value;
    }

    public void Release()
    {
        foreach(GameObject canvas in menuCanvas)
        {
            canvas.SetActive(false);
        }
    }

    private void OnMenuButtonClick(int menuIndex)
    {
        Dialog.SetActive(false);

        if(isClicked == true && GameManager.I.Player.MoveWithMask == false)
        {
            return;
        }

        // 버튼 별로 다른 동작 수행 가능
        if (menuIndex == 0)
        {
            menuCanvas[0].gameObject.SetActive(false);
            GameManager.I.OnAttack();

            isClicked = true;
        }
        else if (menuIndex == 1)
        {
            menuButtons[1].gameObject.SetActive(false);
            menuButtons[4].gameObject.SetActive(true);
        }
        else if (menuIndex == 2)
        {
            menuButtons[2].gameObject.SetActive(false);
            itemTxt.gameObject.SetActive(true);
            GameManager.I.Player.Hp = GameManager.I.Player.MaxHp;
            GameManager.I.UpdateTurn();
            isClicked = true;
        }
        else if (menuIndex == 3)
        {
            menuCanvas[3].gameObject.SetActive(false);
            GameManager.I.UpdateTurn();
            isClicked = true;
        }
        else if(menuIndex == 4)
        {
            menuButtons[4].gameObject.SetActive(false);
            InformationTxt.gameObject.SetActive(true);
            Debug.Log("체크 버튼");
        }
        else if (menuIndex == 5)
        {
            menuCanvas[2].gameObject.SetActive(false);
            Debug.Log("아이템 없음");
        }
    }
}