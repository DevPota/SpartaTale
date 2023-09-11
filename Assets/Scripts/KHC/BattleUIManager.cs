using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    List<SelectMenu> selectMenuList = new List<SelectMenu>();

    public void Init()
    {
        for(int i = 0; i < 4; i++)
        {
            selectMenuList.Add(GameObject.Find("BattleBtn" + i).GetComponent<SelectMenu>());
        }
    }
}
