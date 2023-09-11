using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpKrBar : MonoBehaviour
{
    // HP
    [SerializeField]private Slider hpbar;
    private float maxHp = 100;
    [SerializeField] private float currentHp = 100;

    //KR
    [SerializeField] private Slider krbar;
    private float maxKr = 100;
    [SerializeField] private float currentKr = 100;


    // Start is called before the first frame update
    void Start()
    {
        hpbar.value = (float)currentHp / (float)maxHp;
        hpbar.value = (float)currentKr / (float)maxKr;

        TypingItem.UseItem += UsedItem; // 구독
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // HP 감소[테스트]
        {
            currentHp -= 10;
            if (currentHp < 0)
            {
                currentHp = 0;
            }
            
        }
        HandleHp();


        if (Input.GetKeyDown(KeyCode.U)) // KR 감소[테스트]
        {
            currentKr -= 5;
            if (currentKr < 0)
            {
                currentKr = 0;
            }
        }
        HandleKr();

       
    }

    private void HandleHp()
    {
        hpbar.value = (float)currentHp / (float)maxHp;
    }

    private void HandleKr()
    {
        krbar.value = (float)currentKr / (float)maxKr;
    }
    
    private void UsedItem()
    {
        currentHp += 100;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        HandleHp();
    }

    private void OnDestroy()
    {
        TypingItem.UseItem -= UsedItem;
    }
}
