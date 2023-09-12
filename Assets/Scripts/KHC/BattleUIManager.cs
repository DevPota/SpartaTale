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
        if(I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public List<SelectMenu> SelectMenuList  { get; private set; }  = new List<SelectMenu>();

    public TMP_Text         PlayerLevelText { get; private set; } = null;
    public TMP_Text         PlayerKrText    { get; private set; } = null;
    public Slider           PlayerHpSlider  { get; private set; } = null;

    public void Init()
    {
        PlayerHpSlider  = GameObject.Find("HpSlider").GetComponent<Slider>();
        PlayerKrText    = GameObject.Find("PlayerKRText").GetComponent<TMP_Text>();
        PlayerLevelText = GameObject.Find("PlayerLevelText").GetComponent<TMP_Text>();

        for(int i = 0; i < 4; i++)
        {
            SelectMenuList.Add(GameObject.Find("BattleBtn" + i).GetComponent<SelectMenu>());
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
}
