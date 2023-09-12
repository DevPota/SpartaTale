using System;
using UnityEngine;

public class MenuUIButton : MonoBehaviour
{
    Action onClickListener = null;

    public void Init(Action onClickAction)
    {
        onClickListener = onClickAction;
    }

    public void OnClick()
    {
        GetComponent<AudioSource>().Play();
        onClickListener?.Invoke();
    }
}
